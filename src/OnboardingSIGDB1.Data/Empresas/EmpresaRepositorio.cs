using Microsoft.EntityFrameworkCore;
using OnboardingSIGDB1.Data._Base;
using OnboardingSIGDB1.Domain.Empresas.Entidades;
using OnboardingSIGDB1.Domain.Empresas.Interfaces;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Data.Empresas
{
    public class EmpresaRepositorio : CadastroCompletoRepositorioBase<int, Empresa>, IEmpresaRepositorio
    {
        public EmpresaRepositorio(DbContext context) : base(context)
        {
        }

        public Task<Empresa> ObterPorCnpjAsync(string cnpj)
        {
            return DbSet.FirstOrDefaultAsync(_ => _.Cnpj == cnpj);
        }
    }
}
