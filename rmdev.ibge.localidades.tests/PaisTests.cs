namespace rmdev.ibge.localidades.tests
{
    public class PaisTests
    {
        private readonly IIBGELocalidades _api;
        public PaisTests() => _api = new IBGEClientFactory().Build("http://servicodados.ibge.gov.br/");

        [Fact(DisplayName = "Buscar dados de um pa�s")]
        [Trait("Categoria", "Paises")]
        public async Task CodigoPaisValido_BuscarPaises_DadosPais()
        {
            // Arrange

            // Act
            var paises = await _api.BuscarPaisesAsync(76);

            // Assert
            Assert.Equivalent(new Pais 
            {
                Id = new ()
                {
                    M49 = 76,
                    IsoAlfa2 = "BR",
                    IsoAlfa3 = "BRA"
                },
                Nome = "Brasil",
                RegiaoIntermediaria = new ()
                {
                    Id = new() { M49 = 5 },
                    Nome = "Am�rica do sul"
                },
                SubRegiao = new ()
                {
                    Id = new () { M49 = 419 },
                    Nome = "Am�rica Latina e Caribe",
                    Regiao = new()
                    {
                        Id = new() { M49= 19 },
                        Nome = "Am�rica"
                    }
                }
            }, paises.First());
        }

        [Fact(DisplayName = "Buscar um pa�s")]
		[Trait("Categoria", "Pa�ses")]
		public async Task CodigoPaisValido_BuscarPaises_ApenasUmPais()
		{
            // Arrange

            // Act
            var paises = await _api.BuscarPaisesAsync(76);

            // Assert
            Assert.Single(paises);
        }

        [Fact(DisplayName = "Buscar um pa�s")]
        [Trait("Categoria", "Pa�ses")]
        public async Task CodigoPaisValido_BuscarUnicoPais_PaisSolicitado()
        {
            // Arrange

            // Act
            var pais = await _api.BuscarPaisAsync(76);

            // Assert
            Assert.NotNull(pais);
        }

        [Fact(DisplayName = "Buscar todos os pa�ses em ingl�s")]
        [Trait("Categoria", "Pa�ses")]
        public async Task IdiomaIngles_BuscarPaises_DadosEmIngles()
        {
            // Arrange

            // Act
            var paises = await _api.BuscarPaisesAsync(Idioma.EN);

            // Assert
            Assert.Contains(paises, p => p.Nome == "Brazil");
        }

        [Fact(DisplayName = "Buscar um pa�s em ingl�s")]
        [Trait("Categoria", "Paises")]
        public async Task CodigoPaisIdiomaIngles_BuscarPaises_DadosEmIngles()
        {
            // Arrange

            // Act
            var paises = await _api.BuscarPaisesAsync(76, Idioma.EN);

            // Assert
            var pais = paises.First();
            Assert.Equal("Brazil", pais.Nome);
        }

        [Fact(DisplayName = "Buscar um pais em espanhol")]
        [Trait("Categoria", "Paises")]
        public async Task CodigoPaisIdiomaEspanhol_BuscarPaises_DadosEmEspanhol()
        {
            // Arrange

            // Act
            var paises = await _api.BuscarPaisesAsync(76, Idioma.ES);

            // Assert
            var pais = paises.First();
            Assert.Equal("Am�rica del Sur", pais.RegiaoIntermediaria.Nome);
        }

        [Fact(DisplayName = "Buscar um pais em portugu�s")]
        [Trait("Categoria", "Paises")]
        public async Task CodigoPaisIdiomaPortugues_BuscarPaises_DadosEmPortugues()
        {
            // Arrange

            // Act
            var paises = await _api.BuscarPaisesAsync(76, Idioma.PT);

            // Assert
            var pais = paises.First();
            Assert.Equal("Am�rica do sul", pais.RegiaoIntermediaria!.Nome);
        }

        [Fact(DisplayName = "Buscar dois paises")]
		[Trait("Categoria", "Paises")]
		public async Task CodigosPaisesValidos_BuscarPaises_VariosPais()
		{
            // Arrange

            // Act
            var paises = await _api.BuscarPaisesAsync(76, 4);

            // Assert
            Assert.Equal(2, paises.Count());
        }


        [Fact(DisplayName = "Buscar todos os paises")]
        [Trait("Categoria", "Paises")]
        public async Task BuscarPaises_TodosPaises()
        {
            // Arrange

            // Act
            var paises = await _api.BuscarPaisesAsync();

            // Assert
            Assert.True(paises.Count() > 150);
        }


    }
}