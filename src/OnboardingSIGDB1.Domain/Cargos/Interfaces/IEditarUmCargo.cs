using OnboardingSIGDB1.Domain.Cargos.Dto;
using OnboardingSIGDB1.Domain.Cargos.Entidades;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Cargos.Interfaces
{
    public interface IEditarUmCargo
    {
        Task<Cargo> EditarAsync(CargoDto cargoDto);
    }
}
