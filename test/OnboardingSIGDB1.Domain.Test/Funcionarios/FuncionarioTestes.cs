using OnboardingSIGDB1.Domain._Base.Helpers;
using OnboardingSIGDB1.Domain._Base.Resources;
using OnboardingSIGDB1.Domain.Funcionarios.Entidades;
using OnboardingSIGDB1.Domain.Test._Builders;
using OnboardingSIGDB1.Domain.Test._Comum;
using System;
using Xunit;

namespace OnboardingSIGDB1.Domain.Test.Funcionarios
{
    public class FuncionarioTestes
    {
        private readonly string _nome;
        private readonly string _cpf;
        private readonly DateTime? _dataDeContratacao;
        private readonly OnboardingSIGDB1Faker _onboardingSIGDB1faker;

        public FuncionarioTestes()
        {
            _onboardingSIGDB1faker = OnboardingSIGDB1FakerBuilder.Novo().Build();
            _nome = _onboardingSIGDB1faker.FraseComQuantidadeExataDeCaracteres(Constantes.Numero150);
            _cpf = _onboardingSIGDB1faker.Cpf();
            _dataDeContratacao = _onboardingSIGDB1faker.QualquerDataDoUltimoAno();
        }

        [Fact]
        public void DeveCriarFuncionario()
        {
            var funcionario = new Funcionario(_nome, _cpf, _dataDeContratacao);

            Assert.Equal(_nome, funcionario.Nome);
            Assert.Equal(_cpf.RemoverMascaraDoCpf(), funcionario.Cpf);
            Assert.Equal(_dataDeContratacao, funcionario.DataDeContratacao);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveAceitarNomeNuloOuVazio(string nome)
        {
            var funcionario = FuncionarioBuilder.Novo().ComNome(nome).Build();

            Assert.False(funcionario.Validar());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveAceitarCpfNuloOuVazio(string cpf)
        {
            var funcionario = FuncionarioBuilder.Novo().ComCpf(cpf).Build();

            Assert.False(funcionario.Validar());
        }

        [Theory]
        [InlineData(Constantes.Numero151)]
        public void NaoDeveAceitarNomeComQuantidadeCaracterInvalido(int quantidadeDeCaracteres)
        {
            var nomeInvalido = _onboardingSIGDB1faker.FraseComQuantidadeExataDeCaracteres(quantidadeDeCaracteres);
            var funcionario = FuncionarioBuilder.Novo().ComNome(nomeInvalido).Build();

            Assert.False(funcionario.Validar());
        }

        [Fact]
        public void NaoDeveAceitarDataDeContratacaoComADataMinima()
        {
            var dataInvalida = DateTime.MinValue;
            var funcionario = FuncionarioBuilder.Novo().ComDataDeContratacao(dataInvalida).Build();

            Assert.False(funcionario.Validar());
        }

        [Fact]
        public void DeveAceitarDataDeContratacaoSemInformacao()
        {
            var funcionario = FuncionarioBuilder.Novo().ComDataDeContratacao(null).Build();

            Assert.True(funcionario.Validar());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(4)]
        [InlineData(10)]
        public void NaoDeveAceitarCpfComTamanhoInvalido(int quantidadeDeCaracteres)
        {
            var cpfInvalido = _onboardingSIGDB1faker.NumeroComQuantidadeExataDeCaracteresComoString(quantidadeDeCaracteres);
            var funcionario = FuncionarioBuilder.Novo().ComCpf(cpfInvalido).Build();

            Assert.False(funcionario.Validar());
        }

        [Theory]
        [InlineData("883.208.113-09")]
        [InlineData("138.645.552-05")]
        [InlineData("774.990.927-39")]
        [InlineData("700.331.974-82")]
        [InlineData("242.263.511-34")]
        [InlineData("384.130.511-34")]
        public void DeveAceitarCpfValidos(string cpfValido)
        {
            var funcionario = FuncionarioBuilder.Novo().ComCpf(cpfValido).Build();

            Assert.True(funcionario.Validar());
        }

        [Theory]
        [InlineData("123.456.789-00")]
        [InlineData("000.000.000-00")]
        [InlineData("360.085.111-00")]
        [InlineData("797.194.134-00")]
        [InlineData("606.324.193-01")]
        public void NaoDeveAceitarCpfInvalidos(string cpfInvalido)
        {
            var funcionario = FuncionarioBuilder.Novo().ComCpf(cpfInvalido).Build();

            Assert.False(funcionario.Validar());
        }
    }
}
