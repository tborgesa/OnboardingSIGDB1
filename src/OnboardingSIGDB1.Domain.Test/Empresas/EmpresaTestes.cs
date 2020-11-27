using OnboardingSIGDB1.Domain.Test.Common;
using System;
using Xunit;
using Bogus.Extensions.Brazil;
using OnboardingSIGDB1.Domain._Base.Helpers;
using OnboardingSIGDB1.Domain.Empresas;
using OnboardingSIGDB1.Domain.Test.Builders;
using Bogus;

namespace OnboardingSIGDB1.Domain.Test.Empresas
{
    public class EmpresaTestes
    {
        private static string _nome;
        private static string _cnpj;
        private static DateTime? _dataDeFundacao;
        private readonly Faker _fake;

        public EmpresaTestes()
        {
            _fake = FakerBuilder.Novo().Build();
            _nome = _fake.Lorem.Random.AlphaNumeric(150);
            _cnpj = _fake.Company.Cnpj();
            _dataDeFundacao = _fake.Date.Recent(365);
        }

        [Fact]
        public void DeveCriarEmpresa()
        {
            var empresa = new Empresa(_nome, _cnpj, _dataDeFundacao);

            Assert.Equal(_nome, empresa.Nome);
            Assert.Equal(_cnpj.RemoverMascaraDoCnpj(), empresa.Cnpj);
            Assert.Equal(_dataDeFundacao, empresa.DataDeFundacao);
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
        [InlineData(151)]
        public void NaoDeveAceitarNomeComQuantidadeCaracterInavalido(int quantidadeDeCaracteres)
        {
            var nomeInvalido = _fake.Lorem.Random.AlphaNumeric(quantidadeDeCaracteres);
            var empresa = EmpresaBuilder.Novo().ComNome(nomeInvalido).Build();

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

        [Fact]
        public void NaoDeveAceitarCnpjComTamanhoInvalido()
        {
            var cnpjInvalido = _fake.Random.Number(13).ToString();
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
    }
}
