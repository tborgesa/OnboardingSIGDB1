using OnboardingSIGDB1.Domain._Base.Interfaces;
using OnboardingSIGDB1.Domain._Base.Resources;
using OnboardingSIGDB1.Domain._Base.Services;
using OnboardingSIGDB1.Domain.Cargos.Dto;
using OnboardingSIGDB1.Domain.Cargos.Entidades;
using OnboardingSIGDB1.Domain.Cargos.Interfaces;
using OnboardingSIGDB1.Domain.Cargos.Resources;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain.Cargos.Services
{
    public class EditarUmCargo : OnboardingSIGDB1Service, IEditarUmCargo
    {
        private readonly ICargoRepositorio _cargoRepositorio;

        public EditarUmCargo(IDomainNotificationHandler notificacaoDeDominio, 
            ICargoRepositorio cargoRepositorio) : base(notificacaoDeDominio)
        {
            _cargoRepositorio = cargoRepositorio;
        }

        public async Task<Cargo> EditarAsync(CargoDto cargoDto)
        {
            var cargo = await _cargoRepositorio.ObterPorIdAsync(cargoDto.Id);

            if (cargo == null)
                await NotificacaoDeDominio.HandleNotificacaoDeDominioAsync(
                         Resource.FormatarResource(
                             Resource.MensagemNaoExisteNoBancoDeDadosMasculino, CargoResources.Cargo)
                         );

            cargo?.AlterarDescricao(cargoDto.Descricao);

            return cargo;
        }
    }
}
