namespace rmdev.ibge.localidades
{
    public class Distrito
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public Municipio Municipio { get; set; }
    }
}