using Moq;
using OnboardingSIGDB1.Domain._Base.Interfaces;
using OnboardingSIGDB1.Domain._Base.Resources;
using OnboardingSIGDB1.Domain.Empresas.Interfaces;
using OnboardingSIGDB1.Domain.Empresas.Resources;
using OnboardingSIGDB1.Domain.Empresas.Services;
using OnboardingSIGDB1.Domain.Test._Builders;
using OnboardingSIGDB1.Domain.Test._Comum;
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
        private readonly OnboardingSIGDB1Faker _onboardingSIGDB1faker;
        private readonly string _erroEsperado;

        public ValidarCnpjDaEmpresaJaExistenteTestes()
        {
            _onboardingSIGDB1faker = OnboardingSIGDB1FakerBuilder.Novo().Build();
            _id = _onboardingSIGDB1faker.Id();
            _cnpj = _onboardingSIGDB1faker.Cnpj();
            _erroEsperado = Resource.FormatarResourceToLowerValor2(
                Resource.MensagemJaExisteCadastradoFeminino,
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

            await _validadorCnpjDaEmpresaJaExistente.ValidarAsync(_cnpj, 0);

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
