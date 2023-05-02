using System.Text.Json.Serialization;

namespace rmdev.ibge.localidades
{
    public class RegiaoImediata
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        [JsonPropertyName("regiao-intermediaria")]
        public RegiaoIntermediaria RegiaoIntermediaria { get; set; }
    }
}