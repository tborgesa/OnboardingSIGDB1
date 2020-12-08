using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Funcionarios.Interfaces
{
    public interface IValidadorCpfDeFuncionarioJaExistente
    {
        Task<bool> ValidarAsync(string cpf, int? id = null);
    }
}
