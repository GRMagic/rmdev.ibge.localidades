using System.Globalization;
using System.Text;

namespace rmdev.ibge.localidades
{
    public static class StringExtensions
    {
        public static string NormalizarNomeCidade(this string nome)
        {
            string resultado = string.Empty;
            nome = nome.Trim().Normalize(NormalizationForm.FormKD);
            foreach(var caractere in nome)
            {
                var categoria = CharUnicodeInfo.GetUnicodeCategory(caractere);
                switch(categoria)
                {
                    case UnicodeCategory.UppercaseLetter:
                    case UnicodeCategory.LowercaseLetter:
                        resultado += caractere;
                        break;
                    case UnicodeCategory.SpaceSeparator:
                        resultado += '-';
                        break;
                }
            }
            return resultado;
        }
    }
}