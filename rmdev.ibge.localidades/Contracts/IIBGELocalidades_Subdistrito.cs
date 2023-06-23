using Refit;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace rmdev.ibge.localidades
{
    public partial interface IIBGELocalidades
    {
        [Get("/api/v1/localidades/subdistritos/{codigoSubdistritos}")]
        internal Task<HttpResponseMessage> BuscarSubdistritosInternalAsync(params long[] codigoSubdistritos);

        /// <summary>
        /// Obtém o conjunto de subdistritos do Brasil a partir dos respectivos identificadores
        /// </summary>
        /// <param name="codigoSubdistritos">Um ou mais identificadores de subdistrito</param>
        /// <returns>Lista de subdistritos</returns>
        public async Task<List<Subdistrito>> BuscarSubdistritosAsync(params long[] codigoSubdistritos)
        {
            var response = await BuscarSubdistritosInternalAsync(codigoSubdistritos);
            return await response.LerComoLista<Subdistrito>();
        }

        /// <summary>
        /// Obtém o conjunto de subdistritos do Brasil a partir dos identificadores dos distritos
        /// </summary>
        /// <param name="codigoDistritos">Um ou mais identificadores de distritos</param>
        /// <returns>Lista de subdistritos</returns>
        [Get("/api/v1/localidades/distritos/{codigoDistritos}/subdistritos")]
        Task<List<Subdistrito>> BuscarSubdistritosPorDistritoAsync(params long[] codigoDistritos);

        /// <summary>
        /// Obtém o conjunto de subdistritos do Brasil a partir dos identificadores das Unidades da Federação
        /// </summary>
        /// <param name="codigoUFs">Um ou mais identificadores de Unidades da Federação</param>
        /// <returns>Lista de subdistritos</returns>
        [Get("/api/v1/localidades/estados/{codigoUFs}/subdistritos")]
        Task<List<Subdistrito>> BuscarSubdistritosPorUFAsync(params long[] codigoUFs);

        /// <summary>
        /// Obtém o conjunto de subdistritos do Brasil a partir dos identificadores das mesorregiões
        /// </summary>
        /// <param name="codigoMesorregioes">Um ou mais identificadores de mesorregiões</param>
        /// <returns>Lista de subdistritos</returns>
        [Get("/api/v1/localidades/mesorregioes/{codigoMesorregioes}/subdistritos")]
        Task<List<Subdistrito>> BuscarSubdistritosPorMesorregiaoAsync(params long[] codigoMesorregioes);

        /// <summary>
        /// Obtém o conjunto de subdistritos do Brasil a partir dos identificadores das microrregiões
        /// </summary>
        /// <param name="codigoMicrorregioes">Um ou mais identificadores de microrregiões</param>
        /// <returns>Lista de subdistritos</returns>
        [Get("/api/v1/localidades/microrregioes/{codigoMicrorregioes}/subdistritos")]
        Task<List<Subdistrito>> BuscarSubdistritosPorMicrorregiaoAsync(params long[] codigoMicrorregioes);

        /// <summary>
        /// Obtém o conjunto de subdistritos do Brasil a partir dos identificadores dos municípios
        /// </summary>
        /// <param name="codigoMunicipios">Um ou mais identificadores de municipios</param>
        /// <returns>Lista de subdistritos</returns>
        [Get("/api/v1/localidades/municipios/{codigoMunicipios}/subdistritos")]
        Task<List<Subdistrito>> BuscarSubdistritosPorMunicipioAsync(params long[] codigoMunicipios);

        /// <summary>
        /// Obtém o conjunto de subdistritos do Brasil a partir dos identificadores das regiões imediatas
        /// </summary>
        /// <param name="codigoRegiaoImediata">Um ou mais identificadores de regiões imediatas</param>
        /// <returns>Lista de subdistritos</returns>
        [Get("/api/v1/localidades/regioes-imediatas/{codigoRegiaoImediata}/subdistritos")]
        Task<List<Subdistrito>> BuscarSubdistritosPorRegiaoImediataAsync(params long[] codigoRegiaoImediata);

        /// <summary>
        /// Obtém o conjunto de subdistritos do Brasil a partir dos identificadores das regiões
        /// </summary>
        /// <param name="codigoMacrorregiao">Um ou mais identificadores de regiões</param>
        /// <returns>Lista de subdistritos</returns>
        [Get("/api/v1/localidades/regioes/{codigoMacrorregiao}/subdistritos")]
        Task<List<Subdistrito>> BuscarSubdistritosPorMacrorregiaoAsync(params long[] codigoMacrorregiao);


        /// <summary>
        /// Obtém um único subdistrito do Brasil a partir do identificador
        /// </summary>
        /// <param name="codigoSubdistrito">Identificador de subdistrito</param>
        /// <returns>Dados do subdistrito</returns>
        public async Task<Subdistrito?> BuscarSubdistritoAsync(long codigoSubdistrito)
        {
            var distritos = await BuscarSubdistritosAsync(codigoSubdistrito);
            return distritos.FirstOrDefault();
        }
    }
}