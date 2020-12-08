using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Funcionarios.Interfaces
{
    public interface IExclusaoDeFuncionario
    {
        Task ExcluirAsync(int idFuncionario);
    }
}
