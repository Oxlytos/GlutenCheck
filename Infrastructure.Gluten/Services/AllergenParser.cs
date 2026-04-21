using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Gluten.Models;
using Gluten.ContractDTOs.Models;
using Infrastructure.Gluten.Interfaces;

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

            if (requestData.Model.MainAllergen.Contains("gluten",StringComparison.InvariantCultureIgnoreCase))
            {
                result.IsGlutenFree = false;
            }
            else if(requestData.Model.SecondaryAllergensField.Contains("gluten", StringComparison.InvariantCultureIgnoreCase))
            {
                result.IsGlutenFree = false;
            }
            else
            {
                result.IsGlutenFree = true;
            }

                return result;


        }
    }
}
