using Moq;
using OnboardingSIGDB1.Domain._Base.Interfaces;
using OnboardingSIGDB1.Domain.Empresas.Interfaces;
using OnboardingSIGDB1.Domain.Empresas.Resources;
using OnboardingSIGDB1.Domain.Funcionarios.Interfaces;
using OnboardingSIGDB1.Domain.Funcionarios.Resources;
using OnboardingSIGDB1.Domain.Funcionarios.Services;
using OnboardingSIGDB1.Domain.Test._Builders;
using OnboardingSIGDB1.Domain.Test._Comum;
using System.Threading.Tasks;
using Xunit;

namespace OnboardingSIGDB1.Domain.Test.Funcionarios
{
    public class VinculadorDeFuncionarioNaEmpresaTestes
    {
        private readonly OnboardingSIGDB1Faker _onboardingSIGDB1faker;
        private readonly int _idFuncionario;
        private readonly int _idEmpresa;

        private readonly Mock<IDomainNotificationHandler> _notificacaoDeDominioMock;
        private readonly Mock<IFuncionarioRepositorio> _funcionarioRepositorioMock;
        private readonly Mock<IEmpresaRepositorio> _empresaRepositorioMock;

        private readonly VinculadorDeFuncionarioNaEmpresa _vinculadorDeFuncionarioNaEmpresa;

        public VinculadorDeFuncionarioNaEmpresaTestes()
        {
            _onboardingSIGDB1faker = OnboardingSIGDB1FakerBuilder.Novo().Build();
            _idFuncionario = _onboardingSIGDB1faker.Id();
            _idEmpresa = _onboardingSIGDB1faker.Id();

            _notificacaoDeDominioMock = new Mock<IDomainNotificationHandler>();
            _funcionarioRepositorioMock = new Mock<IFuncionarioRepositorio>();
            _empresaRepositorioMock = new Mock<IEmpresaRepositorio>();

            _vinculadorDeFuncionarioNaEmpresa = new VinculadorDeFuncionarioNaEmpresa(
                _notificacaoDeDominioMock.Object,
                _funcionarioRepositorioMock.Object,
                _empresaRepositorioMock.Object
                );
        }


        [Fact]
        public async Task DeveVincularFuncionarioNaEmpresa()
        {
            var funcionario = FuncionarioBuilder.Novo().ComId(_idFuncionario).Build();
            var empresa = EmpresaBuilder.Novo().ComId(_idEmpresa).Build();

            _funcionarioRepositorioMock.Setup(_ => _.ObterPorIdAsync(_idFuncionario)).ReturnsAsync(funcionario);
            _empresaRepositorioMock.Setup(_ => _.ObterPorIdAsync(_idEmpresa)).ReturnsAsync(empresa);

            await _vinculadorDeFuncionarioNaEmpresa.Vincular(_idFuncionario, _idEmpresa);

            Assert.Equal(funcionario.EmpresaId, empresa.Id);
            Assert.Equal(funcionario.Empresa, empresa);
        }

        [Fact]
        public async Task QuandoFuncionarioNaoExisteDeveNotificarErroDeServico()
        {
            await _vinculadorDeFuncionarioNaEmpresa.Vincular(_idFuncionario, _idEmpresa);

            _notificacaoDeDominioMock.Verify(_ => _.HandleNotificacaoDeServicoAsync(It.Is<string>(_1 => _1 == FuncionarioResources.FuncionarioNaoExiste)));
        }

        [Fact]
        public async Task QuandoFuncionarioJaVinculadoEmUmaEmpresaDeveNotificarErroDeDominio()
        {
            var empresa = EmpresaBuilder.Novo().ComId(_idEmpresa).Build();
            var funcionario = FuncionarioBuilder.Novo().ComId(_idFuncionario).ComEmpresa(empresa).Build();

            _funcionarioRepositorioMock.Setup(_ => _.ObterPorIdAsync(_idFuncionario)).ReturnsAsync(funcionario);

            await _vinculadorDeFuncionarioNaEmpresa.Vincular(_idFuncionario, _idEmpresa);

            _notificacaoDeDominioMock.Verify(_ => _.HandleNotificacaoDeDominioAsync
            (It.Is<string>(_1 => _1 == FuncionarioResources.FuncionarioJaEstaVinculadoAEmpresa)));
        }

        [Fact]
        public async Task QuandoEmpresaNaoExisteDeveNotificarErroDeServico()
        {
            var funcionario = FuncionarioBuilder.Novo().ComId(_idFuncionario).Build();

            _funcionarioRepositorioMock.Setup(_ => _.ObterPorIdAsync(_idFuncionario)).ReturnsAsync(funcionario);

            await _vinculadorDeFuncionarioNaEmpresa.Vincular(_idFuncionario, _idEmpresa);

            _notificacaoDeDominioMock.Verify(_ => _.HandleNotificacaoDeServicoAsync(It.Is<string>(_1 => _1 == EmpresaResources.EmpresaNaoExiste)));
        }

    }
}
