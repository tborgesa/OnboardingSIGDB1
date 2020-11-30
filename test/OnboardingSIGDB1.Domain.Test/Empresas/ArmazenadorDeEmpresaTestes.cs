using Bogus;
using Bogus.Extensions.Brazil;
using Moq;
using OnboardingSIGDB1.Domain._Base.Enumeradores;
using OnboardingSIGDB1.Domain._Base.Notification;
using OnboardingSIGDB1.Domain._Base.Resources;
using OnboardingSIGDB1.Domain.Empresas.Dto;
using OnboardingSIGDB1.Domain.Empresas.Entidades;
using OnboardingSIGDB1.Domain.Empresas.Interfaces;
using OnboardingSIGDB1.Domain.Empresas.Services;
using OnboardingSIGDB1.Domain.Test._Builders;
using OnboardingSIGDB1.Domain.Test._Comum;
using System;
using System.Threading.Tasks;
using Xunit;

namespace OnboardingSIGDB1.Domain.Test.Empresas
{
    public class ArmazenadorDeEmpresaTestes
    {
        private EmpresaDto _empresaDto;
        private int _id;
        private OnboardingSIGDB1Faker _onboardingSIGDB1faker;

        private ArmazenadorDeEmpresa _armazenadorDeEmpresa;
        private Mock<IDomainNotificationHandler> _notificacaoDeDominioMock;
        private Mock<IEmpresaRepositorio> _empresaRepositorioMock;
        private ValidadorCnpjDaEmpresaJaExistente _validadorCnpjDaEmpresaJaExistente;

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

            CriarArmazenador();
        }

        private void CriarArmazenador()
        {
            _notificacaoDeDominioMock = new Mock<IDomainNotificationHandler>();
            _empresaRepositorioMock = new Mock<IEmpresaRepositorio>();

            _validadorCnpjDaEmpresaJaExistente = new ValidadorCnpjDaEmpresaJaExistente(
                _notificacaoDeDominioMock.Object,
                _empresaRepositorioMock.Object
                );

            _armazenadorDeEmpresa = new ArmazenadorDeEmpresa(
                _notificacaoDeDominioMock.Object,
                _empresaRepositorioMock.Object,
                _validadorCnpjDaEmpresaJaExistente
                );
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public async void DeveNotificarErroDeDominio(string nomeInvalido)
        {
            _empresaDto.Nome = nomeInvalido;

            await _armazenadorDeEmpresa.ArmazenarAsync(_empresaDto);

            _notificacaoDeDominioMock.Verify(_ => _.HandleNotificacaoDeDominioAsync(It.IsAny<string>()));
        }

        [Fact]
        public async void DeveNotificarErroDeDominioParaDtoNull()
        {
            await _armazenadorDeEmpresa.ArmazenarAsync(null);
            _notificacaoDeDominioMock.Verify(_ => _.HandleNotificacaoDeDominioAsync(It.IsAny<string>()));
        }

        [Fact]
        public async void DeveNotificarErroDeServico()
        {
            _empresaRepositorioMock.Setup(_ => _.ObterPorCnpjAsync(_empresaDto.Cnpj))
                .ReturnsAsync(EmpresaBuilder.Novo().ComId(_id).ComCnpj(_empresaDto.Cnpj).Build());

            await _armazenadorDeEmpresa.ArmazenarAsync(_empresaDto);

            _notificacaoDeDominioMock.Verify(_ => _.HandleNotificacaoDeServicoAsync(It.IsAny<string>()));
        }

        [Fact]
        public async Task DeveSalvar()
        {
            await _armazenadorDeEmpresa.ArmazenarAsync(_empresaDto);

            _empresaRepositorioMock.Verify(n => n.AdicionarAsync(
                It.IsAny<Empresa>()), Times.Once);
        }

        [Fact]
        public async Task DeveEditarONomeDaEmpresa()
        {
            _empresaDto.Id = _onboardingSIGDB1faker.Id();
            var nomeInicial = _onboardingSIGDB1faker.FraseComQuantidadeExataDeCaracteres(Constantes.Numero150);
            var empresaDoBancoDeDados = EmpresaBuilder.Novo().ComId(_id).ComNome(nomeInicial).Build();

            _empresaRepositorioMock.Setup(_ => _.ObterPorIdAsync(_empresaDto.Id))
                .ReturnsAsync(empresaDoBancoDeDados);

            await _armazenadorDeEmpresa.ArmazenarAsync(_empresaDto);

            Assert.NotEqual(empresaDoBancoDeDados.Nome, nomeInicial);
            Assert.Equal(empresaDoBancoDeDados.Nome, _empresaDto.Nome);
        }

        [Fact]
        public async Task DeveEditarOCnpjDaEmpresa()
        {
            _empresaDto.Id = _onboardingSIGDB1faker.Id();
            var cnpjInicial = _onboardingSIGDB1faker.Cnpj();
            var empresaDoBancoDeDados = EmpresaBuilder.Novo().ComId(_id).ComCnpj(cnpjInicial).Build();

            _empresaRepositorioMock.Setup(_ => _.ObterPorIdAsync(_empresaDto.Id))
                .ReturnsAsync(empresaDoBancoDeDados);

            await _armazenadorDeEmpresa.ArmazenarAsync(_empresaDto);

            Assert.NotEqual(empresaDoBancoDeDados.Cnpj, cnpjInicial);
            Assert.Equal(empresaDoBancoDeDados.Cnpj, _empresaDto.Cnpj);
        }

        [Fact]
        public async Task DeveEditarADataDeFundacaoDaEmpresa()
        {
            _empresaDto.Id = _onboardingSIGDB1faker.Id();
            var dataDeFundacao = _onboardingSIGDB1faker.QualquerDataDoUltimoAno();
            var empresaDoBancoDeDados = EmpresaBuilder.Novo().ComId(_id).ComDataDeFundacao(dataDeFundacao).Build();

            _empresaRepositorioMock.Setup(_ => _.ObterPorIdAsync(_empresaDto.Id))
                .ReturnsAsync(empresaDoBancoDeDados);

            await _armazenadorDeEmpresa.ArmazenarAsync(_empresaDto);

            Assert.NotEqual(empresaDoBancoDeDados.DataDeFundacao, dataDeFundacao);
            Assert.Equal(empresaDoBancoDeDados.DataDeFundacao, _empresaDto.DataDeFundacao);
        }

        [Fact]
        public async Task DeveValidarDominioNaEdicao()
        {
            _empresaDto.Id = _onboardingSIGDB1faker.Id();
            _empresaDto.Nome = null;

            var empresaDoBancoDeDados = EmpresaBuilder.Novo().ComId(_id).Build();

            _empresaRepositorioMock.Setup(_ => _.ObterPorIdAsync(_empresaDto.Id))
                .ReturnsAsync(empresaDoBancoDeDados);

            await _armazenadorDeEmpresa.ArmazenarAsync(_empresaDto);

            _notificacaoDeDominioMock.Verify(_ => _.HandleNotificacaoDeDominioAsync(It.IsAny<string>()));
        }
    }
}
