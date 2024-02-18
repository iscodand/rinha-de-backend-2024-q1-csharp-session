using System.Text.Json.Serialization;
using RinhaCrebito.Entities;

namespace RinhaCrebito.DTOs.Responses
{
    public class TransactionResponse
    {
        [JsonPropertyName("valor")]
        public int Value { get; set; }

        [JsonPropertyName("tipo")]
        public string Type { get; set; }

        [JsonPropertyName("descricao")]
        public string Description { get; set; }

        [JsonPropertyName("realizada_em")]
        public DateTime CreatedAt { get; set; }

        public static ICollection<TransactionResponse> Map(ICollection<Transaction> transactions)
        {
            return transactions.Select(transaction => new TransactionResponse
            {
                Value = transaction.Value,
                Type = transaction.Type,
                Description = transaction.Description,
                CreatedAt = transaction.CreatedAt
            }).ToList();
        }
    }
}