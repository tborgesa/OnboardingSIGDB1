using OnboardingSIGDB1.Domain.Funcionarios.Entidades;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Funcionarios.Interfaces
{
    public interface IFuncionarioRepositorio
    {
        Task<Funcionario> ObterPorCpfAsync(string cpf);
    }
}
