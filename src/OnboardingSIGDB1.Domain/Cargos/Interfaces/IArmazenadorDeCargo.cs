using OnboardingSIGDB1.Domain.Cargos.Dto;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Cargos.Interfaces
{
    public interface IArmazenadorDeCargo
    {
        Task ArmazenarAsync(CargoDto cargoDto);
    }
}