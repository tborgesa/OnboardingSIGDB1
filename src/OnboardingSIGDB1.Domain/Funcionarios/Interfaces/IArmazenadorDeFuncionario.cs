using OnboardingSIGDB1.Domain.Funcionarios.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Funcionarios.Interfaces
{
    public interface IArmazenadorDeFuncionario
    {
        Task ArmazenarAsync(FuncionarioDto funcionarioDto);
    }
}
