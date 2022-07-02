using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using celestial_frontier_web_api.Models;

namespace celestial_frontier_web_api.WebControllers {
    public class UsersController : Controller {
        private readonly CelestialFrontier_DevContext _context;

        public UsersController(CelestialFrontier_DevContext context) {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index() {
            return View(await _context.Users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(Guid? id) {
            if (id == null || _context.Users == null) {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserGuid == id);
            if (user == null) {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create() {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserGuid,Username,PasswordHash")] User user) {
            if (ModelState.IsValid) {
                user.UserGuid = Guid.NewGuid();

                byte[] salt = new byte[128 / 8];
                RandomNumberGenerator.Create().GetBytes(salt);
                string hashedPassword = Convert.ToBase64String(
                    KeyDerivation.Pbkdf2(
                        password: user.PasswordHash,
                        salt: salt,
                        prf: KeyDerivationPrf.HMACSHA256,
                        iterationCount: 100000,
                        numBytesRequested: 256 / 8
                    )
                );
                user.PasswordHash = hashedPassword;
                user.PasswordSalt = Convert.ToBase64String(salt);
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Login
        public async Task<IActionResult> Login(Guid? id, string password) {
            if (id == null || _context.Users == null) {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null) {
                return NotFound();
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(Guid? id) {
            if (id == null || _context.Users == null) {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null) {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("UserGuid,Username")] User user) {
            if (id != user.UserGuid) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!UserExists(user.UserGuid)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(Guid? id) {
            if (id == null || _context.Users == null) {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserGuid == id);
            if (user == null) {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id) {
            if (_context.Users == null) {
                return Problem("Entity set 'CelestialFrontier_DevContext.Users'  is null.");
            }
            var user = await _context.Users.FindAsync(id);
            if (user != null) {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(Guid id) {
            return _context.Users.Any(e => e.UserGuid == id);
        }
    }
}
