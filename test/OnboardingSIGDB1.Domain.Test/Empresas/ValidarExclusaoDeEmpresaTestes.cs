using Moq;
using OnboardingSIGDB1.Domain._Base.Interfaces;
using OnboardingSIGDB1.Domain.Empresas.Interfaces;
using OnboardingSIGDB1.Domain.Empresas.Resources;
using OnboardingSIGDB1.Domain.Empresas.Services;
using OnboardingSIGDB1.Domain.Test._Builders;
using OnboardingSIGDB1.Domain.Test._Comum;
using System.Threading.Tasks;
using Xunit;

namespace OnboardingSIGDB1.Domain.Test.Empresas
{
    public class ValidarExclusaoDeEmpresaTestes
    {
        private readonly OnboardingSIGDB1Faker _onboardingSIGDB1faker;
        private readonly int _empresaId;

        private readonly Mock<IDomainNotificationHandler> _notificacaoDeDominioMock;
        private readonly Mock<IEmpresaRepositorio> _empresaRepositorioMock;
        private readonly ValidadorDeExclusaoDeEmpresa _validadorDeExclusaoDeEmpresa;

        public ValidarExclusaoDeEmpresaTestes()
        {
            _onboardingSIGDB1faker = OnboardingSIGDB1FakerBuilder.Novo().Build();
            _empresaId = _onboardingSIGDB1faker.Id();

            _notificacaoDeDominioMock = new Mock<IDomainNotificationHandler>();
            _empresaRepositorioMock = new Mock<IEmpresaRepositorio>();

            _validadorDeExclusaoDeEmpresa = new ValidadorDeExclusaoDeEmpresa(
                _notificacaoDeDominioMock.Object,
                _empresaRepositorioMock.Object
                );
        }

        [Fact]
        public async Task DeveNotificarQuandoEmpresaNaoExistir()
        {
            await _validadorDeExclusaoDeEmpresa.ValidarAsync(_empresaId);

            _notificacaoDeDominioMock.Verify(_ => _.HandleNotificacaoDeServicoAsync(It.Is<string>(
                _1 => _1 == EmpresaResources.EmpresaNaoExiste
                )));
        }

        [Fact]
        public async Task DeveNotificarQuandoExisteFuncionarioVinculadoNaEmpresa()
        {
            var funcionario = FuncionarioBuilder.Novo().ComId(_onboardingSIGDB1faker.Id()).Build();
            var empresa = EmpresaBuilder.Novo().ComId(_empresaId).ComFuncionario(funcionario).Build();

            _empresaRepositorioMock.Setup(_ => _.ObterPorIdAsync(_empresaId)).ReturnsAsync(empresa);

            await _validadorDeExclusaoDeEmpresa.ValidarAsync(_empresaId);

            _notificacaoDeDominioMock.Verify(_ => _.HandleNotificacaoDeServicoAsync(It.Is<string>(
                _1 => _1 == EmpresaResources.ExisteFuncionarioVinculadoNaEmpresa
                )));
        }
    }
}
