using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Gluten.Models;
using Gluten.ContractDTOs.Models;

namespace Infrastructure.Gluten.Interfaces
{
    public interface IAllergenParser
    {
        Task<AllergenResult> ParseAllergen(string openFoodReturnModel);
    }
}
