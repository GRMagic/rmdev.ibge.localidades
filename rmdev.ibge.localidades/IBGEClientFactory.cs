using Refit;

namespace rmdev.ibge.localidades
{
    public sealed class IBGEClientFactory
    {
        public const string DefaultBaseURL = "https://servicodados.ibge.gov.br/";

        private readonly RefitSettings RefitSettings = new RefitSettings()
        {
            UrlParameterFormatter = new PipeUrlParameterFormatter()
        };

        public IIBGELocalidades Build(string baseURL = DefaultBaseURL)
        {
            return RestService.For<IIBGELocalidades>(baseURL, RefitSettings);
        }
    }

}