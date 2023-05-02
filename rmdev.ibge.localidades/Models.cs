using Refit;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace rmdev.ibge.localidades
{

    public class Pais
    {
        public PaisId Id { get; set; }
        public string Nome { get;set; }

        [JsonPropertyName("regiao-intermediaria")]
        public Regiao RegiaoIntermediaria { get; set; }

        [JsonPropertyName("sub-regiao")]
        public SubRegiao SubRegiao { get; set; }
    }


    public class Regiao
    {
        public Id49 Id { get; set; }

        public string Nome { get; set; }
    }

    public class SubRegiao : Regiao
    {
        public Regiao Regiao { get; set; }
    }

    public interface IIBGE
    {
        [Get("/api/v1/localidades/paises/{codigosPais}")]
        Task<IEnumerable<Pais>> PaisesAsync(params int[] codigosPais);
    }

    public class IBGEClientFactory
    {
        public const string BaseURL = "https://servicodados.ibge.gov.br/";

        public readonly RefitSettings RefitSettings = new()
        {
            UrlParameterFormatter = new PipeUrlParameterFormatter()
        };

        public IIBGE Build()
        {
            return RestService.For<IIBGE>(BaseURL, RefitSettings);
        }
    }
}