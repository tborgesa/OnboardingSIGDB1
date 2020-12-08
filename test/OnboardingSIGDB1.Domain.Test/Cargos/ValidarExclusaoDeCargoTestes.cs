using Moq;
using OnboardingSIGDB1.Domain._Base.Interfaces;
using OnboardingSIGDB1.Domain.Cargos.Interfaces;
using OnboardingSIGDB1.Domain.Cargos.Resources;
using OnboardingSIGDB1.Domain.Cargos.Services;
using OnboardingSIGDB1.Domain.Test._Builders;
using OnboardingSIGDB1.Domain.Test._Comum;
using System.Threading.Tasks;
using Xunit;

namespace OnboardingSIGDB1.Domain.Test.Cargos
{
    public class ValidarExclusaoDeCargoTestes
    {
        private readonly OnboardingSIGDB1Faker _onboardingSIGDB1faker;
        private readonly int _cargoId;

        private readonly Mock<IDomainNotificationHandler> _notificacaoDeDominioMock;
        private readonly Mock<ICargoRepositorio> _cargoRepositorioMock;
        private readonly ValidadorDeExclusaoDeCargo _validadorDeExclusaoDeCargo;

        public ValidarExclusaoDeCargoTestes()
        {
            _onboardingSIGDB1faker = OnboardingSIGDB1FakerBuilder.Novo().Build();
            _cargoId = _onboardingSIGDB1faker.Id();

            _notificacaoDeDominioMock = new Mock<IDomainNotificationHandler>();
            _cargoRepositorioMock = new Mock<ICargoRepositorio>();

            _validadorDeExclusaoDeCargo = new ValidadorDeExclusaoDeCargo(
                _notificacaoDeDominioMock.Object,
                _cargoRepositorioMock.Object
                );
        }

        [Fact]
        public async Task DeveNotificarQuandoCargoNaoExistir()
        {
            await _validadorDeExclusaoDeCargo.ValidarAsync(_cargoId);

            _notificacaoDeDominioMock.Verify(_ => _.HandleNotificacaoDeServicoAsync(It.Is<string>(
                _1 => _1 == CargoResources.CargoNaoExiste
                )));
        }

        [Fact]
        public async Task DeveNotificarQuandoExisteFuncionarioVinculadoNoCargo()
        {
            var funcionario = FuncionarioBuilder.Novo().ComId(_onboardingSIGDB1faker.Id()).Build();
            var cargo = CargoBuilder.Novo().ComId(_cargoId).ComFuncionario(funcionario).Build();

            _cargoRepositorioMock.Setup(_ => _.ObterPorIdAsync(_cargoId)).ReturnsAsync(cargo);

            await _validadorDeExclusaoDeCargo.ValidarAsync(_cargoId);

            _notificacaoDeDominioMock.Verify(_ => _.HandleNotificacaoDeServicoAsync(It.Is<string>(
                _1 => _1 == CargoResources.ExisteFuncionarioVinculadoNoCargo
                )));
        }
    }
}
