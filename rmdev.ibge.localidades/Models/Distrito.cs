namespace rmdev.ibge.localidades
{
    public class Distrito
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Municipio Municipio { get; set; }
    }
}