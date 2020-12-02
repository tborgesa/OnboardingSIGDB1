using Moq;
using OnboardingSIGDB1.Domain._Base.Helpers;
using OnboardingSIGDB1.Domain._Base.Interfaces;
using OnboardingSIGDB1.Domain._Base.Resources;
using OnboardingSIGDB1.Domain.Empresas.Dto;
using OnboardingSIGDB1.Domain.Empresas.Entidades;
using OnboardingSIGDB1.Domain.Empresas.Interfaces;
using OnboardingSIGDB1.Domain.Empresas.Services;
using OnboardingSIGDB1.Domain.Test._Builders;
using OnboardingSIGDB1.Domain.Test._Comum;
using System.Threading.Tasks;
using Xunit;

namespace OnboardingSIGDB1.Domain.Test.Empresas
{
    public class ArmazenadorDeEmpresaTestes
    {
        private readonly EmpresaDto _empresaDto;
        private readonly int _id;
        private readonly OnboardingSIGDB1Faker _onboardingSIGDB1faker;

        private readonly ArmazenadorDeEmpresa _armazenadorDeEmpresa;
        private readonly Mock<IDomainNotificationHandler> _notificacaoDeDominioMock;
        private readonly Mock<IEmpresaRepositorio> _empresaRepositorioMock;
        private readonly Mock<IValidadorCnpjDaEmpresaJaExistente> _validadorCnpjDaEmpresaJaExistenteMock;
        private readonly Mock<IEditarUmaEmpresa> _editarUmaEmpresaMock;

        public ArmazenadorDeEmpresaTestes()
        {
            _onboardingSIGDB1faker = OnboardingSIGDB1FakerBuilder.Novo().Build();

            _id = _onboardingSIGDB1faker.Id();
            _empresaDto = new EmpresaDto
            {
                Nome = _onboardingSIGDB1faker.FraseComQuantidadeExataDeCaracteres(Constantes.Numero150),
                Cnpj = _onboardingSIGDB1faker.Cnpj(),
                DataDeFundacao = _onboardingSIGDB1faker.QualquerDataDoUltimoAno()
            };

            _notificacaoDeDominioMock = new Mock<IDomainNotificationHandler>();
            _empresaRepositorioMock = new Mock<IEmpresaRepositorio>();
            _validadorCnpjDaEmpresaJaExistenteMock = new Mock<IValidadorCnpjDaEmpresaJaExistente>();
            _editarUmaEmpresaMock = new Mock<IEditarUmaEmpresa>();

            _armazenadorDeEmpresa = new ArmazenadorDeEmpresa(
                _notificacaoDeDominioMock.Object,
                _empresaRepositorioMock.Object,
                _validadorCnpjDaEmpresaJaExistenteMock.Object,
                _editarUmaEmpresaMock.Object
                );
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task DeveNotificarErroDeDominioNaInsercao(string nomeInvalido)
        {
            _empresaDto.Nome = nomeInvalido;

            await _armazenadorDeEmpresa.ArmazenarAsync(_empresaDto);

            _notificacaoDeDominioMock.Verify(_ => _.HandleNotificacaoDeDominioAsync(It.IsAny<string>()));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async Task DeveNotificarErroDeDominioNaEdicao(string nomeInvalido)
        {
            _empresaDto.Id = _onboardingSIGDB1faker.Id();
            _empresaDto.Nome = nomeInvalido;

            MockarAEdicaoDoDto();

            await _armazenadorDeEmpresa.ArmazenarAsync(_empresaDto);

            _notificacaoDeDominioMock.Verify(_ => _.HandleNotificacaoDeDominioAsync(It.IsAny<string>()));
        }

        [Fact]
        public async Task DeveNotificarErroDeDominioParaDtoNull()
        {
            await _armazenadorDeEmpresa.ArmazenarAsync(null);
            _notificacaoDeDominioMock.Verify(_ => _.HandleNotificacaoDeDominioAsync(It.IsAny<string>()));
        }

        [Fact]
        public async Task DeveChamarMetodoValidacaoCnpjNaInsercao()
        {
            await _armazenadorDeEmpresa.ArmazenarAsync(_empresaDto);

            _validadorCnpjDaEmpresaJaExistenteMock.Verify(_ => _.ValidarAsync(_empresaDto.Cnpj.RemoverMascaraDoCnpj(), 0));
        }

        [Fact]
        public async Task DeveChamarMetodoValidacaoCnpjNaEdicao()
        {
            _empresaDto.Id = _id;
            MockarAEdicaoDoDto();

            await _armazenadorDeEmpresa.ArmazenarAsync(_empresaDto);

            _validadorCnpjDaEmpresaJaExistenteMock.Verify(_ => _.ValidarAsync(_empresaDto.Cnpj.RemoverMascaraDoCnpj(), _id));
        }

        [Fact]
        public async Task DeveSalvar()
        {
            await _armazenadorDeEmpresa.ArmazenarAsync(_empresaDto);

            _empresaRepositorioMock.Verify(_ => _.AdicionarAsync(
                It.Is<Empresa>(_1 =>
                _1.Nome == _empresaDto.Nome &&
                _1.Cnpj == _empresaDto.Cnpj.RemoverMascaraDoCnpj() &&
                _1.DataDeFundacao == _empresaDto.DataDeFundacao
                )), Times.Once);
        }

        private void MockarAEdicaoDoDto()
        {
            var empresaDoBancoDeDados = EmpresaBuilder.Novo().
                ComId(_id).
                ComCnpj(_empresaDto.Cnpj.RemoverMascaraDoCnpj()).
                ComNome(_empresaDto.Nome).
                ComDataDeFundacao(_empresaDto.DataDeFundacao)
                .Build();

            _editarUmaEmpresaMock.Setup(_ => _.EditarUmaEmpresaAsync(_empresaDto))
                .ReturnsAsync(empresaDoBancoDeDados);
        }
    }
}
