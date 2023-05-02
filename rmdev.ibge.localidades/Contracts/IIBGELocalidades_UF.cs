using Refit;
using System.Collections.Generic;
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
        [Get("/api/v1/localidades/estados/{codigoUF}")]
        Task<UF> BuscarUFAsync(int codigoUF);

        /// <summary>
        /// Obtém o conjunto de Unidades da Federação do Brasil
        /// </summary>
        /// <param name="codigosUF">Identificadores de Unidades da Federação</param>
        /// <returns>Lista de Unidades da Federação</returns>
        [Get("/api/v1/localidades/estados/{codigosUF}")]
        Task<List<UF>> BuscarUFsAsync(params int[] codigosUF);

        /// <summary>
        /// Obtém o conjunto de Unidades da Federação do Brasil
        /// </summary>
        /// <param name="macrorregiao">Um ou mais identificadores de regiões</param>
        /// <returns>Lista de Unidades da Federação</returns>
        [Get("/api/v1/localidades/regioes/{macrorregiao}/estados")]
        Task<List<UF>> BuscarUFsPorMacrorregiaoAsync(params int[] macrorregiao);

    }
}