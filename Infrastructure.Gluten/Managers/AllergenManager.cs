using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Gluten.Models;

namespace Infrastructure.Gluten.Managers
{
    public static class AllergenManager
    {
        public static List<Allergen> ExampleAllergens { get; set; } = new List<Allergen>();

        //Main name, then other keywords related to the name
        public static Dictionary<string, List<string>> AllergenMap = new()
        {
            { "Gluten", new() { "gluten", "wheat", "barley", "rye", "oats" } },
            { "Milk", new() { "milk", "lactose", "casein" } },
            { "Eggs", new() { "egg", "albumin" } },
            { "Peanuts", new() { "peanut" } },
            { "Tree Nuts", new() { "almond", "hazelnut", "walnut", "cashew", "pistachio" } },
            { "Soy", new() { "soy", "soya" } },
            { "Fish", new() { "fish" } },
            { "Shellfish", new() { "shrimp", "prawn", "crab", "lobster" } },
            { "Sesame", new() { "sesame" } },
            { "Celery", new() { "celery" } },
            { "Mustard", new() { "mustard" } },
            { "Lupin", new() { "lupin" } },
            { "Molluscs", new() { "mussel", "clam", "oyster", "squid" } },
            { "Sulphites", new() { "sulphite", "sulfite", "e220", "e221", "e222", "e223", "e224", "e226", "e227", "e228" } }
        };

        public static  Task CreateInitalAllergens()
        {

           ExampleAllergens = AllergenMap.Select(
               a=> new Allergen 
               { 
                   //Main name i e gluten
                   Name = a.Key,

                   //Wheat, rye, oats, other gluten thing
                   Keywords = a.Value,
               
               }).ToList();
           return Task.CompletedTask;

        }
        public static async Task AddAllergen(Allergen allergen)
        {
            int id = ExampleAllergens.Count + 1;
            allergen.Id=id;
            ExampleAllergens.Add(allergen);
        }
    }
}
