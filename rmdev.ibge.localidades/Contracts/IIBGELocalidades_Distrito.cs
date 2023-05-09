using Refit;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace rmdev.ibge.localidades
{
    public partial interface IIBGELocalidades
    {
        [Get("/api/v1/localidades/distritos/{codigoDistritos}")]
        internal Task<HttpResponseMessage> BuscarDistritosInternalAsync(params long[] codigoDistritos);

        /// <summary>
        /// Obtém o conjunto de distritos do Brasil a partir dos respectivos identificadores
        /// </summary>
        /// <param name="codigoDistritos">Um ou mais identificadores de distrito</param>
        /// <returns>Lista de distritos</returns>
        public async Task<List<Distrito>> BuscarDistritosAsync(params long[] codigoDistritos)
        {
            var response = await BuscarDistritosInternalAsync(codigoDistritos);
            return await response.LerComoLista<Distrito>();
        }

        /// <summary>
        /// Obtém o conjunto de distritos do Brasil a partir dos identificadores das Unidades da Federação
        /// </summary>
        /// <param name="codigoUFs">Um ou mais identificadores de Unidades da Federação</param>
        /// <returns>Lista de distritos</returns>
        [Get("/api/v1/localidades/estados/{codigoUFs}/distritos")]
        Task<List<Distrito>> BuscarDistritosPorUFAsync(params long[] codigoUFs);

        /// <summary>
        /// Obtém o conjunto de distritos do Brasil a partir dos identificadores das mesorregiões
        /// </summary>
        /// <param name="codigoMesorregioes">Um ou mais identificadores de mesorregiões</param>
        /// <returns>Lista de distritos</returns>
        [Get("/api/v1/localidades/mesorregioes/{codigoMesorregioes}/distritos")]
        Task<List<Distrito>> BuscarDistritosPorMesorregiaoAsync(params long[] codigoMesorregioes);

        /// <summary>
        /// Obtém o conjunto de distritos do Brasil a partir dos identificadores das microrregiões
        /// </summary>
        /// <param name="codigoMicrorregioes">Um ou mais identificadores de microrregiões</param>
        /// <returns>Lista de distritos</returns>
        [Get("/api/v1/localidades/microrregioes/{codigoMicrorregioes}/distritos")]
        Task<List<Distrito>> BuscarDistritosPorMicrorregiaoAsync(params long[] codigoMicrorregioes);

        /// <summary>
        /// Obtém o conjunto de distritos do Brasil a partir dos identificadores dos municípios
        /// </summary>
        /// <param name="codigoMunicipios">Um ou mais identificadores de municipios</param>
        /// <returns>Lista de distritos</returns>
        [Get("/api/v1/localidades/municipios/{codigoMunicipios}/distritos")]
        Task<List<Distrito>> BuscarDistritosPorMunicipioAsync(params long[] codigoMunicipios);

        /// <summary>
        /// Obtém o conjunto de distritos do Brasil a partir dos identificadores das regiões imediatas
        /// </summary>
        /// <param name="codigoRegiaoImediata">Um ou mais identificadores de regiões imediatas</param>
        /// <returns>Lista de distritos</returns>
        [Get("/api/v1/localidades/regioes-imediatas/{codigoRegiaoImediata}/distritos")]
        Task<List<Distrito>> BuscarDistritosPorRegiaoImediataAsync(params long[] codigoRegiaoImediata);

        /// <summary>
        /// Obtém o conjunto de distritos do Brasil a partir dos identificadores das regiões intermediárias
        /// </summary>
        /// <param name="codigoRegiaoIntermediaria">Um ou mais identificadores de regiões intermediárias</param>
        /// <returns>Lista de distritos</returns>
        [Get("/api/v1/localidades/regioes-intermediarias/{codigoRegiaoIntermediaria}/distritos")]
        Task<List<Distrito>> BuscarDistritosPorRegiaoIntermediariaAsync(params long[] codigoRegiaoIntermediaria);

        /// <summary>
        /// Obtém o conjunto de distritos do Brasil a partir dos identificadores das regiões
        /// </summary>
        /// <param name="codigoMacrorregiao">Um ou mais identificadores de regiões</param>
        /// <returns>Lista de distritos</returns>
        [Get("/api/v1/localidades/regioes/{codigoMacrorregiao}/distritos")]
        Task<List<Distrito>> BuscarDistritosPorMacrorregiaoAsync(params long[] codigoMacrorregiao);

        /// <summary>
        /// Obtém um único distrito do Brasil a partir do identificador
        /// </summary>
        /// <param name="codigoDistrito">Identificador de distrito</param>
        /// <returns>Dados do distrito</returns>
        public async Task<Distrito?> BuscarDistritoAsync(long codigoDistrito)
        {
            var distritos = await BuscarDistritosAsync(codigoDistrito);
            return distritos.FirstOrDefault();
        }

        //TODO: Ler documentação e implementar consultas por microrregiões 
        //TODO: Ler documentação e implementar consultas por regiões imediatas
        //TODO: Ler documentação e implementar consultas por regiões integradas de desenvolvimento
        //TODO: Ler documentação e implementar consultas por regiões intermediárias
        //TODO: Ler documentação e implementar consultas por regiões metropolitanas
        //TODO: Ler documentação e implementar consultas por subdistritos
    }
}