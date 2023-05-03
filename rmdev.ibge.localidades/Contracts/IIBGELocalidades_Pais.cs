using Refit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rmdev.ibge.localidades
{
    public partial interface IIBGELocalidades
    {

        /// <summary>
        /// Obtém o conjunto de países
        /// </summary>
        /// <param name="codigosPais">Identificadores de países</param>
        /// <returns>Lista de países</returns>
        [Get("/api/v1/localidades/paises/{codigosPais}")]
        Task<List<Pais>> BuscarPaisesAsync(params int[] codigosPais);

        /// <summary>
        /// Obtém o conjunto de países
        /// </summary>
        /// <param name="codigoPais">Identificador do país</param>
        /// <param name="idioma">Idioma de retorno</param>
        /// <returns>Lista com um país</returns>
        [Get("/api/v1/localidades/paises/{codigoPais}")]
        Task<List<Pais>> BuscarPaisesAsync(int codigoPais, [AliasAs("lang")] Idioma idioma);

        /// <summary>
        /// Obtém um único país
        /// </summary>
        /// <param name="codigoPais">Identificador do país</param>
        /// <param name="idioma">Idioma de retorno</param>
        /// <returns>Dados do país</returns>
        public async Task<Pais?> BuscarPaisAsync(int codigoPais, [AliasAs("lang")] Idioma idioma = Idioma.PT)
        {
            var paises = await BuscarPaisesAsync(codigoPais, idioma);
            return paises.FirstOrDefault();
        }

        /// <summary>
        /// Obtém o conjunto de países
        /// </summary>
        /// <param name="codigosPais">Identificadores de países</param>
        /// <param name="idioma">Idioma de retorno</param>
        /// <returns>Lista de países</returns>
        [Get("/api/v1/localidades/paises/{codigosPais}")]
        Task<List<Pais>> BuscarPaisesAsync(int[] codigosPais, [AliasAs("lang")] Idioma idioma);

        /// <summary>
        /// Obtém o conjunto de países
        /// </summary>
        /// <param name="idioma">Idioma de retorno</param>
        /// <returns>Lista de países</returns>
        [Get("/api/v1/localidades/paises")]
        Task<List<Pais>> BuscarPaisesAsync([AliasAs("lang")] Idioma idioma);

    }

}