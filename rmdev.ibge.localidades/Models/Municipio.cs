using System.Text.Json.Serialization;

namespace rmdev.ibge.localidades
{
    public class Municipio : Localidade
    {
        public Microrregiao Microrregiao { get; set; }

        [JsonPropertyName("regiao-imediata")]
        public RegiaoImediata RegiaoImediata { get; set; }
    }
}