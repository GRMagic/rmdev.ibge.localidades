namespace rmdev.ibge.localidades.tests
{
    public class PaisTests
    {
        [Fact(DisplayName = "Buscar dados de um pais")]
        [Trait("Categoria", "Pais")]
        public async Task CodigoPaisValido_BuscarPaises_DadosPais()
        {
            // Arrange
            var api = new IBGEClientFactory().Build();

            // Act
            var paises = await api.BuscarPaisesAsync(76);

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
                    Nome = "América do sul"
                },
                SubRegiao = new ()
                {
                    Id = new () { M49 = 419 },
                    Nome = "América Latina e Caribe",
                    Regiao = new()
                    {
                        Id = new() { M49= 19 },
                        Nome = "América"
                    }
                }
            }, paises.First());
        }

        [Fact(DisplayName = "Buscar um pais")]
		[Trait("Categoria", "Pais")]
		public async Task CodigoPaisValido_BuscarPaises_ApenasUmPais()
		{
            // Arrange
            var api = new IBGEClientFactory().Build();

            // Act
            var paises = await api.BuscarPaisesAsync(76);

            // Assert
            Assert.Single(paises);
        }

        [Fact(DisplayName = "Buscar um pais")]
        [Trait("Categoria", "Pais")]
        public async Task CodigoPaisValido_BuscarUnicoPais_PaisSolicitado()
        {
            // Arrange
            var api = new IBGEClientFactory().Build();

            // Act
            var pais = await api.BuscarPaisAsync(76);

            // Assert
            Assert.NotNull(pais);
        }

        [Fact(DisplayName = "Buscar um pais em inglês")]
        [Trait("Categoria", "Pais")]
        public async Task CodigoPaisIdiomaIngles_BuscarPaises_DadosEmIngles()
        {
            // Arrange
            var api = new IBGEClientFactory().Build();

            // Act
            var paises = await api.BuscarPaisesAsync(76, Idioma.EN);

            // Assert
            var pais = paises.First();
            Assert.Equal("Brazil", pais.Nome);
        }

        [Fact(DisplayName = "Buscar um pais em espanhol")]
        [Trait("Categoria", "Pais")]
        public async Task CodigoPaisIdiomaEspanhol_BuscarPaises_DadosEmEspanhol()
        {
            // Arrange
            var api = new IBGEClientFactory().Build();

            // Act
            var paises = await api.BuscarPaisesAsync(76, Idioma.ES);

            // Assert
            var pais = paises.First();
            Assert.Equal("América del Sur", pais.RegiaoIntermediaria.Nome);
        }

        [Fact(DisplayName = "Buscar um pais em português")]
        [Trait("Categoria", "Pais")]
        public async Task CodigoPaisIdiomaPortugues_BuscarPaises_DadosEmPortugues()
        {
            // Arrange
            var api = new IBGEClientFactory().Build();

            // Act
            var paises = await api.BuscarPaisesAsync(76, Idioma.PT);

            // Assert
            var pais = paises.First();
            Assert.Equal("América do sul", pais.RegiaoIntermediaria!.Nome);
        }

        [Fact(DisplayName = "Buscar dois paises")]
		[Trait("Categoria", "Pais")]
		public async Task CodigosPaisesValidos_BuscarPaises_VariosPais()
		{
            // Arrange
            var api = new IBGEClientFactory().Build();

            // Act
            var paises = await api.BuscarPaisesAsync(76, 4);

            // Assert
            Assert.Equal(2, paises.Count());
        }


        [Fact(DisplayName = "Buscar todos os paises")]
        [Trait("Categoria", "Pais")]
        public async Task BuscarPaises_TodosPaises()
        {
            // Arrange
            var api = new IBGEClientFactory().Build();

            // Act
            var paises = await api.BuscarPaisesAsync();

            // Assert
            Assert.True(paises.Count() > 150);
        }


    }
}