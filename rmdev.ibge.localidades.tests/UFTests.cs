namespace rmdev.ibge.localidades.tests
{
    public class UFTests
    {
        private readonly IIBGELocalidades _api;
        public UFTests() => _api = new IBGEClientFactory().Build("http://servicodados.ibge.gov.br/");

        [Fact(DisplayName = "Buscar todas unidades da federação")]
        [Trait("Categoria", "UFs")]
        public async Task BuscarUFs_TodasUFs()
        {
            // Arrange

            // Act
            var ufs = await _api.BuscarUFsAsync();

            // Assert
            Assert.True(ufs.Count > 20);
        }

        [Fact(DisplayName = "Buscar uma unidades da federação")]
        [Trait("Categoria", "UFs")]
        public async Task BuscarUF_UmaUF()
        {
            // Arrange

            // Act
            var uf = await _api.BuscarUFAsync(42);

            // Assert
            Assert.Equivalent(new UF()
            {
                Id = 42,
                Nome = "Santa Catarina",
                Sigla = "SC",
                Regiao = new()
                {
                    Id = 4,
                    Sigla = "S",
                    Nome = "Sul"
                }
            }, uf);
        }

        [Fact(DisplayName = "Buscar unidades da federação por região")]
        [Trait("Categoria", "UFs")]
        public async Task RegiaoSul_BuscarUFsPorMacrorregiao_TresUFs()
        {
            // Arrange

            // Act
            var ufs = await _api.BuscarUFsPorMacrorregiaoAsync(4);

            // Assert
            Assert.True(ufs.Count == 3);
        }
    }
}