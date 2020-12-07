using OnboardingSIGDB1.Domain._Base.Interfaces;
using OnboardingSIGDB1.Domain.Funcionarios.Entidades;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Funcionarios.Interfaces
{
    public interface IFuncionarioRepositorio : ICadastroCompletoRepositorio<int, Funcionario>
    {
        Task<Funcionario> ObterPorCpfAsync(string cpf);
        Task<Funcionario> ObterFuncionariosComEmpresaECargosAsync(int funcionarioId);

        Task AdicionarCargoParaFuncionarioAsync(CargoDoFuncionario cargoDoFuncionario);
    }
}
