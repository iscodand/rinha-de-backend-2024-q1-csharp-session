using System.Text.Json.Serialization;
using System.Transactions;

namespace RinhaCrebito.DTOs.Responses
{
    public class ExtractResponse
    {
        [JsonPropertyName("saldo")]
        public CustomerResponse Balance { get; set; }

        [JsonPropertyName("ultimas_transacoes")]
        public ICollection<TransactionResponse> Transactions { get; set; }
    }
}