using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Empresas.Interfaces
{
    public interface IValidadorDeExclusaoDeEmpresa
    {
        Task ValidarAsync(int empresaId);
    }
}
