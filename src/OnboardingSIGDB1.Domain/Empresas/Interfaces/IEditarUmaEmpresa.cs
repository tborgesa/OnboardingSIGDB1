using OnboardingSIGDB1.Domain.Empresas.Dto;
using OnboardingSIGDB1.Domain.Empresas.Entidades;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Empresas.Interfaces
{
    public interface IEditarUmaEmpresa
    {
        Task<Empresa> EditarAsync(EmpresaDto empresaDto);
    }
}
