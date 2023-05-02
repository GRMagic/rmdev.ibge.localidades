using Refit;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace rmdev.ibge.localidades
{
    public class IBGEClientFactory
    {
        public const string BaseURL = "https://servicodados.ibge.gov.br/";

        public readonly RefitSettings RefitSettings = new()
        {
            UrlParameterFormatter = new PipeUrlParameterFormatter()
        };

        public IIBGE Build()
        {
#if DEBUG
            var httpClient = new HttpClient(new LoggingHandler(new HttpClientHandler()));
            httpClient.BaseAddress = new Uri(BaseURL);
            return RestService.For<IIBGE>(httpClient, RefitSettings);
#else
            return RestService.For<IIBGE>(BaseURL, RefitSettings);
#endif
        }
    }

#if DEBUG
    public class LoggingHandler : DelegatingHandler
    {
        public LoggingHandler(HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Request:");
            Console.WriteLine(request.ToString());
            if (request.Content != null)
            {
                Console.WriteLine(await request.Content.ReadAsStringAsync());
            }
            Console.WriteLine();

            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            Console.WriteLine("Response:");
            Console.WriteLine(response.ToString());
            if (response.Content != null)
            {
                var body = await response.Content.ReadAsStringAsync();
                Console.WriteLine(body);
            }
            Console.WriteLine();

            return response;
        }
    }
#endif
}