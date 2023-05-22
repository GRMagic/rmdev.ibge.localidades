using System.Collections.Generic;

namespace rmdev.ibge.localidades.offline
{
    public class Base
    {
        public Dictionary<Idioma, List<Pais>> Paises { get; set; } = new Dictionary<Idioma, List<Pais>>();
        public Dictionary<long, UF> UFs { get; set; } = new Dictionary<long, UF>();
        public List<Mesorregiao> Mesorregioes { get; set; } = new List<Mesorregiao>();
        public List<Municipio> Municipios { get; set; } = new List<Municipio>();
    }
}
