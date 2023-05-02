using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rmdev.ibge.localidades
{
    public partial interface IIBGELocalidades
    {
        /// <summary>
        /// Obtém o conjunto de municípios do Brasil
        /// </summary>
        /// <param name="municipios">Identificadores de municipios</param>
        /// <returns>Lista de municípios</returns>
        [Get("/api/v1/localidades/municipios/{municipios}")]
        Task<List<Municipio>> BuscarMunicipiosAsync(params int[] municipios);

        /// <summary>
        /// Obtém um município do Brasil
        /// </summary>
        /// <param name="municipio">Identificador de municipio</param>
        /// <returns>Dados do município</returns>
        [Get("/api/v1/localidades/municipios/{municipio}")]
        Task<Municipio> BuscarMunicipioAsync(int municipio);

        /// <summary>
        /// Obtém um município do Brasil
        /// </summary>
        /// <param name="municipio">Nome do município usando - em vez de espaços, se houver, e remova acentos, aspas e caracteres especiais</param>
        /// <remarks>Prefira a função <see cref="BuscarMunicipioPorNomeAsync"/>, ela tratar o nome do município automaticamente.</remarks>
        /// <returns>Dados do município</returns>
        [Get("/api/v1/localidades/municipios/{municipio}")]
        Task<Municipio> BuscarMunicipioAsync(string municipio);

        /// <summary>
        /// Obtém um município do Brasil
        /// </summary>
        /// <param name="municipio">Nome do município usando</param>
        /// <returns>Dados do município</returns>
        async Task<Municipio> BuscarMunicipioPorNomeAsync(string municipio)
        {
            municipio = municipio.NormalizarNomeCidade();
            return await BuscarMunicipioAsync(municipio);
        }

        /// <summary>
        /// Obtém o conjunto de municípios do Brasil a partir dos identificadores das Unidades da Federação
        /// </summary>
        /// <param name="idUF">Um ou mais identificadores de Unidades da Federação</param>
        /// <returns>Dados do município</returns>
        [Get("/api/v1/localidades/estados/{idUF}/municipios")]
        Task<List<Municipio>> BuscarMunicipioPorUFAsync(params int[] idUF);

        //TODO: Municípios por mesorregião
        //TODO: Municípios por microrregião
        //TODO: Municípios por região imediata
        //TODO: Municípios por região intermediária
        //TODO: Municípios por região

        //TODO: Ver Distritos
        //TODO: Ver Mesorregiões 
        //TODO: Ver Microrregiões 
        //TODO: Ver Regiões imediatas
        //TODO: Ver Regiões integradas de desenvolvimento
        //TODO: Ver Regiões intermediárias
        //TODO: Ver Regiões metropolitanas
        //TODO: Ver Subdistritos

    }
}