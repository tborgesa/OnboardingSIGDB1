using Microsoft.EntityFrameworkCore;
using OnboardingSIGDB1.Data._Base;
using OnboardingSIGDB1.Domain._Base.Helpers;
using OnboardingSIGDB1.Domain.Funcionarios.Dto;
using OnboardingSIGDB1.Domain.Funcionarios.Entidades;
using OnboardingSIGDB1.Domain.Funcionarios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Data.Funcionarios
{
    public class FuncionarioRepositorio : CadastroCompletoRepositorioBase<int, Funcionario>, IFuncionarioRepositorio
    {
        public FuncionarioRepositorio(DbContext context) : base(context)
        {
        }

        public Task<Funcionario> ObterPorCpfAsync(string cpf)
        {
            return DbSet.FirstOrDefaultAsync(_ => _.Cpf == cpf);
        }

        public async Task<IEnumerable<FuncionarioComEmpresaECargoDto>> ObterComFiltroAsync(
            IEnumerable<Expression<Func<Funcionario, bool>>> filtros)
        {
            return await DbSet.
                FiltrarUmaListaDeWhere(filtros).
                Select(_ => new FuncionarioComEmpresaECargoDto
                {
                    Id = _.Id,
                    Nome = _.Nome,
                    Cpf = _.Cpf,
                    DataDeContratacao = _.DataDeContratacao,
                    Empresa = _.Empresa == null ? null : _.Empresa.Nome,
                    Cargo = _.ListaDeCargos.OrderByDescending(_1 => _1.DataDeVinculo).FirstOrDefault().Cargo.Descricao                  
                }).
                ToListAsync();
        }

        public async Task AdicionarCargoParaFuncionarioAsync(CargoDoFuncionario cargoDoFuncionario)
        {
            await Context.Set<CargoDoFuncionario>().AddAsync(cargoDoFuncionario);
        }

       
    }
}
