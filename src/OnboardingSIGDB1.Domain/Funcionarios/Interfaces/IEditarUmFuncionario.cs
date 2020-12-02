using OnboardingSIGDB1.Domain.Funcionarios.Dto;
using OnboardingSIGDB1.Domain.Funcionarios.Entidades;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Funcionarios.Interfaces
{
    public interface IEditarUmFuncionario
    {
        Task<Funcionario> EditarAsync(FuncionarioDto funcionarioDto);
    }
}
