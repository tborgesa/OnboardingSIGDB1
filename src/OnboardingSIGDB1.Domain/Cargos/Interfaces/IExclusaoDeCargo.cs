using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Cargos.Interfaces
{
    public interface IExclusaoDeCargo
    {
        Task ExcluirAsync(int cargoId);
    }
}
