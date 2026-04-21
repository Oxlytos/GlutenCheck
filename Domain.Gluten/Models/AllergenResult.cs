using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Gluten.Models
{
    public class AllergenResult
    {
        //Set this to false at ANY hint of gluten
        public bool IsGlutenFree { get; set; }
        public string GlutenStatus => IsGlutenFree ? "Kan vara FRI från gluten" : "Innehåller nog gluten";
    }
}
