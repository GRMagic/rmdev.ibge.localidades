using System.Text.Json.Serialization;

namespace rmdev.ibge.localidades
{
    public class RegiaoImediata : Localidade
    {
        [JsonPropertyName("regiao-intermediaria")]
        public RegiaoIntermediaria RegiaoIntermediaria { get; set; }
    }
}