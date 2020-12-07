using OnboardingSIGDB1.Domain._Base.Resources;

namespace OnboardingSIGDB1.Domain.Empresas.Resources
{
    public static class EmpresaResources
    {
        public static string DataDeFundacao = "Data de Fundação";
        public const string Empresa = "Empresa";
        public const string Cnpj = "CNPJ";

        public static string EmpresaNaoExiste => Resource.FormatarResource(
                             Resource.MensagemNaoExisteNoBancoDeDadosFeminino, Empresa);
    }
}
