using System.Text.Json.Serialization;

namespace rmdev.ibge.localidades
{
    public class PaisId : Id49
    {
        /// <summary>
        /// Identificador especificado pela norma ISO ALPHA-2, que define o identificador do país usando 2 letras
        /// </summary>
        [JsonPropertyName("ISO-ALPHA-2")]
        public string IsoAlfa2 { get; set; }

        /// <summary>
        /// Identificador especificado pela norma ISO ALPHA-3, que define o identificador do país usando 3 letras
        /// </summary>
        [JsonPropertyName("ISO-ALPHA-3")] 
        public string IsoAlfa3 { get; set; }
    }
}