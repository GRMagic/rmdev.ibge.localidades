# rmdev.ibge.localidades

[![NuGet](https://img.shields.io/nuget/v/rmdev.ibge.localidades.svg)](https://nuget.org/packages/rmdev.ibge.localidades)
[![Nuget](https://img.shields.io/nuget/dt/rmdev.ibge.localidades.svg)](https://nuget.org/packages/rmdev.ibge.localidades) 

Biblioteca a facilitar o consumo da API de localidades do IBGE

Documentação da API do IBGE: https://servicodados.ibge.gov.br/api/docs/localidades

## Buscas suportadas
- Distritos
- Municípios
- Mesorregiões
- Estados (UFs)
- Regiões
- Países

## Alguns Exemplos

```csharp
var api = new IBGEClientFactory().Build();

var paises = await api.BuscarPaisesAsync();
var paisesEn = await api.BuscarPaisesAsync(Idioma.EN);
var ufs = await api.BuscarUFsAsync();
var municipio1 = await api.BuscarMunicipioPorNomeAsync("Pérola d'Oeste");
var municipio2 = await api.BuscarMunicipioAsync(4210001);
var municipiosSC = await api.BuscarMunicipioPorUFAsync(42);
var distritos = await api.BuscarDistritosPorMunicipioAsync(1501402);
```

Para mais exemplos consulte o projeto de testes.