using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace rmdev.ibge.localidades.tests
{
    public class MunicipioTests
    {
        private readonly IIBGELocalidades _api;
        public MunicipioTests() => _api = new IBGEClientFactory().Build("http://servicodados.ibge.gov.br/");

        [Fact(DisplayName = "Buscar todos os municípios")]
        [Trait("Categoria", "Municípios")]
        public async Task BuscarMunicipios_TodosMunicipios()
        {
            // Arrange

            // Act
            var municipios = await _api.BuscarMunicipiosAsync();

            // Assert
            Assert.True(municipios.Count > 5000);
        }

        [Fact(DisplayName = "Buscar um município")]
        [Trait("Categoria", "Municípios")]
        public async Task IdentificadorValido_BuscarMunicipio_MunicipioDadosPreenchidos()
        {
            // Arrange
            var codigoIbge = 4210001;

            // Act
            var municipio = await _api.BuscarMunicipioAsync(codigoIbge);

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

            // Act
            var municipios = await _api.BuscarMunicipiosAsync(4210001, 3550308);

            // Assert
            Assert.True(municipios.Count() == 2);
        }

        [Fact(DisplayName = "Buscar município pelo nome")]
        [Trait("Categoria", "Municípios")]
        public async Task PeroladOesteBuscarMunicipioPorNome_DadosMunicipio()
        {
            // Arrange
            var nome = "Pérola d'Oeste";

            // Act
            var municipio = await _api.BuscarMunicipioPorNomeAsync(nome);

            // Assert
            Assert.Equal(nome, municipio.Nome);
        }


        [Fact(DisplayName = "Buscar municípios pela UF")]
        [Trait("Categoria", "Municípios")]
        public async Task IdentificadorUFValido_BuscarMunicipioPorUF_MunicipiosFiltrados()
        {
            // Arrange
            var idUF = 42;

            // Act
            var municipios = await _api.BuscarMunicipioPorUFAsync(idUF);

            // Assert
            Assert.NotEmpty(municipios);
            Assert.All(municipios, m => Assert.Equal(idUF, m.RegiaoImediata.RegiaoIntermediaria.UF.Id));
        }

        [Fact(DisplayName = "Buscar municípios de varias UFs")]
        [Trait("Categoria", "Municípios")]
        public async Task IdentificadoresUFValidos_BuscarMunicipioPorUF_MunicipiosFiltrados()
        {
            // Arrange
            var idsUF = new long[] { 33, 42 };

            // Act
            var municipios = await _api.BuscarMunicipioPorUFAsync(idsUF);

            // Assert
            var ufs = municipios.Select(m => m.RegiaoImediata.RegiaoIntermediaria.UF.Id).ToHashSet();
            Assert.Equal(idsUF.Length, ufs.Count);
            Assert.Subset(idsUF.ToHashSet(), ufs);
        }

        [Fact(DisplayName = "Buscar municípios de algumas mesorregiões")]
        [Trait("Categoria", "Municípios")]
        public async Task IdentificadoresMesorregiaoValidos_BuscarMunicipioPorMesorregiao_MunicipiosFiltrados()
        {
            // Arrange
            var idsMesorregiao = new long[] { 3301, 3302 };

            // Act
            var municipios = await _api.BuscarMunicipioPorMesorregiaoAsync(idsMesorregiao);

            // Assert
            var mesoregioes = municipios.Select(m => m.Microrregiao.Mesorregiao.Id).ToHashSet();
            Assert.Equal(idsMesorregiao.Length, mesoregioes.Count);
            Assert.Subset(idsMesorregiao.ToHashSet(), mesoregioes);
        }

        [Fact(DisplayName = "Buscar municípios de algumas microrregiões")]
        [Trait("Categoria", "Municípios")]
        public async Task IdentificadoresMicrorregiaoValidos_BuscarMunicipioPorMicrorregiao_MunicipiosFiltrados()
        {
            // Arrange
            var idsMicrorregiao = new long[] { 33001, 33005 };

            // Act
            var municipios = await _api.BuscarMunicipioPorMicrorregiaoAsync(idsMicrorregiao);

            // Assert
            var microregioes = municipios.Select(m => m.Microrregiao.Id).ToHashSet();
            Assert.Equal(idsMicrorregiao.Length, microregioes.Count);
            Assert.Subset(idsMicrorregiao.ToHashSet(), microregioes);
        }

        [Fact(DisplayName = "Buscar municípios de algumas regiões imediatas")]
        [Trait("Categoria", "Municípios")]
        public async Task IdentificadoresRegiaoImediataValidos_BuscarMunicipioPorRegiaoImediata_MunicipiosFiltrados()
        {
            // Arrange
            var idsRegioesImediatas = new long[] { 320005, 320007 };

            // Act
            var municipios = await _api.BuscarMunicipioPorRegiaoImediataAsync(idsRegioesImediatas);

            // Assert
            var regioesImediatas = municipios.Select(m => m.RegiaoImediata.Id).ToHashSet();
            Assert.Equal(idsRegioesImediatas.Length, regioesImediatas.Count);
            Assert.Subset(idsRegioesImediatas.ToHashSet(), regioesImediatas);
        }

        [Fact(DisplayName = "Buscar municípios de algumas regiões intermediarias")]
        [Trait("Categoria", "Municípios")]
        public async Task IdentificadoresRegiaoIntermediariaValidos_BuscarMunicipioPorRegiaoIntermediaria_MunicipiosFiltrados()
        {
            // Arrange
            var idsRegioesIntermediarias = new long[] { 5206, 5105 };

            // Act
            var municipios = await _api.BuscarMunicipioPorRegiaoIntermediariaAsync(idsRegioesIntermediarias);

            // Assert
            var regioesIntermediarias = municipios.Select(m => m.RegiaoImediata.RegiaoIntermediaria.Id).ToHashSet();
            Assert.Equal(idsRegioesIntermediarias.Length, regioesIntermediarias.Count);
            Assert.Subset(idsRegioesIntermediarias.ToHashSet(), regioesIntermediarias);
        }

        [Fact(DisplayName = "Buscar municípios de algumas macrorregiões")]
        [Trait("Categoria", "Municípios")]
        public async Task IdentificadoresMacrorregiaoValidos_BuscarMunicipioPorMacrorregiao_MunicipiosFiltrados()
        {
            // Arrange
            var idsMacrorregioes = new long[] { 3, 4 };

            // Act
            var municipios = await _api.BuscarMunicipioPorMacrorregiaoAsync(idsMacrorregioes);

            // Assert
            var macrorregioes = municipios.Select(m => m.RegiaoImediata.RegiaoIntermediaria.UF.Regiao.Id).ToHashSet();
            Assert.Equal(idsMacrorregioes.Length, macrorregioes.Count);
            Assert.Subset(idsMacrorregioes.ToHashSet(), macrorregioes);
        }
    }
}