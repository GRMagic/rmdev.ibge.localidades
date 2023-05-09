using Refit;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace rmdev.ibge.localidades
{
    public partial interface IIBGELocalidades
    {

        /// <summary>
        /// Obtém uma região do Brasil
        /// </summary>
        /// <param name="codigoMacrorregiao">Identificador da região</param>
        /// <returns>Dados da macrorregião</returns>
        public async Task<Macrorregiao> BuscarMacrorregiaoAsync(long codigoMacrorregiao)
        {
            var regioes = await BuscarMacrorregioesAsync(codigoMacrorregiao);
            return regioes.FirstOrDefault();
        }

        [Get("/api/v1/localidades/regioes/{codigoMacrorregioes}")]
        internal Task<HttpResponseMessage> BuscarMacrorregioesInternalAsync(params long[] codigoMacrorregioes);

        /// <summary>
        /// Obtém o conjunto de regiões do Brasil
        /// </summary>
        /// <param name="codigoMacrorregioes">Identificadores de regiões</param>
        /// <returns>Lista das macrorregiões</returns>
        public async Task<List<Macrorregiao>> BuscarMacrorregioesAsync(params long[] codigoMacrorregioes)
        {
            var response = await BuscarMacrorregioesInternalAsync(codigoMacrorregioes);
            return await response.LerComoLista<Macrorregiao>();
        }

    }
}