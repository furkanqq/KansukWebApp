// ViewModels/AssignmentVM.cs

using KansukWebApp.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KansukWebApp.ViewModels
{
    public class AssignmentVM
    {
        // 1. Ana Model: Zimmet Kaydı
        public Assignment Assignment { get; set; }

        // 2. Kişi Seçimi İçin Gerekli Liste
        // Dropdown listesi için SelectListItem koleksiyonu kullanıyoruz.
        [ValidateNever]
        public IEnumerable<SelectListItem> UserList { get; set; }
    }
}