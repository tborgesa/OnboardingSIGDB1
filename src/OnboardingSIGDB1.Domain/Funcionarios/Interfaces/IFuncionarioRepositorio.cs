using OnboardingSIGDB1.Domain._Base.Interfaces;
using OnboardingSIGDB1.Domain.Funcionarios.Entidades;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Funcionarios.Interfaces
{
    public interface IFuncionarioRepositorio : IRepositorioBase<int, Funcionario>
    {
        Task<Funcionario> ObterPorCpfAsync(string cpf);
    }
}
