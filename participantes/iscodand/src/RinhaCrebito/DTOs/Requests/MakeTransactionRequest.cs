using System.Text.Json.Serialization;

namespace RinhaCrebito.DTOs.Requests
{
    public class MakeTransactionRequest
    {
        [JsonPropertyName("valor")]
        public int Value { get; set; }

        [JsonPropertyName("tipo")]
        public string Type { get; set; }

        [JsonPropertyName("descricao")]
        public string Description { get; set; }
    }
}