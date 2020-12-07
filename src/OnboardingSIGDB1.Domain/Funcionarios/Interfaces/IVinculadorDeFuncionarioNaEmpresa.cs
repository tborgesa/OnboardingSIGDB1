using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Funcionarios.Interfaces
{
    public interface IVinculadorDeFuncionarioNaEmpresa
    {
        Task Vincular(int funcionarioId, int empresaId);
    }
}
