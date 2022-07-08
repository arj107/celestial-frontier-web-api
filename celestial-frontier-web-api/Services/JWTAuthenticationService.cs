using AutoMapper;
using celestial_frontier_web_api.Models;
using celestial_frontier_web_api.Models.DTOs;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace celestial_frontier_web_api.Services {
	public interface IJWTAuthenticationService {
		Tokens Authenticate(AuthenticateRequest authRequest);

		/// <summary>
		/// Parses the Bearer token in the HttpRequest Authorization header to retrieve the UserGuid, 
		/// <br></br> which is needed to retrieve many User-specific resources.<br></br>
		/// <br></br> HttpRequestException(HttpStatusCode.Unauthorized) is thrown if the User is not 
		/// <br></br> authenticated or if the Authorization header is not a readable Bearer token.
		/// </summary>
		/// <param name="httpContext"></param>
		/// <returns>UserGuid if the User exists and is authenticated.</returns>
		/// <exception cref="HttpRequestException"></exception>
		Guid GetUserGuid(HttpContext httpContext);
	}
	public class JWTAuthenticationService : IJWTAuthenticationService {

		private readonly IConfiguration _configuration;
		private readonly CelestialFrontier_DevContext _context;
		private readonly IMapper _mapper;
		public JWTAuthenticationService(IConfiguration iconfiguration, CelestialFrontier_DevContext context, IMapper mapper) {
			_configuration = iconfiguration;
			_context = context;
			_mapper = mapper;
		}

		public Tokens Authenticate(AuthenticateRequest authRequest) {
			var user = _context.Users.SingleOrDefault(x => x.Username == authRequest.Username);
			// return null if user not found
			if (user == null)
				return null;

			byte[] salt = Convert.FromBase64String(user.PasswordSalt);
			string hashedPassword = Convert.ToBase64String(
				KeyDerivation.Pbkdf2(
					password: authRequest.Password,
					salt: salt,
					prf: KeyDerivationPrf.HMACSHA256,
					iterationCount: 100000,
					numBytesRequested: 256 / 8
				)
			);

			// return null the password did not match
			if (!user.PasswordHash.Equals(hashedPassword)) {
				return null;
			}

			UserDto userDto = _mapper.Map<User, UserDto>(user);


			// generate token that is valid for 7 days
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
			var tokenDescriptor = new SecurityTokenDescriptor {
				Subject = new ClaimsIdentity(
					new[] {
						new Claim("UserGuid", user.UserGuid.ToString()),
						new Claim("Username", user.Username)
					}
				),
				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return new Tokens { Token = tokenHandler.WriteToken(token) };
		}

		/// <summary>
		/// Parses the Bearer token in the HttpRequest Authorization header to retrieve the UserGuid, 
		/// <br></br> which is needed to retrieve many User-specific resources.<br></br>
		/// <br></br> HttpRequestException(HttpStatusCode.Unauthorized) is thrown if the User is not 
		/// <br></br> authenticated or if the Authorization header is not a readable Bearer token.
		/// </summary>
		/// <param name="httpContext"></param>
		/// <returns>UserGuid if the User exists and is authenticated.</returns>
		/// <exception cref="HttpRequestException"></exception>
		public Guid GetUserGuid(HttpContext httpContext) {
			if (!httpContext.User.Identity.IsAuthenticated) {
				throw new HttpRequestException(HttpStatusCode.Unauthorized.ToString());
			}
			string authHeader = httpContext.Request.Headers.Authorization;
			if (authHeader == null) {
				throw new HttpRequestException(HttpStatusCode.Unauthorized.ToString());
			}

			string jwtEncodedString = authHeader.Substring(7);
			var token = new JwtSecurityToken(jwtEncodedString);
			Guid userGuid = Guid.Parse(token.Claims.First(c => c.Type == "UserGuid").Value);
			return userGuid;
		}
	}
}
