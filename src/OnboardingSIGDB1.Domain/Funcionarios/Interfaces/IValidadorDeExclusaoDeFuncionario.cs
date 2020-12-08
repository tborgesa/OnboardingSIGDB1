using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Funcionarios.Interfaces
{
    public interface IValidadorDeExclusaoDeFuncionario
    {
        Task ValidarAsync(int funcionarioId);
    }
}
