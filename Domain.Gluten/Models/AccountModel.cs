using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Gluten.Models
{
    public class AccountModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateOnly BirthDate { get; set; }

        public List<Allergen> RegisteredAllergens { get; set; } = new List<Allergen>();

        public string LoggedInDisplayString => $"{FirstName} {LastName} {BirthDate}";
    }
}
