using Moq;
using OnboardingSIGDB1.Domain._Base.Interfaces;
using OnboardingSIGDB1.Domain._Base.Resources;
using OnboardingSIGDB1.Domain.Cargos.Dto;
using OnboardingSIGDB1.Domain.Cargos.Entidades;
using OnboardingSIGDB1.Domain.Cargos.Interfaces;
using OnboardingSIGDB1.Domain.Cargos.Services;
using OnboardingSIGDB1.Domain.Test._Builders;
using OnboardingSIGDB1.Domain.Test._Comum;
using System.Threading.Tasks;
using Xunit;

namespace OnboardingSIGDB1.Domain.Test.Cargos
{
    public class ArmazenadorDeCargoTestes
    {
        private readonly OnboardingSIGDB1Faker _onboardingSIGDB1faker;
        private readonly int _id;
        private readonly CargoDto _cargoDto;

        private readonly Mock<IDomainNotificationHandler> _notificacaoDeDominioMock;
        private readonly Mock<IEditarUmCargo> _editarUmCargoMock;
        private readonly Mock<ICargoRepositorio> _cargoRepositorioMock;
        private readonly ArmazenadorDeCargo _armazenadorDeCargo;

        public ArmazenadorDeCargoTestes()
        {
            _onboardingSIGDB1faker = OnboardingSIGDB1FakerBuilder.Novo().Build();

            _id = _onboardingSIGDB1faker.Id();
            _cargoDto = new CargoDto
            {
                Descricao = _onboardingSIGDB1faker.FraseComQuantidadeExataDeCaracteres(Constantes.Numero250)
            };

            _notificacaoDeDominioMock = new Mock<IDomainNotificationHandler>();
            _cargoRepositorioMock = new Mock<ICargoRepositorio>();
            _editarUmCargoMock = new Mock<IEditarUmCargo>();

            _armazenadorDeCargo = new ArmazenadorDeCargo(
                _notificacaoDeDominioMock.Object,
                _cargoRepositorioMock.Object,
                _editarUmCargoMock.Object
                );
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task DeveValidarDominioNaInsercao(string descricaoInvalida)
        {
            _cargoDto.Descricao = descricaoInvalida;

            await _armazenadorDeCargo.ArmazenarAsync(_cargoDto);

            _notificacaoDeDominioMock.Verify(_ => _.HandleNotificacaoDeDominioAsync(It.IsAny<string>()));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task DeveValidarDominioNaEdicao(string descricaoInvalido)
        {
            _cargoDto.Descricao = descricaoInvalido;
            _cargoDto.Id = _id;

            MockarAEdicaoDoDto();

            await _armazenadorDeCargo.ArmazenarAsync(_cargoDto);

            _notificacaoDeDominioMock.Verify(_ => _.HandleNotificacaoDeDominioAsync(It.IsAny<string>()));
        }

        [Fact]
        public async Task DeveNotificarErroDeDominioParaDtoNull()
        {
            await _armazenadorDeCargo.ArmazenarAsync(null);

            _notificacaoDeDominioMock.Verify(_ => _.HandleNotificacaoDeDominioAsync(It.IsAny<string>()));
        }

        [Fact]
        public async Task DeveSalvar()
        {
            await _armazenadorDeCargo.ArmazenarAsync(_cargoDto);

            _cargoRepositorioMock.Verify(_ => _.AdicionarAsync(
                It.Is<Cargo>(_1 =>
                _1.Descricao == _cargoDto.Descricao
                )), Times.Once);
        }

        private void MockarAEdicaoDoDto()
        {
            var cargoDoBancoDeDados = CargoBuilder.Novo().
                ComId(_id).
                ComDescricao(_cargoDto.Descricao).
                Build();

            _editarUmCargoMock.Setup(_ => _.EditarAsync(_cargoDto))
                .ReturnsAsync(cargoDoBancoDeDados);
        }
    }
}
