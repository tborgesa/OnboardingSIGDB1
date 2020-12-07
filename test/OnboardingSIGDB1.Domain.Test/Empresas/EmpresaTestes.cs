using OnboardingSIGDB1.Domain._Base.Helpers;
using OnboardingSIGDB1.Domain._Base.Resources;
using OnboardingSIGDB1.Domain.Empresas.Entidades;
using OnboardingSIGDB1.Domain.Test._Builders;
using OnboardingSIGDB1.Domain.Test._Comum;
using System;
using Xunit;

namespace OnboardingSIGDB1.Domain.Test.Empresas
{
    public class EmpresaTestes
    {
        private readonly string _nome;
        private readonly string _cnpj;
        private readonly DateTime? _dataDeFundacao;
        private readonly OnboardingSIGDB1Faker _onboardingSIGDB1faker;

        public EmpresaTestes()
        {
            _onboardingSIGDB1faker = OnboardingSIGDB1FakerBuilder.Novo().Build();
            _nome = _onboardingSIGDB1faker.FraseComQuantidadeExataDeCaracteres(Constantes.Numero150);
            _cnpj = _onboardingSIGDB1faker.Cnpj();
            _dataDeFundacao = _onboardingSIGDB1faker.QualquerDataDoUltimoAno();
        }

        [Fact]
        public void DeveCriarEmpresaValida()
        {
            var empresa = new Empresa(_nome, _cnpj, _dataDeFundacao);

            Assert.Equal(_nome, empresa.Nome);
            Assert.Equal(_cnpj.RemoverMascaraDoCnpj(), empresa.Cnpj);
            Assert.Equal(_dataDeFundacao, empresa.DataDeFundacao);

            Assert.True(empresa.Validar());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveAceitarNomeNuloOuVazio(string nome)
        {
            var empresa = EmpresaBuilder.Novo().ComNome(nome).Build();

            Assert.False(empresa.Validar());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveAceitarCnpjNuloOuVazio(string cnpj)
        {
            var empresa = EmpresaBuilder.Novo().ComCnpj(cnpj).Build();

            Assert.False(empresa.Validar());
        }

        [Theory]
        [InlineData(Constantes.Numero151)]
        public void NaoDeveAceitarNomeComQuantidadeCaracterInvalido(int quantidadeDeCaracteres)
        {
            var nomeInvalido = _onboardingSIGDB1faker.FraseComQuantidadeExataDeCaracteres(quantidadeDeCaracteres);
            var empresa = EmpresaBuilder.Novo().ComNome(nomeInvalido).Build();

            Assert.False(empresa.Validar());
        }

        [Fact]
        public void NaoDeveAceitarDataDeFundacaoComADataMinima()
        {
            var dataInvalida = DateTime.MinValue;
            var empresa = EmpresaBuilder.Novo().ComDataDeFundacao(dataInvalida).Build();

            Assert.False(empresa.Validar());
        }

        [Fact]
        public void DeveAceitarDataDeFundacaoSemInformacao()
        {
            var empresa = EmpresaBuilder.Novo().ComDataDeFundacao(null).Build();

            Assert.True(empresa.Validar());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(13)]
        public void NaoDeveAceitarCnpjComTamanhoInvalido(int quantidadeDeCaracteres)
        {
            var cnpjInvalido = _onboardingSIGDB1faker.NumeroComQuantidadeExataDeCaracteresComoString(quantidadeDeCaracteres);
            var empresa = EmpresaBuilder.Novo().ComCnpj(cnpjInvalido).Build();

            Assert.False(empresa.Validar());
        }

        [Theory]
        [InlineData("24.549.254/0001-21")]
        [InlineData("96.526.324/0001-37")]
        [InlineData("72.123.715/0001-42")]
        [InlineData("74.644.941/0001-02")]
        [InlineData("46.811.626/0001-89")]
        public void DeveAceitarCnpjValidos(string cnpjValido)
        {
            var empresa = EmpresaBuilder.Novo().ComCnpj(cnpjValido).Build();

            Assert.True(empresa.Validar());
        }

        [Theory]
        [InlineData("11.111.111/1111-11")]
        [InlineData("99.999.999/9999-99")]
        [InlineData("00.000.000/0000-00")]
        [InlineData("42.369.234/0001-14")]
        [InlineData("53.664.564/0001-59")]
        [InlineData("19.447.234/0001-63")]
        public void NaoDeveAceitarCnpjInvalidos(string cnpjInvalido)
        {
            var empresa = EmpresaBuilder.Novo().ComCnpj(cnpjInvalido).Build();

            Assert.False(empresa.Validar());
        }

    }
}
