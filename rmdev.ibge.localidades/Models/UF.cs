namespace rmdev.ibge.localidades
{
    public class UF
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public Macrorregiao Regiao { get; set; }
    }
}