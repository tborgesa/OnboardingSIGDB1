using Moq;
using OnboardingSIGDB1.Domain._Base.Interfaces;
using OnboardingSIGDB1.Domain._Base.Resources;
using OnboardingSIGDB1.Domain.Funcionarios.Entidades;
using OnboardingSIGDB1.Domain.Funcionarios.Interfaces;
using OnboardingSIGDB1.Domain.Funcionarios.Services;
using OnboardingSIGDB1.Domain.Test._Builders;
using OnboardingSIGDB1.Domain.Test._Comum;
using System.Threading.Tasks;
using Xunit;

namespace OnboardingSIGDB1.Domain.Test.Funcionarios
{
    public class ExclusaoDeFuncionarioTestes
    {
        private readonly int _funcionarioId;

        private readonly Mock<IDomainNotificationHandler> _notificacaoDeDominioMock;
        private readonly Mock<IFuncionarioRepositorio> _funcionarioRepositorioMock;
        private readonly Mock<IValidadorDeExclusaoDeFuncionario> _validadorDeExclusaoDeFuncionarioMock;
        private readonly ExclusaoDeFuncionario _exclusaoDeFuncionario;

        public ExclusaoDeFuncionarioTestes()
        {
            var onboardingSIGDB1faker = OnboardingSIGDB1FakerBuilder.Novo().Build();
            _funcionarioId = onboardingSIGDB1faker.Id();

            _notificacaoDeDominioMock = new Mock<IDomainNotificationHandler>();
            _funcionarioRepositorioMock = new Mock<IFuncionarioRepositorio>();
            _validadorDeExclusaoDeFuncionarioMock = new Mock<IValidadorDeExclusaoDeFuncionario>();

            _exclusaoDeFuncionario = new ExclusaoDeFuncionario(
                _notificacaoDeDominioMock.Object,
                _funcionarioRepositorioMock.Object,
                _validadorDeExclusaoDeFuncionarioMock.Object
                );
        }

        [Fact]
        public async Task DeveExcluirFuncionario()
        {
            var funcionario = FuncionarioBuilder.Novo().ComId(_funcionarioId).Build();
            _funcionarioRepositorioMock.Setup(_ => _.ObterPorIdAsync(_funcionarioId)).ReturnsAsync(funcionario);

            await _exclusaoDeFuncionario.ExcluirAsync(_funcionarioId);

            _funcionarioRepositorioMock.Verify(_ => _.Remover(It.Is<Funcionario>(
                _1 => _1.Id == _funcionarioId
                )));
        }

        [Fact]
        public async Task NaoDeveExcluirQuandoExistirNotificacaoDeDominio()
        {
            _notificacaoDeDominioMock.Setup(_ => _.HasNotifications).Returns(Constantes.Verdadeiro);

            await _exclusaoDeFuncionario.ExcluirAsync(_funcionarioId);

            _funcionarioRepositorioMock.Verify(_ => _.Remover(It.IsAny<Funcionario>()), Times.Never);
        }

        [Fact]
        public async Task DeveValidarExclusaoAntesDeExcluir()
        {
            await _exclusaoDeFuncionario.ExcluirAsync(_funcionarioId);

            _validadorDeExclusaoDeFuncionarioMock.Verify(_ => _.ValidarAsync(It.Is<int>(
                _1 => _1 == _funcionarioId
                )));
        }
    }
}