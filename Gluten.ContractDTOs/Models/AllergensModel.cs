using System.Text.Json.Serialization;

namespace Gluten.ContractDTOs.Models
{
    public class AllergensModel
    {
        [JsonPropertyName("product")]
        public  RequestModel Model {get;set;}
    }
    public class RequestModel
    {
        [JsonPropertyName("_id")]
        public string Id {get;set;}

        [JsonPropertyName("allergens")]
        public string MainAllergen { get;set;}

        [JsonPropertyName("brands")]
        public string Brand { get; set;}

        [JsonPropertyName("code")]
        public string BarCode { get; set;}  

    }
}
