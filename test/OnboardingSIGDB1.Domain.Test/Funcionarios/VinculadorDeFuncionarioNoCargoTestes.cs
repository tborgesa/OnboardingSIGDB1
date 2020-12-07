using Moq;
using OnboardingSIGDB1.Domain._Base.Interfaces;
using OnboardingSIGDB1.Domain.Cargos.Entidades;
using OnboardingSIGDB1.Domain.Cargos.Interfaces;
using OnboardingSIGDB1.Domain.Funcionarios.Dto;
using OnboardingSIGDB1.Domain.Funcionarios.Entidades;
using OnboardingSIGDB1.Domain.Funcionarios.Interfaces;
using OnboardingSIGDB1.Domain.Funcionarios.Services;
using OnboardingSIGDB1.Domain.Test._Builders;
using OnboardingSIGDB1.Domain.Test._Comum;
using System.Threading.Tasks;
using Xunit;

namespace OnboardingSIGDB1.Domain.Test.Funcionarios
{
    public class VinculadorDeFuncionarioNoCargoTestes
    {
        private readonly OnboardingSIGDB1Faker _onboardingSIGDB1faker;

        private readonly int _funcionarioId;
        private readonly int _cargoId;

        private readonly Funcionario _funcionario;
        private readonly Cargo _cargo;
        private readonly CargoDoFuncionarioDto _cargoDoFuncionarioDto;

        private readonly Mock<IDomainNotificationHandler> _notificacaoDeDominioMock;
        private readonly Mock<IFuncionarioRepositorio> _funcionarioRepositorioMock;
        private readonly Mock<ICargoRepositorio> _cargoRepositorioMock;

        private readonly VinculadorDeFuncionarioNoCargo _vinculadorDeFuncionarioNoCargo;

        public VinculadorDeFuncionarioNoCargoTestes()
        {
            _funcionarioId = _onboardingSIGDB1faker.Id();
            _cargoId = _onboardingSIGDB1faker.Id();

            _onboardingSIGDB1faker = OnboardingSIGDB1FakerBuilder.Novo().Build();
            _cargoDoFuncionarioDto = new CargoDoFuncionarioDto()
            {
                FuncionarioId = _funcionarioId,
                CargoId = _cargoId,
                DataDeVinculo = _onboardingSIGDB1faker.QualquerDataDoUltimoAno()
            };

            var empresa = EmpresaBuilder.Novo().ComId(_onboardingSIGDB1faker.Id()).Build();
            _funcionario = FuncionarioBuilder.Novo().ComId(_funcionarioId).ComEmpresa(empresa).Build();
            _cargo = CargoBuilder.Novo().ComId(_cargoId).Build();

            _notificacaoDeDominioMock = new Mock<IDomainNotificationHandler>();
            _funcionarioRepositorioMock = new Mock<IFuncionarioRepositorio>();
            _cargoRepositorioMock = new Mock<ICargoRepositorio>();

            _vinculadorDeFuncionarioNoCargo = new VinculadorDeFuncionarioNoCargo(
                _notificacaoDeDominioMock.Object,
                _funcionarioRepositorioMock.Object,
                _cargoRepositorioMock.Object
                );
        }

        [Fact]
        public async Task DeveVincularFuncionarioNoCargo()
        {
            _funcionarioRepositorioMock.
                Setup(_ => _.ObterFuncionariosComEmpresaECargosAsync(_cargoDoFuncionarioDto.FuncionarioId)).
                ReturnsAsync(_funcionario);

            _cargoRepositorioMock.
                Setup(_ => _.ObterPorIdAsync(_cargoDoFuncionarioDto.CargoId)).
                ReturnsAsync(_cargo);

            await _vinculadorDeFuncionarioNoCargo.Vincular(_cargoDoFuncionarioDto);

            _funcionarioRepositorioMock.Verify(_ => _.AdicionarCargoParaFuncionarioAsync(It.Is<CargoDoFuncionario>(_1 =>
                _1.FuncionarioId == _cargoDoFuncionarioDto.FuncionarioId &&
                _1.CargoId == _cargoDoFuncionarioDto.CargoId &&
                _1.DataDeVinculo == _cargoDoFuncionarioDto.DataDeVinculo
             )));
        }

        [Fact]
        public async Task QuandoFuncionarioNaoExisteDeveNotificarErroDeDominio()
        {
            _cargoRepositorioMock.
                Setup(_ => _.ObterPorIdAsync(_cargoDoFuncionarioDto.CargoId)).
                ReturnsAsync(_cargo);

            await _vinculadorDeFuncionarioNoCargo.Vincular(_cargoDoFuncionarioDto);

            _notificacaoDeDominioMock.Verify(_ => _.HandleNotificacaoDeDominioAsync(It.IsAny<string>()));
        }

        [Fact]
        public async Task QuandoCargoNaoExisteDeveNotificarErroDeDominio()
        {
            _funcionarioRepositorioMock.
                Setup(_ => _.ObterFuncionariosComEmpresaECargosAsync(_cargoDoFuncionarioDto.FuncionarioId)).
                ReturnsAsync(_funcionario);

            await _vinculadorDeFuncionarioNoCargo.Vincular(_cargoDoFuncionarioDto);

            _notificacaoDeDominioMock.Verify(_ => _.HandleNotificacaoDeDominioAsync(It.IsAny<string>()));
        }
    }
}
