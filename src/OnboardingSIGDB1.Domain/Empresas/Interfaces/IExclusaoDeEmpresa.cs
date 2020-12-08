using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Empresas.Interfaces
{
    public interface IExclusaoDeEmpresa
    {
        Task ExcluirAsync(int empresaId);
    }
}
