using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Infrastructure.Gluten.Interfaces;

namespace Infrastructure.Gluten.Services
{
    public class ProductFetcher : IProductFetcher
    {
        private string _baseUrl = "https://world.openfoodfacts.net/api/v3/product/";

        private HttpClient _httpClient;
        public ProductFetcher(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_baseUrl);
        }

        public async Task<string?> GetProductData(string barcode)
        {
            if(barcode == null||string.IsNullOrEmpty(barcode))
            {
                return null;
            }
            Regex reg = new Regex("[\\d{13}$]");
            if (!reg.IsMatch(barcode))
            {
                return null;
            }
            HttpResponseMessage msg = await _httpClient.GetAsync(barcode);

            try
            {
                if (msg.IsSuccessStatusCode)
                {
                    var content = await msg.Content.ReadAsStringAsync();
                    if (content != null)
                    {
                        Console.WriteLine(content);
                        return content;
                    }
                }
                else
                {
                    Console.WriteLine(msg.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception {ex.Message}");
            }
            return null;
        }
    }
}
