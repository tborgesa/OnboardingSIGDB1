using OnboardingSIGDB1.Domain._Base.Interfaces;
using OnboardingSIGDB1.Domain.Empresas.Entidades;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Empresas.Interfaces
{
    public interface IEmpresaRepositorio : IRepositorioBase<int,Empresa>
    {
        Task<Empresa> ObterPorCnpjAsync(string cnpj);
    }
}
