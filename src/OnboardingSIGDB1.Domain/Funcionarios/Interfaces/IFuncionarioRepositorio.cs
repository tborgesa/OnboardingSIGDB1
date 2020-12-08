using OnboardingSIGDB1.Domain._Base.Interfaces;
using OnboardingSIGDB1.Domain.Funcionarios.Dto;
using OnboardingSIGDB1.Domain.Funcionarios.Entidades;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Funcionarios.Interfaces
{
    public interface IFuncionarioRepositorio : ICadastroCompletoRepositorio<int, Funcionario>
    {
        Task<Funcionario> ObterPorCpfAsync(string cpf);

        Task<IEnumerable<FuncionarioComEmpresaECargoDto>> ObterComFiltroAsync(
            IEnumerable<Expression<Func<Funcionario, bool>>> filtros);

        Task AdicionarCargoParaFuncionarioAsync(CargoDoFuncionario cargoDoFuncionario);
    }
}
