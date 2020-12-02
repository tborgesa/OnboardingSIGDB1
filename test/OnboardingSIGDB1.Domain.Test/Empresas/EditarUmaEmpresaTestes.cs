using Moq;
using OnboardingSIGDB1.Domain._Base.Helpers;
using OnboardingSIGDB1.Domain._Base.Interfaces;
using OnboardingSIGDB1.Domain._Base.Resources;
using OnboardingSIGDB1.Domain.Empresas.Dto;
using OnboardingSIGDB1.Domain.Empresas.Interfaces;
using OnboardingSIGDB1.Domain.Empresas.Services;
using OnboardingSIGDB1.Domain.Test._Builders;
using OnboardingSIGDB1.Domain.Test._Comum;
using System.Threading.Tasks;
using Xunit;

namespace OnboardingSIGDB1.Domain.Test.Empresas
{
    public class EditarUmaEmpresaTestes
    {
        private readonly EmpresaDto _empresaDto;
        private readonly int _id;
        private readonly OnboardingSIGDB1Faker _onboardingSIGDB1faker;

        private readonly Mock<IDomainNotificationHandler> _notificacaoDeDominioMock;
        private readonly Mock<IEmpresaRepositorio> _empresaRepositorioMock;
        private readonly EditarUmaEmpresa _editarUmaEmpresa;

        public EditarUmaEmpresaTestes()
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

            _editarUmaEmpresa = new EditarUmaEmpresa(
                _notificacaoDeDominioMock.Object,
                _empresaRepositorioMock.Object
                );
        }

        [Fact]
        public async Task DeveEditarONomeDaEmpresa()
        {
            _empresaDto.Id = _onboardingSIGDB1faker.Id();
            var nomeInicial = _onboardingSIGDB1faker.FraseComQuantidadeExataDeCaracteres(Constantes.Numero150);
            var empresaDoBancoDeDados = EmpresaBuilder.Novo().ComId(_id).ComNome(nomeInicial).Build();

            _empresaRepositorioMock.Setup(_ => _.ObterPorIdAsync(_empresaDto.Id))
                .ReturnsAsync(empresaDoBancoDeDados);

            await _editarUmaEmpresa.EditarAsync(_empresaDto);

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

            await _editarUmaEmpresa.EditarAsync(_empresaDto);

            Assert.NotEqual(empresaDoBancoDeDados.Cnpj, cnpjInicial.RemoverMascaraDoCnpj());
            Assert.Equal(empresaDoBancoDeDados.Cnpj, _empresaDto.Cnpj.RemoverMascaraDoCnpj());
        }

        [Fact]
        public async Task DeveEditarADataDeFundacaoDaEmpresa()
        {
            _empresaDto.Id = _onboardingSIGDB1faker.Id();
            var dataDeFundacao = _onboardingSIGDB1faker.QualquerDataDoUltimoAno();
            var empresaDoBancoDeDados = EmpresaBuilder.Novo().ComId(_id).ComDataDeFundacao(dataDeFundacao).Build();

            _empresaRepositorioMock.Setup(_ => _.ObterPorIdAsync(_empresaDto.Id))
                .ReturnsAsync(empresaDoBancoDeDados);

            await _editarUmaEmpresa.EditarAsync(_empresaDto);

            Assert.NotEqual(empresaDoBancoDeDados.DataDeFundacao, dataDeFundacao);
            Assert.Equal(empresaDoBancoDeDados.DataDeFundacao, _empresaDto.DataDeFundacao);
        }

        [Fact]
        public async Task DeveNotificarErroDeServicoAoInformarUmIdNaEdicaoQueNaoExisteNaBase()
        {
            _empresaDto.Id = _id;

            await _editarUmaEmpresa.EditarAsync(_empresaDto);

            _notificacaoDeDominioMock.Verify(_ => _.HandleNotificacaoDeDominioAsync(It.IsAny<string>()));
        }
    }
}
