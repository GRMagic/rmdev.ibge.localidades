using Refit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rmdev.ibge.localidades
{
    public partial interface IIBGELocalidades
    {
        //TODO: Ver Distritos

        /// <summary>
        /// Obtém o conjunto de distritos do Brasil a partir dos respectivos identificadores
        /// </summary>
        /// <param name="codigoDistritos">Um ou mais identificadores de distrito</param>
        /// <returns>Lista de distritos</returns>
        [Get("/api/v1/localidades/distritos/{codigoDistritos}")]
        Task<List<Distrito>> BuscarDistritosAsync(params int[] codigoDistritos);

        /// <summary>
        /// Obtém o conjunto de distritos do Brasil a partir dos identificadores das Unidades da Federação
        /// </summary>
        /// <param name="codigoUFs">Um ou mais identificadores de Unidades da Federação</param>
        /// <returns>Lista de distritos</returns>
        [Get("/api/v1/localidades/estados/{codigoUFs}/distritos")]
        Task<List<Distrito>> BuscarDistritosPorUFAsync(params int[] codigoUFs);

        /// <summary>
        /// Obtém o conjunto de distritos do Brasil a partir dos identificadores das mesorregiões
        /// </summary>
        /// <param name="codigoMesorregioes">Um ou mais identificadores de mesorregiões</param>
        /// <returns>Lista de distritos</returns>
        [Get("/api/v1/localidades/mesorregioes/{codigoMesorregioes}/distritos")]
        Task<List<Distrito>> BuscarDistritosPorMesorregiaoAsync(params int[] codigoMesorregioes);

        /// <summary>
        /// Obtém o conjunto de distritos do Brasil a partir dos identificadores das microrregiões
        /// </summary>
        /// <param name="codigoMicrorregioes">Um ou mais identificadores de microrregiões</param>
        /// <returns>Lista de distritos</returns>
        [Get("/api/v1/localidades/microrregioes/{codigoMicrorregioes}/distritos")]
        Task<List<Distrito>> BuscarDistritosPorMicrorregiaoAsync(params int[] codigoMicrorregioes);

        /// <summary>
        /// Obtém o conjunto de distritos do Brasil a partir dos identificadores dos municípios
        /// </summary>
        /// <param name="codigoMunicipios">Um ou mais identificadores de municipios</param>
        /// <returns>Lista de distritos</returns>
        [Get("/api/v1/localidades/municipios/{codigoMunicipios}/distritos")]
        Task<List<Distrito>> BuscarDistritosPorMunicipioAsync(params int[] codigoMunicipios);

        /// <summary>
        /// Obtém o conjunto de distritos do Brasil a partir dos identificadores das regiões imediatas
        /// </summary>
        /// <param name="codigoRegiaoImediata">Um ou mais identificadores de regiões imediatas</param>
        /// <returns>Lista de distritos</returns>
        [Get("/api/v1/localidades/regioes-imediatas/{codigoRegiaoImediata}/distritos")]
        Task<List<Distrito>> BuscarDistritosPorRegiaoImediataAsync(params int[] codigoRegiaoImediata);

        /// <summary>
        /// Obtém o conjunto de distritos do Brasil a partir dos identificadores das regiões intermediárias
        /// </summary>
        /// <param name="codigoRegiaoIntermediaria">Um ou mais identificadores de regiões intermediárias</param>
        /// <returns>Lista de distritos</returns>
        [Get("/api/v1/localidades/regioes-intermediarias/{codigoRegiaoIntermediaria}/distritos")]
        Task<List<Distrito>> BuscarDistritosPorRegiaoIntermediariaAsync(params int[] codigoRegiaoIntermediaria);

        /// <summary>
        /// Obtém o conjunto de distritos do Brasil a partir dos identificadores das regiões
        /// </summary>
        /// <param name="codigoMacrorregiao">Um ou mais identificadores de regiões</param>
        /// <returns>Lista de distritos</returns>
        [Get("/api/v1/localidades/regioes/{codigoMacrorregiao}/distritos")]
        Task<List<Distrito>> BuscarDistritosPorMacrorregiaoAsync(params int[] codigoMacrorregiao);

        /// <summary>
        /// Obtém um único distritos do Brasil a partir do identificador
        /// </summary>
        /// <param name="codigoDistrito">Identificador de distrito</param>
        /// <returns>Dados do distrito</returns>
        public async Task<Distrito?> BuscarDistritoAsync(int codigoDistrito)
        {
            var distritos = await BuscarDistritosAsync(codigoDistrito);
            return distritos.FirstOrDefault();
        }

        
        //TODO: Ver Mesorregiões 
        //TODO: Ver Microrregiões 
        //TODO: Ver Regiões imediatas
        //TODO: Ver Regiões integradas de desenvolvimento
        //TODO: Ver Regiões intermediárias
        //TODO: Ver Regiões metropolitanas
        //TODO: Ver Subdistritos
    }
}