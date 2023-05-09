using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace rmdev.ibge.localidades
{
    public partial interface IIBGELocalidades
    {

        /// <summary>
        /// Obtém uma região do Brasil
        /// </summary>
        /// <param name="codigoMacrorregiao">Identificador de região</param>
        /// <returns>Dados da macrorregião</returns>
        [Get("/api/v1/localidades/regioes/{codigoMacrorregiao}")]
        Task<Macrorregiao> BuscarMacrorregiaoAsync(long codigoMacrorregiao);

        /// <summary>
        /// Obtém o conjunto de regiões do Brasil
        /// </summary>
        /// <param name="codigoMacrorregioes">Identificadores de regiões</param>
        /// <returns>Lista das macrorregiões</returns>
        [Get("/api/v1/localidades/regioes/{codigoMacrorregioes}")]
        Task<List<Macrorregiao>> BuscarMacrorregioesAsync(params long[] codigoMacrorregioes);

    }
}