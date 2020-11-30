using OnboardingSIGDB1.Domain.Cargos.Dto;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Cargos.Interfaces
{
    internal interface IArmazenadorDeCargo
    {
        Task ArmazenarAsync(CargoDto cargoDto);
    }
}