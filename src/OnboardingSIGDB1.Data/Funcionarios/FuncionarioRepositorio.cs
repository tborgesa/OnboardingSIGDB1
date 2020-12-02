using Microsoft.EntityFrameworkCore;
using OnboardingSIGDB1.Data._Base;
using OnboardingSIGDB1.Domain.Funcionarios.Entidades;
using OnboardingSIGDB1.Domain.Funcionarios.Interfaces;
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
    }
}
