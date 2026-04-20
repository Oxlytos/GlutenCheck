using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Gluten.ContractDTOs.Models;
using Infrastructure.Gluten.Interfaces;

namespace Infrastructure.Gluten.Services
{
    public class AllergenParser : IAllergenParser
    {
        public async Task<AllergensModel> ParseAllergen(string openFoodReturnModel)
        {
            Console.WriteLine(openFoodReturnModel);
            var allergenResult = JsonSerializer.Deserialize<AllergensModel>(openFoodReturnModel);

            Console.WriteLine(allergenResult);
            return allergenResult;
        }
    }
}
