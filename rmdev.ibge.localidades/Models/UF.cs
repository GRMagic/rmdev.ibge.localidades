namespace rmdev.ibge.localidades
{
    public class UF : Localidade
    {
        public string Sigla { get; set; }
        public Macrorregiao Regiao { get; set; }
    }
}