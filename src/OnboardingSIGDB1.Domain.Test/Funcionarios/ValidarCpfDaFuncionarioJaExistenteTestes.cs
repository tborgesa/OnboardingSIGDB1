﻿using Bogus;
using Bogus.Extensions.Brazil;
using Moq;
using OnboardingSIGDB1.Domain._Base.Notification;
using OnboardingSIGDB1.Domain._Base.Resources;
using OnboardingSIGDB1.Domain.Funcionarios.Interfaces;
using OnboardingSIGDB1.Domain.Funcionarios.Resources;
using OnboardingSIGDB1.Domain.Funcionarios.Services;
using OnboardingSIGDB1.Domain.Test.Builders;
using OnboardingSIGDB1.Domain.Test.Common;
using System.Threading.Tasks;
using Xunit;

namespace OnboardingSIGDB1.Domain.Test.Funcionarios
{
    public class ValidarCpfDaFuncionarioJaExistenteTestes
    {
        private readonly Mock<IFuncionarioRepositorio> _funcionarioRepositorio;
        private readonly ValidadorCpfDaFuncionarioJaExistente _validadorCpfDaFuncionarioJaExistente;
        private readonly Mock<IDomainNotificationHandler> _notificacaoDeDominioMock;

        private readonly string _cpf;
        private int _id;
        private readonly Faker _faker;
        private readonly string _erroEsperado;

        public ValidarCpfDaFuncionarioJaExistenteTestes()
        {
            _faker = FakerBuilder.Novo().Build();

            _id = _faker.Id();
            _cpf = _faker.Person.Cpf();
            _erroEsperado = Resource.FormatarResourceToLowerValor2(
                Resource.MensagemJaExisteCadastrada,
                FuncionarioResources.Funcionario, FuncionarioResources.Cpf);

            _funcionarioRepositorio = new Mock<IFuncionarioRepositorio>();
            _notificacaoDeDominioMock = new Mock<IDomainNotificationHandler>();
            _validadorCpfDaFuncionarioJaExistente = new ValidadorCpfDaFuncionarioJaExistente(
                _notificacaoDeDominioMock.Object,
                _funcionarioRepositorio.Object);
        }

        [Fact]
        public async Task NaoDeveAceitarCpfJaCadastrado()
        {
            _funcionarioRepositorio.Setup(_ => _.ObterPorCpfAsync(_cpf))
                .ReturnsAsync(FuncionarioBuilder.Novo().ComId(_id).ComCpf(_cpf).Build());

            await _validadorCpfDaFuncionarioJaExistente.ValidarAsync(_cpf);

            _notificacaoDeDominioMock.Verify(_ => _.HandleNotificacaoDeServicoAsync(It.Is<string>(_1 => _1 == _erroEsperado)));
        }

        [Fact]
        public async Task NaoDeveAceitarCpfJaCadastradoNaEdicao()
        {
            int idOutroFuncionario = 2;

            _funcionarioRepositorio.Setup(_ => _.ObterPorCpfAsync(_cpf))
                .ReturnsAsync(FuncionarioBuilder.Novo().ComId(_id).ComCpf(_cpf).Build());

            await _validadorCpfDaFuncionarioJaExistente.ValidarAsync(_cpf, idOutroFuncionario);

            _notificacaoDeDominioMock.Verify(_ => _.HandleNotificacaoDeServicoAsync(It.Is<string>(_1 => _1 == _erroEsperado)));
        }

        [Fact]
        public async Task DeveAceitarCnpjJaCadastradoComOMesmoId()
        {
            _funcionarioRepositorio.Setup(_ => _.ObterPorCpfAsync(_cpf))
                .ReturnsAsync(FuncionarioBuilder.Novo().ComId(_id).ComCpf(_cpf).Build());

            await _validadorCpfDaFuncionarioJaExistente.ValidarAsync(_cpf, _id);

            _notificacaoDeDominioMock.Verify(_ => _.HandleNotificacaoDeServicoAsync(It.IsAny<string>()), Times.Never);
        }
    }
}
