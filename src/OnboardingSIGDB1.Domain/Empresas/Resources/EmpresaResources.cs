using OnboardingSIGDB1.Domain._Base.Resources;

namespace OnboardingSIGDB1.Domain.Empresas.Resources
{
    public static class EmpresaResources
    {
        public static string DataDeFundacao = "Data de Fundação";
        public const string Empresa = "Empresa";
        public const string Cnpj = "CNPJ";

        public const string ExisteFuncionarioVinculadoNaEmpresa = "Esta empresa não pode ser excluída, pois existe funcionário vinculado.";

        public static string EmpresaNaoExiste => Resource.FormatarResource(
                             Resource.MensagemNaoExisteNoBancoDeDadosFeminino, Empresa);
    }
}
