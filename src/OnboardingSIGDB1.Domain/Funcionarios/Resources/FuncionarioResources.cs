using OnboardingSIGDB1.Domain._Base.Resources;

namespace OnboardingSIGDB1.Domain.Funcionarios.Resources
{
    public static class FuncionarioResources
    {
        public const string DataDeContratacao = "Data de Contratação";
        public const string DataDeVinculo = "Data de Vínculo";
        public const string Funcionario = "Funcionário";
        public const string Cpf = "CPF";

        public const string FuncionarioJaEstaVinculadoAEmpresa = "Não é possível vincular o funcionário com a empresa pois já existe um vínculo para este funcionário.";

        public const string FuncionarioJaFoiVinculadoNoCargo = "Não é possível vincular o funcionário com o cargo pois esse funcionário já teve esse cargo anteriormente.";
        public const string FuncionarioSemEmpresaVinculado = "Não é possível vincular o funcionário com o cargo pois esse funcionário não está vinculado a nenhuma empresa.";

        public static string FuncionarioNaoExiste => Resource.FormatarResource(
                             Resource.MensagemNaoExisteNoBancoDeDadosMasculino, Funcionario);
    }
}
