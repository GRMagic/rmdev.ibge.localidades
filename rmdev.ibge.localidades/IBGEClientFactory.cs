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
            return RestService.For<IIBGELocalidades>(baseURL, RefitSettings);
        }
    }

}