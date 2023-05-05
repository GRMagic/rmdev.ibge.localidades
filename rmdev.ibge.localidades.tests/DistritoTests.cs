namespace rmdev.ibge.localidades.tests
{
    public class DistritoTests
    {
        private readonly IIBGELocalidades _api;
        public DistritoTests() => _api = new IBGEClientFactory().Build("http://servicodados.ibge.gov.br/");

        [Fact(DisplayName = "Buscar todos os distritos")]
        [Trait("Categoria", "Distritos")]
        public async Task BuscarDistritos_TodosDistritos()
        {
            // Arrange

            // Act
            var distritos = await _api.BuscarDistritosAsync();

            // Assert
            Assert.True(distritos.Count > 10000);
        }

        [Fact(DisplayName = "Buscar vários os distritos")]
        [Trait("Categoria", "Distritos")]
        public async Task CodigosValidos_BuscarDistritos_VariosDistritos()
        {
            // Arrange
            var codigos = new[] { 291790410, 160030312 };

            // Act
            var distritos = await _api.BuscarDistritosAsync(codigos);

            // Assert
            Assert.True(distritos.Count == codigos.Length);
        }

        [Fact(DisplayName = "Buscar um distrito")]
        [Trait("Categoria", "Distritos")]
        public async Task CodigoValido_BuscarDistrito_DadosDistritos()
        {
            // Arrange
            var codigo = 160030312;

            // Act
            var distrito = await _api.BuscarDistritoAsync(codigo);

            // Assert
            Assert.Equivalent(new Distrito
            {
                Id = codigo,
                Nome = "Fazendinha",
                Municipio = new Municipio
                {
                    Id = 1600303,
                    Nome = "Macapá",
                    Microrregiao = new Microrregiao
                    {
                        Id = 16003,
                        Nome = "Macapá",
                        Mesorregiao = new Mesorregiao
                        {
                            Id = 1602,
                            Nome = "Sul do Amapá",
                            UF = new UF
                            {
                                Id = 16,
                                Nome = "Amapá",
                                Sigla = "AP",
                                Regiao = new Macrorregiao
                                {
                                    Id = 1,
                                    Nome = "Norte",
                                    Sigla = "N"
                                }
                            }
                        }
                    },
                    RegiaoImediata = new RegiaoImediata
                    {
                        Id = 160001,
                        Nome = "Macapá",
                        RegiaoIntermediaria = new RegiaoIntermediaria
                        {
                            Id = 1601,
                            Nome = "Macapá",
                            UF = new UF
                            {
                                Id = 16,
                                Nome = "Amapá",
                                Sigla = "AP",
                                Regiao = new Macrorregiao
                                {
                                    Id = 1,
                                    Nome = "Norte",
                                    Sigla = "N"
                                }
                            }
                        }
                    }
                }
            }, distrito);
        }

        [Fact(DisplayName = "Buscar distritos de várias UFs")]
        [Trait("Categoria", "Distritos")]
        public async Task CodigosValidos_BuscarDistritosPorUF_DistritosDasUFsSelecionadas()
        {
            // Arrange
            var codigosUF = new[] { 42, 33 };

            // Act
            var distritos = await _api.BuscarDistritosPorUFAsync(codigosUF);

            // Assert
            Assert.NotEmpty(distritos);
            var ufsSet = distritos.Select(d => d.Municipio.RegiaoImediata.RegiaoIntermediaria.UF.Id).ToHashSet();
            Assert.Subset(codigosUF.ToHashSet(), ufsSet);
        }

        [Fact(DisplayName = "Buscar distritos de várias mesorregiões")]
        [Trait("Categoria", "Distritos")]
        public async Task CodigosValidos_BuscarDistritosPorMesorregiao_DistritosDasMesorregioesSelecionadas()
        {
            // Arrange
            var codigosMesorregioes = new[] { 1602, 4307 };

            // Act
            var distritos = await _api.BuscarDistritosPorMesorregiaoAsync(codigosMesorregioes);

            // Assert
            var mesorregioesSet = distritos.Select(d => d.Municipio.Microrregiao.Mesorregiao.Id).ToHashSet();
            Assert.Equal(codigosMesorregioes.Length, mesorregioesSet.Count);
            Assert.Subset(codigosMesorregioes.ToHashSet(), mesorregioesSet);
        }

        [Fact(DisplayName = "Buscar distritos de várias microrregiões")]
        [Trait("Categoria", "Distritos")]
        public async Task CodigosValidos_BuscarDistritosPorMicrorregiao_DistritosDasMicrorregioesSelecionadas()
        {
            // Arrange
            var codigosMicrorregioes = new[] { 16003, 43032 };

            // Act
            var distritos = await _api.BuscarDistritosPorMicrorregiaoAsync(codigosMicrorregioes);

            // Assert
            var microrregioesSet = distritos.Select(d => d.Municipio.Microrregiao.Id).ToHashSet();
            Assert.Equal(codigosMicrorregioes.Length, microrregioesSet.Count);
            Assert.Subset(codigosMicrorregioes.ToHashSet(), microrregioesSet);
        }

        [Fact(DisplayName = "Buscar distritos de vários municípios")]
        [Trait("Categoria", "Distritos")]
        public async Task CodigosValidos_BuscarDistritosPorMunicipio_DistritosDosMunicipiosSelecionados()
        {
            // Arrange
            var codigosMunicipios = new[] { 3550308, 1501402 };

            // Act
            var distritos = await _api.BuscarDistritosPorMunicipioAsync(codigosMunicipios);

            // Assert
            var municipiosSet = distritos.Select(d => d.Municipio.Id).ToHashSet();
            Assert.Equal(codigosMunicipios.Length, municipiosSet.Count);
            Assert.Subset(codigosMunicipios.ToHashSet(), municipiosSet);
        }

        [Fact(DisplayName = "Buscar distritos de várias regiões imediatas")]
        [Trait("Categoria", "Distritos")]
        public async Task CodigosValidos_BuscarDistritosPorRegiaoImediata_DistritosDasRegioesImediatasSelecionadas()
        {
            // Arrange
            var codigosRegiaoImediata = new[] { 330002, 330005 };

            // Act
            var distritos = await _api.BuscarDistritosPorRegiaoImediataAsync(codigosRegiaoImediata);

            // Assert
            var regiaoImediataSet = distritos.Select(d => d.Municipio.RegiaoImediata.Id).ToHashSet();
            Assert.Equal(codigosRegiaoImediata.Length, regiaoImediataSet.Count);
            Assert.Subset(codigosRegiaoImediata.ToHashSet(), regiaoImediataSet);
        }

        [Fact(DisplayName = "Buscar distritos de várias regiões intermediarias")]
        [Trait("Categoria", "Distritos")]
        public async Task CodigosValidos_BuscarDistritosPorRegiaoIntermediaria_DistritosDasRegioesIntermediariasSelecionadas()
        {
            // Arrange
            var codigosRegiaoIntermediaria = new[] { 2604, 2908 };

            // Act
            var distritos = await _api.BuscarDistritosPorRegiaoIntermediariaAsync(codigosRegiaoIntermediaria);

            // Assert
            var regiaoIntermediariaSet = distritos.Select(d => d.Municipio.RegiaoImediata.RegiaoIntermediaria.Id).ToHashSet();
            Assert.Equal(codigosRegiaoIntermediaria.Length, regiaoIntermediariaSet.Count);
            Assert.Subset(codigosRegiaoIntermediaria.ToHashSet(), regiaoIntermediariaSet);
        }

        [Fact(DisplayName = "Buscar distritos de várias Macrorregiões")]
        [Trait("Categoria", "Distritos")]
        public async Task CodigosValidos_BuscarDistritosPorMacrorregiao_DistritosDasMacrorregioesSelecionadas()
        {
            // Arrange
            var codigosMacrorregiao = new[] { 3, 4 };

            // Act
            var distritos = await _api.BuscarDistritosPorMacrorregiaoAsync(codigosMacrorregiao);

            // Assert
            var macrorregiaoSet = distritos.Select(d => d.Municipio.RegiaoImediata.RegiaoIntermediaria.UF.Regiao.Id).ToHashSet();
            Assert.Equal(codigosMacrorregiao.Length, macrorregiaoSet.Count);
            Assert.Subset(codigosMacrorregiao.ToHashSet(), macrorregiaoSet);
        }
    }
}