using OnboardingSIGDB1.Domain._Base.Resources;

namespace OnboardingSIGDB1.Domain.Cargos.Resources
{
    public static class CargoResources
    {
        public const string Cargo = "Cargo";

        public static string CargoNaoExiste => Resource.FormatarResource(
                             Resource.MensagemNaoExisteNoBancoDeDadosMasculino, Cargo);

        public static string ExisteFuncionarioVinculadoNoCargo = "Este cargo não pode ser excluída, pois existe funcionário vinculado.";
    }
}
