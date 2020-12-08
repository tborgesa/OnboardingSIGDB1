using Moq;
using OnboardingSIGDB1.Domain._Base.Interfaces;
using OnboardingSIGDB1.Domain._Base.Resources;
using OnboardingSIGDB1.Domain.Cargos.Entidades;
using OnboardingSIGDB1.Domain.Cargos.Interfaces;
using OnboardingSIGDB1.Domain.Cargos.Services;
using OnboardingSIGDB1.Domain.Test._Builders;
using OnboardingSIGDB1.Domain.Test._Comum;
using System.Threading.Tasks;
using Xunit;

namespace OnboardingSIGDB1.Domain.Test.Cargos
{
    public class ExclusaoDeCargoTestes
    {
        private readonly int _cargoId;

        private readonly Mock<IDomainNotificationHandler> _notificacaoDeDominioMock;
        private readonly Mock<ICargoRepositorio> _cargoRepositorioMock;
        private readonly Mock<IValidadorDeExclusaoDeCargo> _validadorDeExclusaoDeCargoMock;
        private readonly ExclusaoDeCargo _exclusaoDeCargo;

        public ExclusaoDeCargoTestes()
        {
            var onboardingSIGDB1faker = OnboardingSIGDB1FakerBuilder.Novo().Build();
            _cargoId = onboardingSIGDB1faker.Id();

            _notificacaoDeDominioMock = new Mock<IDomainNotificationHandler>();
            _cargoRepositorioMock = new Mock<ICargoRepositorio>();
            _validadorDeExclusaoDeCargoMock = new Mock<IValidadorDeExclusaoDeCargo>();

            _exclusaoDeCargo = new ExclusaoDeCargo(
                _notificacaoDeDominioMock.Object,
                _cargoRepositorioMock.Object,
                _validadorDeExclusaoDeCargoMock.Object
                );
        }

        [Fact]
        public async Task DeveExcluiroCargo()
        {
            var cargo = CargoBuilder.Novo().ComId(_cargoId).Build();
            _cargoRepositorioMock.Setup(_ => _.ObterPorIdAsync(_cargoId)).ReturnsAsync(cargo);

            await _exclusaoDeCargo.ExcluirAsync(_cargoId);

            _cargoRepositorioMock.Verify(_ => _.Remover(It.Is<Cargo>(
                _1 => _1.Id == _cargoId
                )));
        }

        [Fact]
        public async Task NaoDeveExcluirQuandoExistirNotificacaoDeDominio()
        {
            _notificacaoDeDominioMock.Setup(_ => _.HasNotifications).Returns(Constantes.Verdadeiro);

            await _exclusaoDeCargo.ExcluirAsync(_cargoId);

            _cargoRepositorioMock.Verify(_ => _.Remover(It.IsAny<Cargo>()), Times.Never);
        }

        [Fact]
        public async Task DeveValidarExclusaoAntesDeExcluir()
        {
            await _exclusaoDeCargo.ExcluirAsync(_cargoId);

            _validadorDeExclusaoDeCargoMock.Verify(_ => _.ValidarAsync(It.Is<int>(
                _1 => _1 == _cargoId
                )));
        }
    }
}