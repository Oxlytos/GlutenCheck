using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Gluten.Interfaces
{
    public interface IProductFetcher
    {
        Task<string?> GetProductData(string barcode);
    }
}
