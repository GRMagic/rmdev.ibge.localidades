using Refit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rmdev.ibge.localidades
{
    public interface IIBGE
    {
        [Get("/api/v1/localidades/paises/{codigosPais}")]
        Task<IEnumerable<Pais>> BuscarPaisesAsync(params int[] codigosPais);

        [Get("/api/v1/localidades/paises/{codigoPais}")]
        Task<IEnumerable<Pais>> BuscarPaisesAsync(int codigoPais, [AliasAs("lang")] Idioma idioma);

        public async Task<Pais?> BuscarPaisAsync(int codigoPais, [AliasAs("lang")] Idioma idioma = Idioma.PT)
        {
            var paises = await BuscarPaisesAsync(codigoPais, idioma);
            return paises.FirstOrDefault();
        }

        [Get("/api/v1/localidades/paises/{codigosPais}")]
        Task<IEnumerable<Pais>> BuscarPaisesAsync(int[] codigosPais, [AliasAs("lang")] Idioma idioma);


        [Get("/api/v1/localidades/regioes/{macrorregiao}")]
        Task<Macrorregiao> BuscarMacrorregiaoAsync(int macrorregiao);

        [Get("/api/v1/localidades/regioes/{macrorregiao}")]
        Task<IEnumerable<Macrorregiao>> BuscarMacrorregioesAsync(params int[] macrorregiao);

    }
}