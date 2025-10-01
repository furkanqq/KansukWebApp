using KansukWebApp.Context;
using KansukWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Bu using'i ekle
using System.Linq;
using System.Threading.Tasks;

namespace KansukWebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        // ----------------------------------------------------------------
        // 1. KULLANICI LİSTESİ (READ)
        // ----------------------------------------------------------------
        public async Task<IActionResult> Index()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }

        // ----------------------------------------------------------------
        // 2. YENİ KAYIT / GÜNCELLEME FORMU (CREATE / UPDATE - GET)
        // ----------------------------------------------------------------
        public async Task<IActionResult> Upsert(int? id)
        {
            User user = new User();

            if (id == null || id == 0)
            {
                // Yeni Kayıt
                return View(user);
            }
            else
            {
                // Güncelleme
                user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                return View(user);
            }
        }

        // ----------------------------------------------------------------
        // 3. KAYIT KAYDETME (CREATE / UPDATE - POST)
        // ----------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(User user)
        {
            if (ModelState.IsValid)
            {
                if (user.Id == 0)
                {
                    // Yeni Kayıt
                    _context.Users.Add(user);
                }
                else
                {
                    // Güncelleme
                    _context.Users.Update(user);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // ----------------------------------------------------------------
        // 4. KAYIT SİLME (DELETE - POST)
        // ----------------------------------------------------------------
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}