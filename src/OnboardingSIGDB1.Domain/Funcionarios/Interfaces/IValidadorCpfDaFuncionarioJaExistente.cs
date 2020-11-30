using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Funcionarios.Interfaces
{
    interface IValidadorCpfDaFuncionarioJaExistente
    {
        Task<bool> ValidarAsync(string cpf, int? id = null);
    }
}
