using OnboardingSIGDB1.Domain._Base.Resources;

namespace OnboardingSIGDB1.Domain.Funcionarios.Resources
{
    public static class FuncionarioResources
    {
        public const string DataDeContratacao = "Data de Contratação";
        public const string DataDeVinculo = "Data de Vínculo";
        public const string Funcionario = "Funcionário";
        public const string Cpf = "CPF";

        public const string FuncionarioJaEstaVinculadoAEmpresa = "O funcionário já tem vínculo com uma empresa e não pode ser vinculado novamente.";

        public static string FuncionarioNaoExiste => Resource.FormatarResource(
                             Resource.MensagemNaoExisteNoBancoDeDadosMasculino, Funcionario);
    }
}
