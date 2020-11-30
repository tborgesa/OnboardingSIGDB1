using OnboardingSIGDB1.Domain.Funcionarios.Dto;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Funcionarios.Interfaces
{
    public interface IArmazenadorDeFuncionario
    {
        Task ArmazenarAsync(FuncionarioDto funcionarioDto);
    }
}
