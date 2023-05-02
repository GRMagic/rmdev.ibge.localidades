using System.Threading.Tasks;
using Xunit;
namespace rmdev.ibge.localidades.tests
{
    public class UnitTest1
    {
        [Fact(DisplayName = "Buscar dados de um pais")]
        [Trait("Categoria", "Pais")]
        public async Task CodigoPaisValido_BuscarPais_DadosPais()
        {
            // Arrange
            var api = new IBGEClientFactory().Build();

            // Act
            var paises = await api.PaisesAsync(76);

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
		public async Task CodigoPaisValido_BuscarPais_ApenasUmPais()
		{
            // Arrange
            var api = new IBGEClientFactory().Build();

            // Act
            var paises = await api.PaisesAsync(76);

            // Assert
            Assert.Single(paises);
        }


		[Fact(DisplayName = "Buscar dois paises")]
		[Trait("Categoria", "Pais")]
		public async Task CodigosPaisesValidos_BuscarPais_VariosPais()
		{
            // Arrange
            var api = new IBGEClientFactory().Build();

            // Act
            var paises = await api.PaisesAsync(76, 4);

            // Assert
            Assert.Equal(2, paises.Count());

        }

	}
}