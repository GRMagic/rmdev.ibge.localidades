namespace rmdev.ibge.localidades.tests
{
    public class MacrorregiaoTests
    {

		[Fact(DisplayName = "Buscar todas as macroregiões")]
		[Trait("Categoria", "Macrorregiões")]
		public async Task BuscarMacrorregioes_TodasMacrorregioes()
		{
            // Arrange
            var api = new IBGEClientFactory().Build();

            // Act
            var regioes = await api.BuscarMacrorregioesAsync();

            // Assert
            Assert.True(regioes.Count() > 4);
        }

        [Fact(DisplayName = "Buscar algumas macroregiões")]
        [Trait("Categoria", "Macrorregiões")]
        public async Task DoisCodigosValidos_BuscarMacrorregioes_DuasMacrorregioes()
        {
            // Arrange
            var api = new IBGEClientFactory().Build();

            // Act
            var regioes = await api.BuscarMacrorregioesAsync(1, 2);

            // Assert
            Assert.True(regioes.Count() == 2);
        }

        [Fact(DisplayName = "Buscar uma macroregião")]
        [Trait("Categoria", "Macrorregiões")]
        public async Task CodigoValido_BuscarMacrorregioes_DadosMacrorregiao()
        {
            // Arrange
            var api = new IBGEClientFactory().Build();

            // Act
            var regiao = await api.BuscarMacrorregiaoAsync(4);

            // Assert
            Assert.Equivalent(new Macrorregiao
            {
                Id= 4,
                Nome = "Sul",
                Sigla = "S"
            }, regiao);
        }
    }
}