using OnboardingSIGDB1.Domain.Empresas.Dto;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Empresas.Interfaces
{
    public interface IArmazenadorDeEmpresa
    {
        Task ArmazenarAsync(EmpresaDto empresaDto);
    }
}