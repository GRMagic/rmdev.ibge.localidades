namespace rmdev.ibge.localidades.tests
{
    public class MacrorregiaoTests
    {
        private readonly IIBGELocalidades _api;
        public MacrorregiaoTests() => _api = new IBGEClientFactory().Build("http://servicodados.ibge.gov.br/");

		[Fact(DisplayName = "Buscar todas as macroregiões")]
		[Trait("Categoria", "Macrorregiões")]
		public async Task BuscarMacrorregioes_TodasMacrorregioes()
		{
            // Arrange

            // Act
            var regioes = await _api.BuscarMacrorregioesAsync();

            // Assert
            Assert.True(regioes.Count() > 4);
        }

        [Fact(DisplayName = "Buscar algumas macroregiões")]
        [Trait("Categoria", "Macrorregiões")]
        public async Task DoisCodigosValidos_BuscarMacrorregioes_DuasMacrorregioes()
        {
            // Arrange

            // Act
            var regioes = await _api.BuscarMacrorregioesAsync(1, 2);

            // Assert
            Assert.True(regioes.Count() == 2);
        }

        [Fact(DisplayName = "Buscar uma macroregião")]
        [Trait("Categoria", "Macrorregiões")]
        public async Task CodigoValido_BuscarMacrorregioes_DadosMacrorregiao()
        {
            // Arrange

            // Act
            var regiao = await _api.BuscarMacrorregiaoAsync(4);

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