namespace rmdev.ibge.localidades.tests
{
    public class UFTests
    {
        [Fact(DisplayName = "Buscar todas unidades da federação")]
        [Trait("Categoria", "UFs")]
        public async Task BuscarUFs_TodasUFs()
        {
            // Arrange
            var api = new IBGEClientFactory().Build();

            // Act
            var ufs = await api.BuscarUFsAsync();

            // Assert
            Assert.True(ufs.Count > 20);
        }

        [Fact(DisplayName = "Buscar uma unidades da federação")]
        [Trait("Categoria", "UFs")]
        public async Task BuscarUF_UmaUF()
        {
            // Arrange
            var api = new IBGEClientFactory().Build();

            // Act
            var uf = await api.BuscarUFAsync(42);

            // Assert
            Assert.Equivalent(new UF() 
            {
                Id = 42,
                Nome = "Santa Catarina",
                Sigla = "SC",
                Regiao = new ()
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
            var api = new IBGEClientFactory().Build();

            // Act
            var ufs = await api.BuscarUFsPorMacrorregiaoAsync(4);

            // Assert
            Assert.True(ufs.Count == 3);
        }
    }
}