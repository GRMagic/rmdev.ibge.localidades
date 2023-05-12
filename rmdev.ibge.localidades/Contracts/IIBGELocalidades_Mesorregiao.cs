using Refit;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace rmdev.ibge.localidades
{
    public partial interface IIBGELocalidades
    {
        [Get("/api/v1/localidades/mesorregioes/{codigoMesorregioes}")]
        internal Task<HttpResponseMessage> BuscarMesorregioesInternalAsync(params long[] codigoMesorregioes);

        /// <summary>
        /// Obtém o conjunto de mesorregiões do Brasil a partir dos respectivos identificadores
        /// </summary>
        /// <param name="codigoMesorregioes">Um ou mais identificadores de mesorregiões</param>
        /// <returns>Lista de mesorregiões</returns>
        async Task<List<Mesorregiao>> BuscarMesorregioesAsync(params long[] codigoMesorregioes)
        {
            var response = await BuscarMesorregioesInternalAsync(codigoMesorregioes);
            return await response.LerComoLista<Mesorregiao>();
        }

        /// <summary>
        /// Obtém uma mesorregião do Brasil a partir do identificador
        /// </summary>
        /// <param name="codigoMesorregiao">Um identificador de mesorregião</param>
        /// <returns>Dados da mesorregião</returns>
        async Task<Mesorregiao?> BuscarMesorregiaoAsync(long codigoMesorregiao)
        {
            var regioes = await BuscarMesorregioesAsync(codigoMesorregiao);
            return regioes.FirstOrDefault();
        }

        /// <summary>
        /// Obtém o conjunto de mesorregiões do Brasil a partir dos identificadores das Unidades da Federação
        /// </summary>
        /// <param name="codigoUFs">Um ou mais identificadores de Unidades da Federação</param>
        /// <returns>Lista de mesorregiões</returns>
        [Get("/api/v1/localidades/estados/{codigoUFs}/mesorregioes")]
        Task<List<Mesorregiao>> BuscarMesorregioesPorUFAsync(params long[] codigoUFs);

        /// <summary>
        /// Obtém o conjunto de mesorregiões do Brasil a partir dos identificadores das regiões
        /// </summary>
        /// <param name="codigoMacrorregioes">Um ou mais identificadores de regiões</param>
        /// <returns>Lista de mesorregiões</returns>
        [Get("/api/v1/localidades/regioes/{codigoMacrorregioes}/mesorregioes")]
        Task<List<Mesorregiao>> BuscarMesorregioesPorMacrorregiaoAsync(params long[] codigoMacrorregioes);
    }
}