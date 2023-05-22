// See https://aka.ms/new-console-template for more information
using rmdev.ibge.localidades.offline;
using System.Text.Json;

Console.WriteLine("Hello, World!");

var sgdb = new SGDB();

//var db = await sgdb.CarregarOnline();
//await sgdb.Salvar(db);

var db = await sgdb.Carregar();


Console.WriteLine("Hello, World!");