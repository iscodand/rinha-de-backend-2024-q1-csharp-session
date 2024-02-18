using System.Text.Json.Serialization;
using RinhaCrebito.Entities;

namespace RinhaCrebito.DTOs.Responses
{
    public class CustomerResponse
    {
        [JsonPropertyName("total")]
        public int Total { get; set; }

        [JsonPropertyName("limite")]
        public int Limit { get; set; }

        [JsonPropertyName("data_extrato")]
        public DateTime ExtractDate { get; set; }

        public static CustomerResponse Map(Customer customer) => new()
        {
            Total = customer.Balance.Total,
            Limit = customer.Limit,
            ExtractDate = DateTime.Now
        };
    }
}