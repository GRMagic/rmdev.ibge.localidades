namespace rmdev.ibge.localidades.tests
{
    public class SubdistritoTests
    {
        private readonly IIBGELocalidades _api;
        public SubdistritoTests() => _api = new IBGEClientFactory().Build("http://servicodados.ibge.gov.br/");

        [Fact(DisplayName = "Buscar todos os subdistritos")]
        [Trait("Categoria", "Subdistritos")]
        public async Task BuscarSubdistritos_TodosSubdistritos()
        {
            // Arrange

            // Act
            var subdistritos = await _api.BuscarSubdistritosAsync();

            // Assert
            Assert.True(subdistritos.Count > 500);
        }

        [Fact(DisplayName = "Buscar vários os subdistritos")]
        [Trait("Categoria", "Subdistritos")]
        public async Task CodigosValidos_BuscarSubdistritos_VariosSubdistritos()
        {
            // Arrange
            var codigos = new long[] { 110020505, 130260305 };

            // Act
            var subdistritos = await _api.BuscarDistritosAsync(codigos);

            // Assert
            Assert.True(subdistritos.Count == codigos.Length);
        }

        [Fact(DisplayName = "Buscar um subdistrito")]
        [Trait("Categoria", "Subdistritos")]
        public async Task CodigoValido_BuscarSubdistrito_DadosSubdistritos()
        {
            // Arrange
            var codigo = 53001080517;

            // Act
            var subdistrito = await _api.BuscarSubdistritoAsync(codigo);

            // Assert
            Assert.Equivalent(new Subdistrito
            {
                Id = codigo,
                Nome = "Cruzeiro",
                Distrito = new()
                {
                    Id = 530010805,
                    Nome = "Brasília",
                    Municipio = new()
                    {
                        Id = 5300108,
                        Nome = "Brasília",
                        Microrregiao = new()
                        {
                            Id = 53001,
                            Nome = "Brasília",
                            Mesorregiao = new()
                            {
                                Id = 5301,
                                Nome = "Distrito Federal",
                                UF = new()
                                {
                                    Id = 53,
                                    Nome = "Distrito Federal",
                                    Sigla = "DF",
                                    Regiao = new()
                                    {
                                        Id = 5,
                                        Nome = "Centro-Oeste",
                                        Sigla = "CO"
                                    }
                                }
                            }
                        },
                        RegiaoImediata = new()
                        {
                            Id = 530001,
                            Nome = "Distrito Federal",
                            RegiaoIntermediaria = new()
                            {
                                Id = 5301,
                                Nome = "Distrito Federal",
                                UF = new()
                                {
                                    Id = 53,
                                    Nome = "Distrito Federal",
                                    Sigla = "DF",
                                    Regiao = new()
                                    {
                                        Id = 5,
                                        Nome = "Centro-Oeste",
                                        Sigla = "CO"
                                    }
                                }
                            }
                        }
                    }
                }
            }
            , subdistrito);
           
        }

        [Fact(DisplayName = "Buscar subdistritos de vários distritos")]
        [Trait("Categoria", "Subdistritos")]
        public async Task CodigosValidos_BuscarSubdistritosPorDistrito_SubdistritosDosDistritosSelecionados()
        {
            // Arrange
            var codigosDistritos = new long[] { 110020505, 130260305 };

            // Act
            var subdistritos = await _api.BuscarSubdistritosPorDistritoAsync(codigosDistritos);

            // Assert
            Assert.NotEmpty(subdistritos);
            var distritosSet = subdistritos.Select(d => d.Distrito.Id).ToHashSet();
            Assert.Subset(codigosDistritos.ToHashSet(), distritosSet);
        }

        [Fact(DisplayName = "Buscar subdistritos de várias UFs")]
        [Trait("Categoria", "Subdistritos")]
        public async Task CodigosValidos_BuscarSubdistritosPorUF_SubdistritosDasUFsSelecionadas()
        {
            // Arrange
            var codigosUF = new long[] { 42, 33 };

            // Act
            var subdistritos = await _api.BuscarSubdistritosPorUFAsync(codigosUF);

            // Assert
            Assert.NotEmpty(subdistritos);
            var ufsSet = subdistritos.Select(d => d.Distrito.Municipio.RegiaoImediata.RegiaoIntermediaria.UF.Id).ToHashSet();
            Assert.Subset(codigosUF.ToHashSet(), ufsSet);
        }
        
        [Fact(DisplayName = "Buscar subdistritos de várias mesorregiões")]
        [Trait("Categoria", "Subdistritos")]
        public async Task CodigosValidos_BuscarSubdistritosPorMesorregiao_SubdistritosDasMesorregioesSelecionadas()
        {
            // Arrange
            var codigosMesorregioes = new long[] { 4305, 5203 };

            // Act
            var subdistritos = await _api.BuscarSubdistritosPorMesorregiaoAsync(codigosMesorregioes);

            // Assert
            var mesorregioesSet = subdistritos.Select(d => d.Distrito.Municipio.Microrregiao.Mesorregiao.Id).ToHashSet();
            Assert.Equal(codigosMesorregioes.Length, mesorregioesSet.Count);
            Assert.Subset(codigosMesorregioes.ToHashSet(), mesorregioesSet);
        }
        
        [Fact(DisplayName = "Buscar subdistritos de várias microrregiões")]
        [Trait("Categoria", "Subdistritos")]
        public async Task CodigosValidos_BuscarSubdistritosPorMicrorregiao_SubdistritosDasMicrorregioesSelecionadas()
        {
            // Arrange
            var codigosMicrorregioes = new long[] { 11001, 43029 };

            // Act
            var subdistritos = await _api.BuscarSubdistritosPorMicrorregiaoAsync(codigosMicrorregioes);

            // Assert
            var microrregioesSet = subdistritos.Select(d => d.Distrito.Municipio.Microrregiao.Id).ToHashSet();
            Assert.Equal(codigosMicrorregioes.Length, microrregioesSet.Count);
            Assert.Subset(codigosMicrorregioes.ToHashSet(), microrregioesSet);
        }
        
        [Fact(DisplayName = "Buscar subdistritos de vários municípios")]
        [Trait("Categoria", "Subdistritos")]
        public async Task CodigosValidos_BuscarsubdistritosPorMunicipio_SubdistritosDosMunicipiosSelecionados()
        {
            // Arrange
            var codigosMunicipios = new long[] { 3304557, 3305208 };

            // Act
            var subdistritos = await _api.BuscarSubdistritosPorMunicipioAsync(codigosMunicipios);

            // Assert
            var municipiosSet = subdistritos.Select(d => d.Distrito.Municipio.Id).ToHashSet();
            Assert.Equal(codigosMunicipios.Length, municipiosSet.Count);
            Assert.Subset(codigosMunicipios.ToHashSet(), municipiosSet);
        }
        
        [Fact(DisplayName = "Buscar subdistritos de várias regiões imediatas")]
        [Trait("Categoria", "Subdistritos")]
        public async Task CodigosValidos_BuscarSubdistritosPorRegiaoImediata_SubdistritosDasRegioesImediatasSelecionadas()
        {
            // Arrange
            var codigosRegiaoImediata = new long[] { 330001, 330013 };

            // Act
            var subdistritos = await _api.BuscarSubdistritosPorRegiaoImediataAsync(codigosRegiaoImediata);

            // Assert
            var regiaoImediataSet = subdistritos.Select(d => d.Distrito.Municipio.RegiaoImediata.Id).ToHashSet();
            Assert.Equal(codigosRegiaoImediata.Length, regiaoImediataSet.Count);
            Assert.Subset(codigosRegiaoImediata.ToHashSet(), regiaoImediataSet);
        }
       
        [Fact(DisplayName = "Buscar subdistritos de várias macrorregiões")]
        [Trait("Categoria", "Subdistritos")]
        public async Task CodigosValidos_BuscarSubdistritosPorMacrorregiao_SubdistritosDasMacrorregioesSelecionadas()
        {
            // Arrange
            var codigosMacrorregiao = new long[] { 3, 4 };

            // Act
            var subdistritos = await _api.BuscarSubdistritosPorMacrorregiaoAsync(codigosMacrorregiao);

            // Assert
            var macrorregiaoSet = subdistritos.Select(d => d.Distrito.Municipio.RegiaoImediata.RegiaoIntermediaria.UF.Regiao.Id).ToHashSet();
            Assert.Equal(codigosMacrorregiao.Length, macrorregiaoSet.Count);
            Assert.Subset(codigosMacrorregiao.ToHashSet(), macrorregiaoSet);
        }
    }
}