using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Cargos.Interfaces
{
    public interface IValidadorDeExclusaoDeCargo
    {
        Task ValidarAsync(int cargoId);
    }
}
