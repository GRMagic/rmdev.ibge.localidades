using Refit;
using System.Net.Http;
using System;

namespace rmdev.ibge.localidades
{
    public sealed class IBGEClientFactory
    {
        public const string DefaultBaseURL = "https://servicodados.ibge.gov.br/";

        private readonly RefitSettings RefitSettings = new()
        {
            UrlParameterFormatter = new PipeUrlParameterFormatter()
        };

        public IIBGELocalidades Build(string baseURL = DefaultBaseURL)
        {
#if DEBUG
            var httpClient = new HttpClient(new LoggingHandler(new HttpClientHandler()));
            httpClient.BaseAddress = new Uri(baseURL);
            return RestService.For<IIBGELocalidades>(httpClient, RefitSettings);
#else
            return RestService.For<IIBGELocalidades>(baseURL, RefitSettings);
#endif
        }
    }

}