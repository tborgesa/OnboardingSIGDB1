using OnboardingSIGDB1.Domain._Base.Interfaces;
using OnboardingSIGDB1.Domain._Base.Services;
using OnboardingSIGDB1.Domain.Cargos.Dto;
using OnboardingSIGDB1.Domain.Cargos.Entidades;
using OnboardingSIGDB1.Domain.Cargos.Interfaces;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Cargos.Services
{
    public class ArmazenadorDeCargo : OnboardingSIGDB1Service, IArmazenadorDeCargo
    {
        private readonly ICargoRepositorio _cargoRepositorio;
        private readonly IEditarUmCargo _editarUmCargo;

        public ArmazenadorDeCargo(
            IDomainNotificationHandler notificacaoDeDominio,
            ICargoRepositorio cargoRepositorio,
            IEditarUmCargo editarUmCargo) : base(notificacaoDeDominio)
        {
            _cargoRepositorio = cargoRepositorio;
            _editarUmCargo = editarUmCargo;
        }

        public async Task ArmazenarAsync(CargoDto cargoDto)
        {
            cargoDto = cargoDto ?? new CargoDto();

            var cargo = cargoDto.Id == 0 ?
                CriarUmNovoCargo(cargoDto) :
                await _editarUmCargo.EditarAsync(cargoDto);

            if (NotificacaoDeDominio.HasNotifications)
                return;

            if (!cargo.Validar())
                await NotificarValidacoesDeDominioAsync(cargo.ValidationResult);

            if (!NotificacaoDeDominio.HasNotifications && cargo.Id == 0)
                await _cargoRepositorio.AdicionarAsync(cargo);
        }

        private Cargo CriarUmNovoCargo(CargoDto cargoDto)
        {
            return new Cargo(cargoDto.Descricao);
        }
    }
}
