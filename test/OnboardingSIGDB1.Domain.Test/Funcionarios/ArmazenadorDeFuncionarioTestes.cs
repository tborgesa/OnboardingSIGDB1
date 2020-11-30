using Moq;
using OnboardingSIGDB1.Domain._Base.Helpers;
using OnboardingSIGDB1.Domain._Base.Notification;
using OnboardingSIGDB1.Domain._Base.Resources;
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
    public class ArmazenadorDeFuncionarioTestes
    {
        private readonly OnboardingSIGDB1Faker _onboardingSIGDB1faker;
        private readonly FuncionarioDto _funcionarioDto;
        private readonly int _id;

        private readonly Mock<IDomainNotificationHandler> _notificacaoDeDominioMock;
        private readonly Mock<IFuncionarioRepositorio> _funcionarioRepositorioMock;
        private readonly ValidadorCpfDaFuncionarioJaExistente _validadorCpfDaFuncionarioJaExistente;
        private readonly ArmazenadorDeFuncionario _armazenadorDeFuncionario;

        public ArmazenadorDeFuncionarioTestes()
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

            _validadorCpfDaFuncionarioJaExistente = new ValidadorCpfDaFuncionarioJaExistente(
                _notificacaoDeDominioMock.Object,
                _funcionarioRepositorioMock.Object
                );

            _armazenadorDeFuncionario = new ArmazenadorDeFuncionario(
                _notificacaoDeDominioMock.Object,
                _funcionarioRepositorioMock.Object,
                _validadorCpfDaFuncionarioJaExistente
             );
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task DeveNotificarErroDeDominio(string nomeInvalido)
        {
            _funcionarioDto.Nome = nomeInvalido;

            await _armazenadorDeFuncionario.ArmazenarAsync(_funcionarioDto);

            _notificacaoDeDominioMock.Verify(_ => _.HandleNotificacaoDeDominioAsync(It.IsAny<string>()));
        }

        [Fact]
        public async Task DeveNotificarErroDeDominioParaDtoNull()
        {
            await _armazenadorDeFuncionario.ArmazenarAsync(null);

            _notificacaoDeDominioMock.Verify(_ => _.HandleNotificacaoDeDominioAsync(It.IsAny<string>()));
        }

        [Fact]
        public async Task DeveNotificarErroDeServico()
        {
            var funcionarioDoBancoDeDados = FuncionarioBuilder.Novo().ComId(_id).Build();

            _funcionarioRepositorioMock.Setup(_ => _.ObterPorCpfAsync(_funcionarioDto.Cpf)).ReturnsAsync(funcionarioDoBancoDeDados);

            await _armazenadorDeFuncionario.ArmazenarAsync(_funcionarioDto);

            _notificacaoDeDominioMock.Verify(_ => _.HandleNotificacaoDeServicoAsync(It.IsAny<string>()));
        }

        [Fact]
        public async Task DeveSalvar()
        {
            await _armazenadorDeFuncionario.ArmazenarAsync(_funcionarioDto);

            _funcionarioRepositorioMock.Verify(_ => _.AdicionarAsync(
                It.Is<Funcionario>(_1 =>
                    _1.Nome == _funcionarioDto.Nome &&
                    _1.Cpf == _funcionarioDto.Cpf.RemoverMascaraDoCpf() &&
                    _1.DataDeContratacao == _funcionarioDto.DataDeContratacao
                    )), Times.Once);
        }

        [Fact]
        public async Task DeveEditarONomeDoFuncionario()
        {
            var nomeInicial = _onboardingSIGDB1faker.FraseComQuantidadeExataDeCaracteres(Constantes.Numero150);
            var funcionarioDoBancoDeDados = FuncionarioBuilder.Novo().ComId(_id).ComNome(nomeInicial).Build();
            _funcionarioDto.Id = _id;

            _funcionarioRepositorioMock.Setup(_ => _.ObterPorIdAsync(_id)).ReturnsAsync(funcionarioDoBancoDeDados);

            await _armazenadorDeFuncionario.ArmazenarAsync(_funcionarioDto);

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

            await _armazenadorDeFuncionario.ArmazenarAsync(_funcionarioDto);

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

            await _armazenadorDeFuncionario.ArmazenarAsync(_funcionarioDto);

            Assert.NotEqual(funcionarioDoBancoDeDados.DataDeContratacao, dataDeContratacaoInicial);
            Assert.Equal(funcionarioDoBancoDeDados.DataDeContratacao, _funcionarioDto.DataDeContratacao);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task DeveValidarDominioNaEdicao(string nomeInvalido)
        {
            _funcionarioDto.Id = _id;
            _funcionarioDto.Nome = nomeInvalido;
            var funcionarioDoBancoDeDados = FuncionarioBuilder.Novo().ComId(_id).Build();

            _funcionarioRepositorioMock.Setup(_ => _.ObterPorIdAsync(_id)).ReturnsAsync(funcionarioDoBancoDeDados);

            await _armazenadorDeFuncionario.ArmazenarAsync(_funcionarioDto);

            _funcionarioRepositorioMock.Verify(_ => _.ObterPorIdAsync(_id), Times.Once);
            _notificacaoDeDominioMock.Verify(_ => _.HandleNotificacaoDeDominioAsync(It.IsAny<string>()));
        }
    }
}
