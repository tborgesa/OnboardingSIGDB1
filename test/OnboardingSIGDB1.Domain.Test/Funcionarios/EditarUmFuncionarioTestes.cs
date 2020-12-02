using Moq;
using OnboardingSIGDB1.Domain._Base.Helpers;
using OnboardingSIGDB1.Domain._Base.Interfaces;
using OnboardingSIGDB1.Domain._Base.Resources;
using OnboardingSIGDB1.Domain.Funcionarios.Dto;
using OnboardingSIGDB1.Domain.Funcionarios.Interfaces;
using OnboardingSIGDB1.Domain.Funcionarios.Services;
using OnboardingSIGDB1.Domain.Test._Builders;
using OnboardingSIGDB1.Domain.Test._Comum;
using System.Threading.Tasks;
using Xunit;

namespace OnboardingSIGDB1.Domain.Test.Funcionarios
{
    public class EditarUmFuncionarioTestes
    {
        private readonly OnboardingSIGDB1Faker _onboardingSIGDB1faker;
        private readonly FuncionarioDto _funcionarioDto;
        private readonly int _id;

        private readonly Mock<IDomainNotificationHandler> _notificacaoDeDominioMock;
        private readonly Mock<IFuncionarioRepositorio> _funcionarioRepositorioMock;

        private readonly EditarUmFuncionario _editarUmFuncionario;

        public EditarUmFuncionarioTestes()
        {
            _onboardingSIGDB1faker = OnboardingSIGDB1FakerBuilder.Novo().Build();

            _id = _onboardingSIGDB1faker.Id();
            _funcionarioDto = new FuncionarioDto
            {
                Nome = _onboardingSIGDB1faker.FraseComQuantidadeExataDeCaracteres(Constantes.Numero150),
                Cpf = _onboardingSIGDB1faker.Cpf(),
                DataDeContratacao = _onboardingSIGDB1faker.QualquerDataDoUltimoAno()
            };

            _notificacaoDeDominioMock = new Mock<IDomainNotificationHandler>();
            _funcionarioRepositorioMock = new Mock<IFuncionarioRepositorio>();

            _editarUmFuncionario = new EditarUmFuncionario(
                _notificacaoDeDominioMock.Object,
                _funcionarioRepositorioMock.Object
             );
        }

        [Fact]
        public async Task DeveEditarONomeDoFuncionario()
        {
            var nomeInicial = _onboardingSIGDB1faker.FraseComQuantidadeExataDeCaracteres(Constantes.Numero150);
            var funcionarioDoBancoDeDados = FuncionarioBuilder.Novo().ComId(_id).ComNome(nomeInicial).Build();
            _funcionarioDto.Id = _id;

            _funcionarioRepositorioMock.Setup(_ => _.ObterPorIdAsync(_id)).ReturnsAsync(funcionarioDoBancoDeDados);

            await _editarUmFuncionario.EditarAsync(_funcionarioDto);

            Assert.NotEqual(funcionarioDoBancoDeDados.Nome, nomeInicial);
            Assert.Equal(funcionarioDoBancoDeDados.Nome, _funcionarioDto.Nome);
        }

        [Fact]
        public async Task DeveEditarOCpfDoFuncionario()
        {
            var cpfInicial = _onboardingSIGDB1faker.Cpf();
            var funcionarioDoBancoDeDados = FuncionarioBuilder.Novo().ComId(_id).ComCpf(cpfInicial).Build();
            _funcionarioDto.Id = _id;

            _funcionarioRepositorioMock.Setup(_ => _.ObterPorIdAsync(_id)).ReturnsAsync(funcionarioDoBancoDeDados);

            await _editarUmFuncionario.EditarAsync(_funcionarioDto);

            Assert.NotEqual(funcionarioDoBancoDeDados.Cpf, cpfInicial.RemoverMascaraDoCpf());
            Assert.Equal(funcionarioDoBancoDeDados.Cpf, _funcionarioDto.Cpf.RemoverMascaraDoCpf());
        }

        [Fact]
        public async Task DeveEditarADataDeContratacaoDoFuncionario()
        {
            var dataDeContratacaoInicial = _onboardingSIGDB1faker.QualquerDataDoUltimoAno();
            var funcionarioDoBancoDeDados = FuncionarioBuilder.Novo().ComId(_id).ComDataDeContratacao(dataDeContratacaoInicial).Build();
            _funcionarioDto.Id = _id;

            _funcionarioRepositorioMock.Setup(_ => _.ObterPorIdAsync(_id)).ReturnsAsync(funcionarioDoBancoDeDados);

            await _editarUmFuncionario.EditarAsync(_funcionarioDto);

            Assert.NotEqual(funcionarioDoBancoDeDados.DataDeContratacao, dataDeContratacaoInicial);
            Assert.Equal(funcionarioDoBancoDeDados.DataDeContratacao, _funcionarioDto.DataDeContratacao);
        }

        [Fact]
        public async Task DeveNotificarErroDeServicoAoInformarUmIdNaEdicaoQueNaoExisteNaBase()
        {
            _funcionarioDto.Id = _id;

            await _editarUmFuncionario.EditarAsync(_funcionarioDto);

            _notificacaoDeDominioMock.Verify(_ => _.HandleNotificacaoDeDominioAsync(It.IsAny<string>()));
        }
    }
}
