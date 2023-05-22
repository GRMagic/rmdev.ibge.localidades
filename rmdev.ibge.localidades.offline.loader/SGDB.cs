using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace rmdev.ibge.localidades.offline
{
    public class SGDB
    {
        private IIBGELocalidades _online = new IBGEClientFactory().Build("http://servicodados.ibge.gov.br/");
        private const string _path = "db.json";
        private readonly Encoding _encoding = Encoding.UTF8;

        public async Task Salvar(Base db)
        {
            var json = JsonSerializer.Serialize(db);
            File.WriteAllText(_path, json, _encoding);
        }

        public async Task<Base> Carregar()
        {
            var json = await File.ReadAllTextAsync(_path, _encoding);
            var db = JsonSerializer.Deserialize<Base>(json)!;
            AtualizarReferencias(db);
            return db;
        }

        public async Task<Base> CarregarOnline()
        {
            var db = new Base();

            await CarregarPaisesAsync(db);
            // TODO: Região, SubRegião, ...
            await CarregarUFsAsync(db);
            await CarregarMesorregioesAsync(db);
            await CarregarMunicipiosAsync(db);

            AtualizarReferencias(db);

            return db;
        }

        public async Task CarregarPaisesAsync(Base db)
        {
            var idiomas = Enum.GetValues(typeof(Idioma));
            foreach(var idioma in idiomas)
            {
                var i = (Idioma)idioma;
                db.Paises[i] = await _online.BuscarPaisesAsync(i);
            }
        }

        public async Task CarregarUFsAsync(Base db)
        {
            var list = await _online.BuscarUFsAsync();
            db.UFs = list.ToDictionary(x => x.Id);
        }
        
        public async Task CarregarMesorregioesAsync(Base db) => db.Mesorregioes = await _online.BuscarMesorregioesAsync();

        public async Task CarregarMunicipiosAsync(Base db) => db.Municipios = await _online.BuscarMunicipiosAsync();

        public void AtualizarReferencias(Base db)
        {
            foreach (var mesorregiao in db.Mesorregioes) mesorregiao.UF = db.UFs[mesorregiao.UF.Id];
        }
    }
}
