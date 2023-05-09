﻿using Refit;
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
        public async Task<Mesorregiao?> BuscarMesorregiaoAsync(long codigoMesorregiao)
        {
            var regioes = await BuscarMesorregioesAsync(codigoMesorregiao);
            return regioes.FirstOrDefault();
        }

        // TODO: Continuar aqui!
    }
}