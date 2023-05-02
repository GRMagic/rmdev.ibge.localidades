using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace rmdev.ibge.localidades.tests
{
    public class MunicipioTests
    {
        [Fact(DisplayName = "Buscar todos os municípios")]
        [Trait("Categoria", "Municípios")]
        public async Task BuscarMunicipios_TodasMunicipios()
        {
            // Arrange
            var api = new IBGEClientFactory().Build();

            // Act
            var municipios = await api.BuscarMunicipiosAsync();

            // Assert
            Assert.True(municipios.Count > 5000);
        }

        [Fact(DisplayName = "Buscar um município")]
        [Trait("Categoria", "Municípios")]
        public async Task IdentificadorValido_BuscarMunicipio_MunicipioDadosPreenchidos()
        {
            // Arrange
            var api = new IBGEClientFactory().Build();
            var codigoIbge = 4210001;

            // Act
            var municipio = await api.BuscarMunicipioAsync(codigoIbge);

            // Assert
            Assert.True(municipio is Municipio
            {
                Id: > 0,
                Nome.Length: > 0,
                Microrregiao: 
                {
                    Id: > 0,
                    Nome.Length: > 0,
                    Mesorregiao:
                    {
                        Id: > 0,
                        Nome.Length: > 0,
                        UF:
                        {
                            Id: > 0,
                            Nome.Length: > 0,
                            Sigla.Length: > 0,
                            Regiao:
                            {
                                Id: > 0,
                                Nome.Length: > 0,
                                Sigla.Length: > 0
                            }
                        }
                    }
                },
                RegiaoImediata:
                {
                    Id: > 0,
                    Nome.Length: > 0,
                    RegiaoIntermediaria:
                    {
                        Id: > 0,
                        Nome.Length: > 0,
                        UF:
                        {
                            Id: > 0,
                            Nome.Length: > 0,
                            Sigla.Length: > 0,
                            Regiao:
                            {
                                Id: > 0,
                                Nome.Length: > 0,
                                Sigla.Length: > 0
                            }
                        }
                    }
                }
            });
        }

        [Fact(DisplayName = "Buscar varios municípios")]
        [Trait("Categoria", "Municípios")]
        public async Task IdentificadoresValidos_BuscarMunicipios_VariosMunicipios()
        {
            // Arrange
            var api = new IBGEClientFactory().Build();

            // Act
            var municipios = await api.BuscarMunicipiosAsync(4210001, 3550308);

            // Assert
            Assert.True(municipios.Count() == 2);
        }

        [Fact(DisplayName = "Buscar município pelo nome")]
        [Trait("Categoria", "Municípios")]
        public async Task PeroladOesteBuscarMunicipioPorNome_DadosMunicipio()
        {
            // Arrange
            var api = new IBGEClientFactory().Build();
            var nome = "Pérola d'Oeste";

            // Act
            var municipio = await api.BuscarMunicipioPorNomeAsync(nome);

            // Assert
            Assert.Equal(nome, municipio.Nome);
        }


        [Fact(DisplayName = "Buscar municípios pela UF")]
        [Trait("Categoria", "Municípios")]
        public async Task IdentificadorUFValido_BuscarMunicipioPorUF_MunicipiosFiltrados()
        {
            // Arrange
            var api = new IBGEClientFactory().Build();
            var idUF = 42;

            // Act
            var municipios = await api.BuscarMunicipioPorUFAsync(idUF);

            // Assert
            Assert.All(municipios, m => Assert.Equal(idUF, m.RegiaoImediata.RegiaoIntermediaria.UF.Id));
        }

        [Fact(DisplayName = "Buscar municípios de varias UFs")]
        [Trait("Categoria", "Municípios")]
        public async Task IdentificadoresUFValidos_BuscarMunicipioPorUF_MunicipiosFiltrados()
        {
            // Arrange
            var api = new IBGEClientFactory().Build();
            var idsUF = new[] { 33, 42 };

            // Act
            var municipios = await api.BuscarMunicipioPorUFAsync(idsUF);

            // Assert
            var ufs = municipios.Select(m => m.RegiaoImediata.RegiaoIntermediaria.UF.Id).ToHashSet();
            Assert.Subset(idsUF.ToHashSet(), ufs);
        }
    }
}