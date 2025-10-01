using KansukWebApp.Context;
using KansukWebApp.Models; // AppDbContext i�in
using KansukWebApp.ViewModels; // HomeVM'i kullanmak i�in
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // ToListAsync() i�in
using System.Diagnostics;
using System.Threading.Tasks;

namespace KansukWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context; // AppDbContext'i enjekte et

        public HomeController(ILogger<HomeController> logger, AppDbContext context) // Constructor'� g�ncelle
        {
            _logger = logger;
            _context = context; // Atama yap
        }

        public async Task<IActionResult> Index()
        {
            // Veritaban�ndan say�lar� �ek
            int totalUsers = await _context.Users.Where(u => u.Status).CountAsync();
            int totalAssignments = await _context.Assignments.CountAsync();

            // ViewModel'e doldur
            HomeVM homeVM = new HomeVM
            {
                TotalUsers = totalUsers,
                TotalAssignments = totalAssignments
            };

            // ViewModel'i View'a g�nder
            return View(homeVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}