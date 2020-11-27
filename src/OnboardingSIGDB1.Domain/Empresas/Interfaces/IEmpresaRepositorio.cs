using OnboardingSIGDB1.Domain.Empresas.Entidades;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Empresas.Interfaces
{
    public interface IEmpresaRepositorio
    {
        Task<Empresa> ObterPorCnpjAsync(string cnpj);
    }
}
