﻿using Refit;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace rmdev.ibge.localidades
{
    public partial interface IIBGELocalidades
    {

        [Get("/api/v1/localidades/municipios/{codigoMunicipios}")]
        internal Task<HttpResponseMessage> BuscarMunicipiosInternalAsync(params long[] codigoMunicipios);

        /// <summary>
        /// Obtém o conjunto de municípios do Brasil
        /// </summary>
        /// <param name="codigoMunicipios">Identificadores de municipios</param>
        /// <returns>Lista de municípios</returns>
        public async Task<List<Municipio>> BuscarMunicipiosAsync(params long[] codigoMunicipios)
        {
            var response = await BuscarMunicipiosInternalAsync(codigoMunicipios);
            return await response.LerComoLista<Municipio>();
        }

        /// <summary>
        /// Obtém um município do Brasil
        /// </summary>
        /// <param name="codigoMunicipio">Identificador de municipio</param>
        /// <returns>Dados do município</returns>
        public async Task<Municipio> BuscarMunicipioAsync(long codigoMunicipio)
        {
            var municipios = await BuscarMunicipiosAsync(codigoMunicipio);
            return municipios.FirstOrDefault();
        }

        [Get("/api/v1/localidades/municipios/{nomeMunicipio}")]
        internal Task<HttpResponseMessage> BuscarMunicipiosInternalAsync(string nomeMunicipio);

        /// <summary>
        /// Obtém um município do Brasil
        /// </summary>
        /// <param name="nomeMunicipio">Nome do município usando</param>
        /// <returns>Dados do município</returns>
        async Task<Municipio> BuscarMunicipioPorNomeAsync(string nomeMunicipio)
        {
            nomeMunicipio = nomeMunicipio.NormalizarNomeCidade();
            var response = await BuscarMunicipiosInternalAsync(nomeMunicipio);
            var municipios = await response.LerComoLista<Municipio>();
            return municipios.FirstOrDefault();
        }

        /// <summary>
        /// Obtém o conjunto de municípios do Brasil a partir dos identificadores das Unidades da Federação
        /// </summary>
        /// <param name="idUF">Um ou mais identificadores de Unidades da Federação</param>
        /// <returns>Dados do município</returns>
        [Get("/api/v1/localidades/estados/{idUF}/municipios")]
        Task<List<Municipio>> BuscarMunicipioPorUFAsync(params long[] idUF);

        /// <summary>
        /// Obtém o conjunto de municípios do Brasil a partir dos identificadores das mesorregiões
        /// </summary>
        /// <param name="codigoMesorregioes">Um ou mais identificadores de mesorregiões</param>
        /// <returns>Lista de municípios nas regiões selecionadas</returns>
        [Get("/api/v1/localidades/mesorregioes/{codigoMesorregioes}/municipios")]
        Task<List<Municipio>> BuscarMunicipioPorMesorregiaoAsync(params long[] codigoMesorregioes);

        /// <summary>
        /// Obtém o conjunto de municípios do Brasil a partir dos identificadores das microrregiões
        /// </summary>
        /// <param name="codigoMicrorregioes">Um ou mais identificadores de microrregiões</param>
        /// <returns>Lista de municípios nas regiões selecionadas</returns>
        [Get("/api/v1/localidades/microrregioes/{codigoMicrorregioes}/municipios")]
        Task<List<Municipio>> BuscarMunicipioPorMicrorregiaoAsync(params long[] codigoMicrorregioes);

        /// <summary>
        /// Obtém o conjunto de municípios do Brasil a partir dos identificadores das regiões imediatas
        /// </summary>
        /// <param name="codigoRegioesImediatas">Um ou mais identificadores de regiões imediatas</param>
        /// <returns>Lista de municípios nas regiões selecionadas</returns>
        [Get("/api/v1/localidades/regioes-imediatas/{codigoRegioesImediatas}/municipios")]
        Task<List<Municipio>> BuscarMunicipioPorRegiaoImediataAsync(params long[] codigoRegioesImediatas);

        /// <summary>
        /// Obtém o conjunto de municípios do Brasil a partir dos identificadores das regiões intermediárias
        /// </summary>
        /// <param name="codigoRegioesIntermediarias">Um ou mais identificadores de regiões intermediárias</param>
        /// <returns>Lista de municípios nas regiões selecionadas</returns>
        [Get("/api/v1/localidades/regioes-intermediarias/{codigoRegioesIntermediarias}/municipios")]
        Task<List<Municipio>> BuscarMunicipioPorRegiaoIntermediariaAsync(params long[] codigoRegioesIntermediarias);

        /// <summary>
        /// Obtém o conjunto de municípios do Brasil a partir dos identificadores das regiões
        /// </summary>
        /// <param name="codigoMacrorregioes">Um ou mais identificadores de regiões</param>
        /// <returns>Lista de municípios nas regiões selecionadas</returns>
        [Get("/api/v1/localidades/regioes/{codigoMacrorregioes}/municipios")]
        Task<List<Municipio>> BuscarMunicipioPorMacrorregiaoAsync(params long[] codigoMacrorregioes);
    }
}