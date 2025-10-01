using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace KansukWebApp.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        public string ItemType { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }

        [ValidateNever]
        public virtual User User { get; set; }
    }
}
