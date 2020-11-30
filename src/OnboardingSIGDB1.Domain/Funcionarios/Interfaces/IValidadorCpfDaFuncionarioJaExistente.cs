using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Funcionarios.Interfaces
{
    public interface IValidadorCpfDaFuncionarioJaExistente
    {
        Task<bool> ValidarAsync(string cpf, int? id = null);
    }
}
