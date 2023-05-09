namespace rmdev.ibge.localidades.tests
{
    public class MesorregiaoTests
    {
        private readonly IIBGELocalidades _api;
        public MesorregiaoTests() => _api = new IBGEClientFactory().Build("http://servicodados.ibge.gov.br/");

		[Fact(DisplayName = "Buscar todas as mesoregiões")]
		[Trait("Categoria", "Mesorregiões")]
		public async Task BuscarMesorregioes_TodasMesorregioes()
		{
            // Arrange

            // Act
            var regioes = await _api.BuscarMesorregioesAsync();

            // Assert
            Assert.True(regioes.Count() > 4);
        }

        [Fact(DisplayName = "Buscar algumas mesoregiões")]
        [Trait("Categoria", "Mesorregiões")]
        public async Task DoisCodigosValidos_BuscarMesorregioes_DuasMesorregioes()
        {
            // Arrange

            // Act
            var regioes = await _api.BuscarMesorregioesAsync(3302, 3509);

            // Assert
            Assert.True(regioes.Count() == 2);
        }

        [Fact(DisplayName = "Buscar uma mesoregião")]
        [Trait("Categoria", "Mesorregiões")]
        public async Task CodigoValido_BuscarMesorregioes_DadosMesorregiao()
        {
            // Arrange

            // Act
            var regiao = await _api.BuscarMesorregiaoAsync(3509);

            // Assert
            Assert.Equivalent(new
            {
                Id = 3509L,
                Nome = "Marília",
                UF = new
                {
                    Id = 35L
                }
            }, regiao);
        }

        [Fact(DisplayName = "Buscar duas mesoregiões, sendo que uma não existe")]
        [Trait("Categoria", "Mesorregiões")]
        public async Task UmCodigoValidoUmCodigoInvalido_BuscarMesorregioes_DadosUmaMesorregiao()
        {
            // Arrange

            // Act
            var regiao = await _api.BuscarMesorregioesAsync(3509, 3599);

            // Assert
            Assert.Single(regiao);
        }

        [Fact(DisplayName = "Buscar uma mesoregiões que não existe")]
        [Trait("Categoria", "Mesorregiões")]
        public async Task UmCodigoInvalido_BuscarMesorregioes_Null()
        {
            // Arrange

            // Act
            var regiao = await _api.BuscarMesorregiaoAsync(3599);

            // Assert
            Assert.Null(regiao);
        }
    }
}