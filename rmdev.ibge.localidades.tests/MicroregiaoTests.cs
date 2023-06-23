namespace rmdev.ibge.localidades.tests
{
    public class MicroregiaoTests
    {
        private readonly IIBGELocalidades _api;
        public MicroregiaoTests() => _api = new IBGEClientFactory().Build("http://servicodados.ibge.gov.br/");

        [Fact(DisplayName = "Buscar todas as microregioes")]
        [Trait("Categoria", "Microregiões")]
        public async Task BuscarMicroregioes_TodasMicroregioes()
        {
            // Arrange

            // Act
            var microregioes = await _api.BuscarMicroregioesAsync();

            // Assert
            Assert.True(microregioes.Count > 500);
        }

        [Fact(DisplayName = "Buscar uma microregiao")]
        [Trait("Categoria", "Microregiões")]
        public async Task IdentificadorValido_BuscarMicroregiao_MicroregiaoDadosPreenchidos()
        {
            // Arrange
            var codigoIbge = 33007;

            // Act
            var microregiao = await _api.BuscarMicroregiaoAsync(codigoIbge);

            // Assert
            Assert.Equivalent(new Microrregiao
            {
                Id = 33007,
                Nome = "Nova Friburgo",
                Mesorregiao = new Mesorregiao
                {
                    Id = 3303,
                    Nome = "Centro Fluminense",
                    UF = new UF
                    {
                        Id = 33,
                        Nome = "Rio de Janeiro",
                        Sigla = "RJ",
                        Regiao = new Macrorregiao
                        {
                            Id = 3,
                            Nome = "Sudeste",
                            Sigla = "SE"
                        }
                    }
                }
            },
            microregiao);
        }

        [Fact(DisplayName = "Buscar varias microregiões")]
        [Trait("Categoria", "Microregiões")]
        public async Task IdentificadoresValidos_BuscarMicroregioes_VariasMicroregioes()
        {
            // Arrange

            // Act
            var microregioes = await _api.BuscarMicroregioesAsync(31007, 33007);

            // Assert
            Assert.True(microregioes.Count() == 2);
        }

        [Fact(DisplayName = "Buscar microregiões pela UF")]
        [Trait("Categoria", "Microregiões")]
        public async Task IdentificadorUFValido_BuscarMicroregiãoPorUF_MicroregioesFiltradas()
        {
            // Arrange
            var idUF = 42;

            // Act
            var microregioes = await _api.BuscarMicroregiaoPorUFAsync(idUF);

            // Assert
            Assert.NotEmpty(microregioes);
            Assert.All(microregioes, m => Assert.Equal(idUF, m.Mesorregiao.UF.Id));
        }

        [Fact(DisplayName = "Buscar microregiões de varias UFs")]
        [Trait("Categoria", "Microregiões")]
        public async Task IdentificadoresUFValidos_BuscarMicroregiaoPorUF_MicroregioesFiltradas()
        {
            // Arrange
            var idsUF = new long[] { 33, 42 };

            // Act
            var microregioes = await _api.BuscarMicroregiaoPorUFAsync(idsUF);

            // Assert
            var ufs = microregioes.Select(m => m.Mesorregiao.UF.Id).ToHashSet();
            Assert.Equal(idsUF.Length, ufs.Count);
            Assert.Subset(idsUF.ToHashSet(), ufs);
        }

        [Fact(DisplayName = "Buscar microregiões de algumas mesorregiões")]
        [Trait("Categoria", "Microregiões")]
        public async Task IdentificadoresMesorregiaoValidos_BuscarMicroregiaoPorMesorregiao_MicroregioesFiltradas()
        {
            // Arrange
            var idsMesorregiao = new long[] { 3303, 3304 };

            // Act
            var microregioes = await _api.BuscarMicroregiaoPorMesorregiaoAsync(idsMesorregiao);

            // Assert
            var mesoregioes = microregioes.Select(m => m.Mesorregiao.Id).ToHashSet();
            Assert.Equal(idsMesorregiao.Length, mesoregioes.Count);
            Assert.Subset(idsMesorregiao.ToHashSet(), mesoregioes);
        }

        [Fact(DisplayName = "Buscar microregião de algumas macrorregiões")]
        [Trait("Categoria", "Microregiões")]
        public async Task IdentificadoresMacrorregiaoValidos_BuscarMicroregiaoPorMacrorregiao_MicroregioesFiltradas()
        {
            // Arrange
            var idsMacrorregioes = new long[] { 3, 4 };

            // Act
            var microregioes = await _api.BuscarMicroregiaoPorMacrorregiaoAsync(idsMacrorregioes);

            // Assert
            var macrorregioes = microregioes.Select(m => m.Mesorregiao.UF.Regiao.Id).ToHashSet();
            Assert.Equal(idsMacrorregioes.Length, macrorregioes.Count);
            Assert.Subset(idsMacrorregioes.ToHashSet(), macrorregioes);
        }

    }
}