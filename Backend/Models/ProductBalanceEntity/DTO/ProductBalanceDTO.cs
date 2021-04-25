using Newtonsoft.Json;

namespace Backend.Models.ProductBalanceEntity.DTO
{
    public class ProductBalanceDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("medicamentName")]
        public string Name { get; set; }
        
        [JsonProperty("price")]
        public decimal Price { get; set; }
        
        [JsonProperty("amount")]
        public double Amount { get; set; }
        
        [JsonProperty("expirationDate")]
        public string ExpirationDate { get; set; }
        
        public ProductBalanceDTO(ProductBalance pb)
        {
            Id = pb.Id;
            Name = pb.Medicament.Name;
            Price = pb.Medicament.CalculatePriceReimbursed();
            Amount = pb.Amount;
            ExpirationDate = pb.ExpirationDate.Date.ToString("O");
        }
    }
}
