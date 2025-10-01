using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace KansukWebApp.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public bool Status { get; set; }

        [ValidateNever]
        public virtual ICollection<Assignment> Assignments { get; set; }

    }
}
