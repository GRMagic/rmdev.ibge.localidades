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
        /// <param name="macrorregiao">Identificador de região</param>
        /// <returns>Dados da macrorregião</returns>
        [Get("/api/v1/localidades/regioes/{macrorregiao}")]
        Task<Macrorregiao> BuscarMacrorregiaoAsync(int macrorregiao);

        /// <summary>
        /// Obtém o conjunto de regiões do Brasil
        /// </summary>
        /// <param name="macrorregiao">Identificadores de regiões</param>
        /// <returns>Lista das macrorregiões</returns>
        [Get("/api/v1/localidades/regioes/{macrorregiao}")]
        Task<List<Macrorregiao>> BuscarMacrorregioesAsync(params int[] macrorregiao);

    }
}