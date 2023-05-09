using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace rmdev.ibge.localidades
{
    internal static class JsonHelper
    {
        public static async Task<List<T>> LerComoLista<T>(this HttpResponseMessage responseMessage)
        {
            responseMessage.EnsureSuccessStatusCode();

            var content = await responseMessage.Content.ReadAsStringAsync();
            
            // Vazio como lista vazia
            if (string.IsNullOrWhiteSpace(content))
                return new List<T>();
            
            // Array como lista
            using var stringContent = new StringContent(content);
            if (content.FirstOrDefault() == '[')
                return await stringContent.ReadFromJsonAsync<List<T>>() ?? new List<T>();

            // Objeto como lista de um item
            var retorno = new List<T>();
            var item = await stringContent.ReadFromJsonAsync<T>();
            if (item != null) retorno.Add(item);
            return retorno;
        }
    }
}