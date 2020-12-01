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

        public ArmazenadorDeCargo(
            IDomainNotificationHandler notificacaoDeDominio,
            ICargoRepositorio cargoRepositorio
            ) : base(notificacaoDeDominio)
        {
            _cargoRepositorio = cargoRepositorio;
        }

        public async Task ArmazenarAsync(CargoDto cargoDto)
        {
            cargoDto = cargoDto ?? new CargoDto();

            var cargo = cargoDto.Id == 0 ?
                CriarUmNovoCargo(cargoDto) :
                await EditarUmCargoAsync(cargoDto);

            if (!cargo.Validar())
                await NotificarValidacoesDeDominioAsync(cargo.ValidationResult);

            if (!NotificacaoDeDominio.HasNotifications && cargo.Id == 0)
                await _cargoRepositorio.AdicionarAsync(cargo);
        }

        private async Task<Cargo> EditarUmCargoAsync(CargoDto cargoDto)
        {
            var cargo = await _cargoRepositorio.ObterPorIdAsync(cargoDto.Id);

            cargo.AlterarDescricao(cargoDto.Descricao);

            return cargo;
        }

        private Cargo CriarUmNovoCargo(CargoDto cargoDto)
        {
            return new Cargo(cargoDto.Descricao);
        }
    }
}
