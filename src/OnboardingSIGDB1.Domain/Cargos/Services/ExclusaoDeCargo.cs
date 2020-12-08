using OnboardingSIGDB1.Domain._Base.Interfaces;
using OnboardingSIGDB1.Domain._Base.Services;
using OnboardingSIGDB1.Domain.Cargos.Interfaces;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Cargos.Services
{
    public class ExclusaoDeCargo : OnboardingSIGDB1Service, IExclusaoDeCargo
    {
        private readonly ICargoRepositorio _cargoRepositorio;
        private readonly IValidadorDeExclusaoDeCargo _validadorDeExclusaoDeCargo;

        public ExclusaoDeCargo(
            IDomainNotificationHandler notificacaoDeDominio,
            ICargoRepositorio cargoRepositorio,
            IValidadorDeExclusaoDeCargo validadorDeExclusaoDeCargo) : base(notificacaoDeDominio)
        {
            _cargoRepositorio = cargoRepositorio;
            _validadorDeExclusaoDeCargo = validadorDeExclusaoDeCargo;
        }

        public async Task ExcluirAsync(int cargoId)
        {
            await _validadorDeExclusaoDeCargo.ValidarAsync(cargoId);

            if (NotificacaoDeDominio.HasNotifications)
                return;

            var cargo = await _cargoRepositorio.ObterPorIdAsync(cargoId);
            _cargoRepositorio.Remover(cargo);
        }
    }
}
