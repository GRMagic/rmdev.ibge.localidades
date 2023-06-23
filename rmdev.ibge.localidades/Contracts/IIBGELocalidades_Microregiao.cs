using Refit;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace rmdev.ibge.localidades
{
    public partial interface IIBGELocalidades
    {

        [Get("/api/v1/localidades/microrregioes/{codigoMicroregioes}")]
        internal Task<HttpResponseMessage> BuscarMicroregioesInternalAsync(params long[] codigoMicroregioes);

        /// <summary>
        /// Obtém o conjunto de microregiões do Brasil
        /// </summary>
        /// <param name="codigoMicroregioes">Identificadores de microregiões</param>
        /// <returns>Lista de microregiões</returns>
        public async Task<List<Microrregiao>> BuscarMicroregioesAsync(params long[] codigoMicroregioes)
        {
            var response = await BuscarMicroregioesInternalAsync(codigoMicroregioes);
            return await response.LerComoLista<Microrregiao>();
        }

        /// <summary>
        /// Obtém uma microregião do Brasil
        /// </summary>
        /// <param name="codigoMicroregiao">Identificador de microregião</param>
        /// <returns>Dados da microregião</returns>
        public async Task<Microrregiao> BuscarMicroregiaoAsync(long codigoMicroregiao)
        {
            var microregioes = await BuscarMicroregioesAsync(codigoMicroregiao);
            return microregioes.FirstOrDefault();
        }

        /// <summary>
        /// Obtém o conjunto de microrregiões do Brasil a partir dos identificadores das Unidades da Federação
        /// </summary>
        /// <param name="idUF">Um ou mais identificadores de Unidades da Federação</param>
        /// <returns>Dados da microregião</returns>
        [Get("/api/v1/localidades/estados/{idUF}/microrregioes")]
        Task<List<Microrregiao>> BuscarMicroregiaoPorUFAsync(params long[] idUF);

        /// <summary>
        /// Obtém o conjunto de microregiões do Brasil a partir dos identificadores das mesorregiões
        /// </summary>
        /// <param name="codigoMesorregioes">Um ou mais identificadores de mesorregiões</param>
        /// <returns>Lista de microregiões nas mesoregiões selecionadas</returns>
        [Get("/api/v1/localidades/mesorregioes/{codigoMesorregioes}/microrregioes")]
        Task<List<Microrregiao>> BuscarMicroregiaoPorMesorregiaoAsync(params long[] codigoMesorregioes);

        /// <summary>
        /// Obtém o conjunto de microregiões do Brasil a partir dos identificadores das regiões
        /// </summary>
        /// <param name="codigoMacrorregioes">Um ou mais identificadores de regiões</param>
        /// <returns>Lista de microregiões nas regiões selecionadas</returns>
        [Get("/api/v1/localidades/regioes/{codigoMacrorregioes}/microrregioes")]
        Task<List<Microrregiao>> BuscarMicroregiaoPorMacrorregiaoAsync(params long[] codigoMacrorregioes);

    }
}