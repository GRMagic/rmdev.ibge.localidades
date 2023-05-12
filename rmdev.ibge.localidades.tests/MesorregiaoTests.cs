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
            var mesorregioes = await _api.BuscarMesorregioesAsync();

            // Assert
            Assert.True(mesorregioes.Count() > 4);
        }

        [Fact(DisplayName = "Buscar algumas mesoregiões")]
        [Trait("Categoria", "Mesorregiões")]
        public async Task DoisCodigosValidos_BuscarMesorregioes_DuasMesorregioes()
        {
            // Arrange

            // Act
            var mesorregioes = await _api.BuscarMesorregioesAsync(3302, 3509);

            // Assert
            Assert.True(mesorregioes.Count() == 2);
        }

        [Fact(DisplayName = "Buscar uma mesoregião")]
        [Trait("Categoria", "Mesorregiões")]
        public async Task CodigoValido_BuscarMesorregioes_DadosMesorregiao()
        {
            // Arrange

            // Act
            var mesorregiao = await _api.BuscarMesorregiaoAsync(3509);

            // Assert
            Assert.Equivalent(new
            {
                Id = 3509L,
                Nome = "Marília",
                UF = new
                {
                    Id = 35L
                }
            }, mesorregiao);
        }

        [Fact(DisplayName = "Buscar duas mesoregiões, sendo que uma não existe")]
        [Trait("Categoria", "Mesorregiões")]
        public async Task UmCodigoValidoUmCodigoInvalido_BuscarMesorregioes_DadosUmaMesorregiao()
        {
            // Arrange

            // Act
            var mesorregiao = await _api.BuscarMesorregioesAsync(3509, 3599);

            // Assert
            Assert.Single(mesorregiao);
        }

        [Fact(DisplayName = "Buscar uma mesoregiões que não existe")]
        [Trait("Categoria", "Mesorregiões")]
        public async Task UmCodigoInvalido_BuscarMesorregioes_Null()
        {
            // Arrange

            // Act
            var mesorregiao = await _api.BuscarMesorregiaoAsync(3599);

            // Assert
            Assert.Null(mesorregiao);
        }

        [Fact(DisplayName = "Buscar mesoregiões de uma UF")]
        [Trait("Categoria", "Mesorregiões")]
        public async Task UmCodigoValido_BuscarMesorregioesPorUF_ListaMesoregioes()
        {
            // Arrange

            // Act
            var mesorregioes = await _api.BuscarMesorregioesPorUFAsync(33);

            // Assert
            Assert.True(mesorregioes.Count > 4);
        }

        [Fact(DisplayName = "Buscar mesoregiões de uma UF sem informar a UF")]
        [Trait("Categoria", "Mesorregiões")]
        public async Task SemUF_BuscarMesorregioesPorUF_ListaVazia()
        {
            // Arrange

            // Act
            var mesorregioes = await _api.BuscarMesorregioesPorUFAsync();

            // Assert
            Assert.Empty(mesorregioes);
        }

        [Fact(DisplayName = "Buscar mesoregiões de duas UF")]
        [Trait("Categoria", "Mesorregiões")]
        public async Task DuasUF_BuscarMesorregioesPorUF_RegioesDeAbasUFs()
        {
            // Arrange

            var idsUFs = new long[] { 33, 35 };

            // Act
            var mesorregioes = await _api.BuscarMesorregioesPorUFAsync(idsUFs);

            // Assert
            var ufs = mesorregioes.Select(r => r.UF.Id).ToHashSet();
            Assert.Equal(idsUFs.Length, ufs.Count);
            Assert.Subset(idsUFs.ToHashSet(), ufs);
        }

        [Fact(DisplayName = "Buscar mesoregiões de uma macroregião")]
        [Trait("Categoria", "Mesorregiões")]
        public async Task UmCodigoValido_BuscarMesorregioesPorMacrorregiao_ListaMesoregioes()
        {
            // Arrange

            // Act
            var mesorregioes = await _api.BuscarMesorregioesPorMacrorregiaoAsync(4);

            // Assert
            Assert.True(mesorregioes.Count > 15);
        }

        [Fact(DisplayName = "Buscar mesoregiões de uma macrorregião sem informar a macrorregiao")]
        [Trait("Categoria", "Mesorregiões")]
        public async Task SemMacrorregiao_BuscarMesorregioesPorMacrorregiao_ListaVazia()
        {
            // Arrange

            // Act
            var mesorregioes = await _api.BuscarMesorregioesPorMacrorregiaoAsync();

            // Assert
            Assert.Empty(mesorregioes);
        }
    }
}