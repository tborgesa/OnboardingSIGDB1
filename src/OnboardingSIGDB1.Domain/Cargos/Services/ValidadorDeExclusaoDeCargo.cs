using OnboardingSIGDB1.Domain._Base.Interfaces;
using OnboardingSIGDB1.Domain._Base.Services;
using OnboardingSIGDB1.Domain.Cargos.Interfaces;
using OnboardingSIGDB1.Domain.Cargos.Resources;
using System.Linq;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Cargos.Services
{
    public class ValidadorDeExclusaoDeCargo : OnboardingSIGDB1Service, IValidadorDeExclusaoDeCargo
    {
        private readonly ICargoRepositorio _cargoRepositorio;

        public ValidadorDeExclusaoDeCargo(
            IDomainNotificationHandler notificacaoDeDominio,
            ICargoRepositorio cargoRepositorio) : base(notificacaoDeDominio)
        {
            _cargoRepositorio = cargoRepositorio;
        }

        public async Task ValidarAsync(int cargoId)
        {
            var cargo = await _cargoRepositorio.ObterPorIdAsync(cargoId);

            if (cargo == null)
            {
                await NotificacaoDeDominio.HandleNotificacaoDeServicoAsync(CargoResources.CargoNaoExiste);
                return;
            }

            if (cargo.ListaDeFuncionarios.Any())
                await NotificacaoDeDominio.HandleNotificacaoDeServicoAsync(CargoResources.ExisteFuncionarioVinculadoNoCargo);
        }
    }
}
