using KansukWebApp.Context;
using KansukWebApp.Models;
using KansukWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace KansukWebApp.Controllers
{
    public class AssignmentController : Controller
    {
        private readonly AppDbContext _context;

        public AssignmentController(AppDbContext context)
        {
            _context = context;
        }

        // ZİMMET LİSTESİ SAYFASI (INDEX)
        public async Task<IActionResult> Index()
        {
            // İlgili User verisi ile tüm zimmetleri çekiyoruz.
            var assignments = await _context.Assignments
                                            .Include(a => a.User)
                                            .OrderByDescending(a => a.CreatedAt)
                                            .ToListAsync();

            // Yeni Zimmet Giriş sayfasına yönlendirme butonunu view'da kullanacağız.
            return View(assignments);
        }

        // ----------------------------------------------------------------
        // ZİMMET GİRİŞ / GÜNCELLEME FORMU (UPSERT - GET)
        // ----------------------------------------------------------------
        public IActionResult Upsert(int? id)
        {
         //   AssignmentVM assignmentVM = new AssignmentVM()
         //   {
         //       Assignment = new Assignment(),
         //       UserList = _context.Users.Select(u => new SelectListItem
         //       {
         //           Text = u.Name,
         //           Value = u.Id.ToString()
         //       })
         //   };

            // Zimmet ve User listesini içerecek ViewModel'i hazırlıyoruz.
            AssignmentVM assignmentVM = new AssignmentVM()
            {
                Assignment = new Assignment(),
                UserList = _context.Users
                    .Where(u => u.Status) // <-- BU SATIRI EKLE: Sadece Status'ü TRUE olanları filtrele
                    .Select(u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id.ToString()
                    })
            };

            if (id == null || id == 0)
            {
                // YENİ KAYIT DURUMUNDA CreatedAt'i OTOMATİK DOLDUR
                assignmentVM.Assignment.CreatedAt = DateTime.Now;
                return View(assignmentVM);
            }
            else
            {
                // Güncelleme durumunda bu alana dokunmuyoruz, mevcut değer korunacak.
                assignmentVM.Assignment = _context.Assignments.Find(id);
                if (assignmentVM.Assignment == null)
                {
                    return NotFound();
                }
                return View(assignmentVM);
            }
        }

        // ----------------------------------------------------------------
        // ZİMMET KAYDETME (UPSERT - POST)
        // ----------------------------------------------------------------
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(AssignmentVM assignmentVM)
        {
            if (ModelState.IsValid)
            {
                if (assignmentVM.Assignment.Id == 0)
                {
                    // YENİ KAYIT DURUMU: CreateAt değerini burada tekrar otomatik doldur
                    // Çünkü View'dan dönerken bu değer kaybolmuş oluyor.
                    assignmentVM.Assignment.CreatedAt = DateTime.Now;
                    _context.Assignments.Add(assignmentVM.Assignment);
                }
                else
                {
                    // GÜNCELLEME: Mevcut kaydın CreatedAt'ini veritabanından koruyoruz.
                    var objFromDb = await _context.Assignments.AsNoTracking().FirstOrDefaultAsync(a => a.Id == assignmentVM.Assignment.Id);
                    assignmentVM.Assignment.CreatedAt = objFromDb.CreatedAt;

                    _context.Assignments.Update(assignmentVM.Assignment);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Listeleme sayfasına dön
            }

            // Eğer validasyon hatası varsa, UserList'i tekrar doldurmalıyız.
            assignmentVM.UserList = _context.Users.Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(assignmentVM);
        }
        // ----------------------------------------------------------------
        // ZİMMET SİLME (DELETE - POST)
        // ----------------------------------------------------------------
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var assignment = await _context.Assignments.FindAsync(id);
            if (assignment == null)
            {
                return NotFound();
            }

            _context.Assignments.Remove(assignment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}