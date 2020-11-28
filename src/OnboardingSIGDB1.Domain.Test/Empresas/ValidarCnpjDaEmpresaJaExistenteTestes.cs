using Bogus;
using Bogus.Extensions.Brazil;
using Moq;
using OnboardingSIGDB1.Domain._Base.Notification;
using OnboardingSIGDB1.Domain._Base.Resources;
using OnboardingSIGDB1.Domain.Empresas.Interfaces;
using OnboardingSIGDB1.Domain.Empresas.Resources;
using OnboardingSIGDB1.Domain.Empresas.Services;
using OnboardingSIGDB1.Domain.Test.Builders;
using OnboardingSIGDB1.Domain.Test.Common;
using System.Threading.Tasks;
using Xunit;

namespace OnboardingSIGDB1.Domain.Test.Empresas
{
    public class ValidarCnpjDaEmpresaJaExistenteTestes
    {
        private readonly Mock<IEmpresaRepositorio> _empresaRepositorioMock;
        private readonly ValidadorCnpjDaEmpresaJaExistente _validadorCnpjDaEmpresaJaExistente;
        private readonly Mock<IDomainNotificationHandler> _notificacaoDeDominioMock;

        private readonly string _cnpj;
        private readonly int _id;
        private readonly Faker _faker;
        private readonly string _erroEsperado;

        public ValidarCnpjDaEmpresaJaExistenteTestes()
        {
            _faker = FakerBuilder.Novo().Build();
            _id = _faker.Id();
            _cnpj = _faker.Company.Cnpj();
            _erroEsperado = Resource.FormatarResourceToLowerValor2(
                Resource.MensagemJaExisteCadastrada,
                EmpresaResources.Empresa, EmpresaResources.Cnpj);

            _empresaRepositorioMock = new Mock<IEmpresaRepositorio>();
            _notificacaoDeDominioMock = new Mock<IDomainNotificationHandler>();
            _validadorCnpjDaEmpresaJaExistente = new ValidadorCnpjDaEmpresaJaExistente(
                _notificacaoDeDominioMock.Object,
                _empresaRepositorioMock.Object);
        }

        [Fact]
        public async Task NaoDeveAceitarCnpjJaCadastrado()
        {
            _empresaRepositorioMock.Setup(_ => _.ObterPorCnpjAsync(_cnpj))
                .ReturnsAsync(EmpresaBuilder.Novo().ComId(_id).ComCnpj(_cnpj).Build());

            await _validadorCnpjDaEmpresaJaExistente.ValidarAsync(_cnpj);

            _notificacaoDeDominioMock.Verify(_ => _.HandleNotificacaoDeServicoAsync(It.Is<string>(_1 => _1 == _erroEsperado)));
        }

        [Fact]
        public async Task NaoDeveAceitarCnpjJaCadastradoNaEdicao()
        {
            int idOutraEmpresa = 2;
           
            _empresaRepositorioMock.Setup(_ => _.ObterPorCnpjAsync(_cnpj))
                .ReturnsAsync(EmpresaBuilder.Novo().ComId(_id).ComCnpj(_cnpj).Build());

            await _validadorCnpjDaEmpresaJaExistente.ValidarAsync(_cnpj, idOutraEmpresa);

            _notificacaoDeDominioMock.Verify(_ => _.HandleNotificacaoDeServicoAsync(It.Is<string>(_1 => _1 == _erroEsperado)));
        }

        [Fact]
        public async Task DeveAceitarCnpjJaCadastradoComOMesmoId()
        {
           _empresaRepositorioMock.Setup(_ => _.ObterPorCnpjAsync(_cnpj))
                .ReturnsAsync(EmpresaBuilder.Novo().ComId(_id).ComCnpj(_cnpj).Build());

            await _validadorCnpjDaEmpresaJaExistente.ValidarAsync(_cnpj, _id);

            _notificacaoDeDominioMock.Verify(_ => _.HandleNotificacaoDeServicoAsync(It.IsAny<string>()), Times.Never);
        }
    }
}
