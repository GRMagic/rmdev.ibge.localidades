using System.Text.Json.Serialization;

namespace rmdev.ibge.localidades
{
    public class Pais
    {
        public PaisId Id { get; set; }
        public string Nome { get; set; }

        [JsonPropertyName("regiao-intermediaria")]
        public Regiao? RegiaoIntermediaria { get; set; }

        [JsonPropertyName("sub-regiao")]
        public SubRegiao SubRegiao { get; set; }
    }
}