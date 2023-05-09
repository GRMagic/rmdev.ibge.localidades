using System.Text.Json.Serialization;

namespace rmdev.ibge.localidades
{
    public class Municipio
    {
        public long Id { get; set; }

        public string Nome { get; set; }

        public Microrregiao Microrregiao { get; set; }

        [JsonPropertyName("regiao-imediata")]
        public RegiaoImediata RegiaoImediata { get; set; }
    }
}