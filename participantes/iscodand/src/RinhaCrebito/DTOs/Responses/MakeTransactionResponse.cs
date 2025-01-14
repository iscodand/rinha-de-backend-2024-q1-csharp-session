using System.Text.Json.Serialization;

namespace RinhaCrebito.DTOs.Responses
{
    public class MakeTransactionResponse
    {
        [JsonPropertyName("saldo")]
        public int Balance { get; set; }

        [JsonPropertyName("limite")]
        public int Limit { get; set; }
    }
}