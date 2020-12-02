using Moq;
using OnboardingSIGDB1.Domain._Base.Interfaces;
using OnboardingSIGDB1.Domain._Base.Resources;
using OnboardingSIGDB1.Domain.Cargos.Dto;
using OnboardingSIGDB1.Domain.Cargos.Interfaces;
using OnboardingSIGDB1.Domain.Cargos.Services;
using OnboardingSIGDB1.Domain.Test._Builders;
using OnboardingSIGDB1.Domain.Test._Comum;
using System.Threading.Tasks;
using Xunit;

namespace OnboardingSIGDB1.Domain.Test.Cargos
{
    public class EditarUmCargoTestes
    {
        private readonly OnboardingSIGDB1Faker _onboardingSIGDB1faker;
        private readonly int _id;
        private readonly CargoDto _cargoDto;

        private readonly Mock<IDomainNotificationHandler> _notificacaoDeDominioMock;
        private readonly Mock<ICargoRepositorio> _cargoRepositorioMock;
        private readonly EditarUmCargo _editarUmCargo;

        public EditarUmCargoTestes()
        {
            _onboardingSIGDB1faker = OnboardingSIGDB1FakerBuilder.Novo().Build();

            _id = _onboardingSIGDB1faker.Id();
            _cargoDto = new CargoDto
            {
                Descricao = _onboardingSIGDB1faker.FraseComQuantidadeExataDeCaracteres(Constantes.Numero250)
            };

            _notificacaoDeDominioMock = new Mock<IDomainNotificationHandler>();
            _cargoRepositorioMock = new Mock<ICargoRepositorio>();

            _editarUmCargo = new EditarUmCargo(
                _notificacaoDeDominioMock.Object,
                _cargoRepositorioMock.Object
                );
        }

        [Fact]
        public async Task DeveEditarADescricaoDoCargo()
        {
            var descricaoInicial = _onboardingSIGDB1faker.FraseComQuantidadeExataDeCaracteres(Constantes.Numero250);
            var cargoDoBancoDeDados = CargoBuilder.Novo().ComId(_id).Build();
            _cargoDto.Id = _id;

            _cargoRepositorioMock.Setup(_ => _.ObterPorIdAsync(_id)).ReturnsAsync(cargoDoBancoDeDados);

            await _editarUmCargo.EditarAsync(_cargoDto);

            Assert.NotEqual(cargoDoBancoDeDados.Descricao, descricaoInicial);
            Assert.Equal(cargoDoBancoDeDados.Descricao, _cargoDto.Descricao);
        }

        [Fact]
        public async Task DeveNotificarErroDeServicoAoInformarUmIdNaEdicaoQueNaoExisteNaBase()
        {
            _cargoDto.Id = _id;

            await _editarUmCargo.EditarAsync(_cargoDto);

            _notificacaoDeDominioMock.Verify(_ => _.HandleNotificacaoDeDominioAsync(It.IsAny<string>()));
        }
    }
}
