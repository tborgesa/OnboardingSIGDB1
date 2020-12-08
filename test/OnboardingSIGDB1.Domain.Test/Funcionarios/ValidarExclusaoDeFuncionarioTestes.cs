using Moq;
using OnboardingSIGDB1.Domain._Base.Interfaces;
using OnboardingSIGDB1.Domain.Funcionarios.Interfaces;
using OnboardingSIGDB1.Domain.Funcionarios.Resources;
using OnboardingSIGDB1.Domain.Funcionarios.Services;
using OnboardingSIGDB1.Domain.Test._Comum;
using System.Threading.Tasks;
using Xunit;

namespace OnboardingSIGDB1.Domain.Test.Funcionarios
{
    public class ValidarExclusaoDeFuncionarioTestes
    {
        private readonly int _idFuncionario;

        private readonly Mock<IDomainNotificationHandler> _notificacaoDeDominioMock;
        private readonly Mock<IFuncionarioRepositorio> _funcionarioRepositorioMock;
        private readonly ValidadorDeExclusaoDeFuncionario _validadorDeExclusaoDeFuncionario;

        public ValidarExclusaoDeFuncionarioTestes()
        {
            var onboardingSIGDB1faker = OnboardingSIGDB1FakerBuilder.Novo().Build();
            _idFuncionario = onboardingSIGDB1faker.Id();

            _notificacaoDeDominioMock = new Mock<IDomainNotificationHandler>();
            _funcionarioRepositorioMock = new Mock<IFuncionarioRepositorio>();

            _validadorDeExclusaoDeFuncionario = new ValidadorDeExclusaoDeFuncionario(
                _notificacaoDeDominioMock.Object,
                _funcionarioRepositorioMock.Object
                );
        }

        [Fact]
        public async Task DeveNotificarQuandoFuncionarioNaoExistir()
        {
            await _validadorDeExclusaoDeFuncionario.ValidarAsync(_idFuncionario);

            _notificacaoDeDominioMock.Verify(_ => _.HandleNotificacaoDeServicoAsync(It.Is<string>(
                _1 => _1 == FuncionarioResources.FuncionarioNaoExiste
                )));
        }
    }
}
