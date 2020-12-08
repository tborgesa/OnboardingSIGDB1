using Moq;
using OnboardingSIGDB1.Domain._Base.Interfaces;
using OnboardingSIGDB1.Domain._Base.Resources;
using OnboardingSIGDB1.Domain.Empresas.Entidades;
using OnboardingSIGDB1.Domain.Empresas.Interfaces;
using OnboardingSIGDB1.Domain.Empresas.Services;
using OnboardingSIGDB1.Domain.Test._Builders;
using OnboardingSIGDB1.Domain.Test._Comum;
using System.Threading.Tasks;
using Xunit;

namespace OnboardingSIGDB1.Domain.Test.Empresas
{
    public class ExclusaoDeEmpresaTestes
    {
        private readonly int _empresaId;

        private readonly Mock<IDomainNotificationHandler> _notificacaoDeDominioMock;
        private readonly Mock<IEmpresaRepositorio> _empresaRepositorioMock;
        private readonly Mock<IValidadorDeExclusaoDeEmpresa> _validadorDeExclusaoDeEmpresaMock;
        private readonly ExclusaoDeEmpresa _exclusaoDeEmpresa;

        public ExclusaoDeEmpresaTestes()
        {
            var onboardingSIGDB1faker = OnboardingSIGDB1FakerBuilder.Novo().Build();
            _empresaId = onboardingSIGDB1faker.Id();

            _notificacaoDeDominioMock = new Mock<IDomainNotificationHandler>();
            _empresaRepositorioMock = new Mock<IEmpresaRepositorio>();
            _validadorDeExclusaoDeEmpresaMock = new Mock<IValidadorDeExclusaoDeEmpresa>();

            _exclusaoDeEmpresa = new ExclusaoDeEmpresa(
                _notificacaoDeDominioMock.Object,
                _empresaRepositorioMock.Object,
                _validadorDeExclusaoDeEmpresaMock.Object
                );
        }

        [Fact]
        public async Task DeveExcluirAEmpresa()
        {
            var empresa = EmpresaBuilder.Novo().ComId(_empresaId).Build();
            _empresaRepositorioMock.Setup(_ => _.ObterPorIdAsync(_empresaId)).ReturnsAsync(empresa);

            await _exclusaoDeEmpresa.ExcluirAsync(_empresaId);

            _empresaRepositorioMock.Verify(_ => _.Remover(It.Is<Empresa>(
                _1 => _1.Id == _empresaId
                )));
        }

        [Fact]
        public async Task NaoDeveExcluirQuandoExistirNotificacaoDeDominio()
        {
            _notificacaoDeDominioMock.Setup(_ => _.HasNotifications).Returns(Constantes.Verdadeiro);

            await _exclusaoDeEmpresa.ExcluirAsync(_empresaId);

            _empresaRepositorioMock.Verify(_ => _.Remover(It.IsAny<Empresa>()), Times.Never);
        }

        [Fact]
        public async Task DeveValidarExclusaoAntesDeExcluir()
        {
            await _exclusaoDeEmpresa.ExcluirAsync(_empresaId);

            _validadorDeExclusaoDeEmpresaMock.Verify(_ => _.ValidarAsync(It.Is<int>(
                _1 => _1 == _empresaId
                )));
        }
    }
}