using Moq;
using OnboardingSIGDB1.Domain._Base.Helpers;
using OnboardingSIGDB1.Domain._Base.Interfaces;
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
        private readonly Mock<IValidadorCpfDaFuncionarioJaExistente> _validadorCpfDaFuncionarioJaExistenteMock;
        private readonly Mock<IEditarUmFuncionario> _editarUmFuncionarioMock;
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
            _validadorCpfDaFuncionarioJaExistenteMock = new Mock<IValidadorCpfDaFuncionarioJaExistente>();
            _editarUmFuncionarioMock = new Mock<IEditarUmFuncionario>();

            _armazenadorDeFuncionario = new ArmazenadorDeFuncionario(
                _notificacaoDeDominioMock.Object,
                _funcionarioRepositorioMock.Object,
                _validadorCpfDaFuncionarioJaExistenteMock.Object,
                _editarUmFuncionarioMock.Object
             );
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task DeveValidarDominioNaInsercao(string nomeInvalido)
        {
            _funcionarioDto.Nome = nomeInvalido;

            await _armazenadorDeFuncionario.ArmazenarAsync(_funcionarioDto);

            _notificacaoDeDominioMock.Verify(_ => _.HandleNotificacaoDeDominioAsync(It.IsAny<string>()));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task DeveValidarDominioNaEdicao(string nomeInvalido)
        {
            _funcionarioDto.Id = _id;
            _funcionarioDto.Nome = nomeInvalido;
            var funcionarioDoBancoDeDados = FuncionarioBuilder.Novo().ComId(_id).Build();

            MockarAEdicaoDoDto();

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
        public async Task DeveChamarMetodoValidacaoCnpjNaInsercao()
        {
            await _armazenadorDeFuncionario.ArmazenarAsync(_funcionarioDto);

            _validadorCpfDaFuncionarioJaExistenteMock.Verify(_ => _.ValidarAsync(_funcionarioDto.Cpf.RemoverMascaraDoCpf(), 0));
        }

        [Fact]
        public async Task DeveChamarMetodoValidacaoCnpjNaEdicao()
        {
            _funcionarioDto.Id = _id;
            MockarAEdicaoDoDto();

            await _armazenadorDeFuncionario.ArmazenarAsync(_funcionarioDto);

            _validadorCpfDaFuncionarioJaExistenteMock.Verify(_ => _.ValidarAsync(_funcionarioDto.Cpf.RemoverMascaraDoCpf(), _funcionarioDto.Id));
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

        private void MockarAEdicaoDoDto()
        {
            var funcionarioDoBancoDeDados = FuncionarioBuilder.Novo().
                ComId(_id).
                ComCpf(_funcionarioDto.Cpf).
                ComNome(_funcionarioDto.Nome).
                ComDataDeContratacao(_funcionarioDto.DataDeContratacao)
                .Build();

            _editarUmFuncionarioMock.Setup(_ => _.EditarAsync(_funcionarioDto))
                .ReturnsAsync(funcionarioDoBancoDeDados);
        }
    }
}
