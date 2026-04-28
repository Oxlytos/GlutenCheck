using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Gluten.Models;
using Gluten.ContractDTOs.Models;
using Infrastructure.Gluten.Interfaces;
using Infrastructure.Gluten.Managers;

namespace Infrastructure.Gluten.Services
{
    public class AllergenParser : IAllergenParser
    {
        public async Task<AllergenResult> ParseAllergen(string openFoodReturnModel)
        {
            Console.WriteLine(openFoodReturnModel);
            var allergenResult = JsonSerializer.Deserialize<AllergensModel>(openFoodReturnModel);

            var returnObject = Create(allergenResult);
            return returnObject;
        }

        public AllergenResult Create(AllergensModel requestData)
        {
            AllergenResult result = new AllergenResult();
            result.RegiesteredAllergenResults = new List<Allergen>();

            foreach(var allergen in AllergenManager.ExampleAllergens)
            {
                if(requestData.Model.MainAllergen.Contains(allergen.Name, StringComparison.OrdinalIgnoreCase))
                {
                    result.RegiesteredAllergenResults.Add(allergen);
                }
                if(requestData.Model.SecondaryAllergensField.Contains(allergen.Name, StringComparison.OrdinalIgnoreCase))
                {
                    result.RegiesteredAllergenResults.Add(allergen);
                }
            }


                return result;


        }
    }
}
