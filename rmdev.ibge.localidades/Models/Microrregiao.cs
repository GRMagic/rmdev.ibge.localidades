namespace rmdev.ibge.localidades
{
    public class Microrregiao
    {
        public long Id { get; set; }

        public string Nome { get; set; }
        public Mesorregiao Mesorregiao { get; set; }
    }
}