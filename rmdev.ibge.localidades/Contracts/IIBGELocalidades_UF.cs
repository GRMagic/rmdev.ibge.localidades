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
        /// Obtém dados de uma Unidade da Federação do Brasil
        /// </summary>
        /// <param name="codigoUF">Identificador da Unidade da Federação</param>
        /// <returns>Dados da Unidade da Federação</returns>
        public async Task<UF> BuscarUFAsync(long codigoUF)
        {
            var ufs = await BuscarUFsAsync(codigoUF);
            return ufs.FirstOrDefault();
        }

        [Get("/api/v1/localidades/estados/{codigoUFs}")]
        internal Task<HttpResponseMessage> BuscarUFsInternalAsync(params long[] codigoUFs);

        /// <summary>
        /// Obtém o conjunto de Unidades da Federação do Brasil
        /// </summary>
        /// <param name="codigoUFs">Identificadores de Unidades da Federação</param>
        /// <returns>Lista de Unidades da Federação</returns>
        public async Task<List<UF>> BuscarUFsAsync(params long[] codigoUFs)
        {
            var response = await BuscarUFsInternalAsync(codigoUFs);
            return await response.LerComoLista<UF>();
        }

        /// <summary>
        /// Obtém o conjunto de Unidades da Federação do Brasil
        /// </summary>
        /// <param name="codigoMacrorregiao">Um ou mais identificadores de regiões</param>
        /// <returns>Lista de Unidades da Federação</returns>
        [Get("/api/v1/localidades/regioes/{codigoMacrorregiao}/estados")]
        Task<List<UF>> BuscarUFsPorMacrorregiaoAsync(params long[] codigoMacrorregiao);

    }
}