using Bogus;
using OnboardingSIGDB1.Domain.Cargos;
using OnboardingSIGDB1.Domain.Test.Builders;
using OnboardingSIGDB1.Domain.Test.Common;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OnboardingSIGDB1.Domain.Test.Cargos
{
    public class CargoTestes
    {
        private readonly Faker _fake;
        private readonly string _descricao;

        public CargoTestes()
        {
            _fake = FakerBuilder.Novo().Build();
            _descricao = _fake.Lorem.Random.AlphaNumeric(250);
        }

        [Fact]
        public void DeveCriarCargo()
        {
            var cargo = new Cargo(_descricao);

            Assert.Equal(_descricao, cargo.Descricao);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveAceitarDescricaoNuloOuVazio(string descricao)
        {
            var cargo = CargoBuilder.Novo().ComDescricao(descricao).Build();

            Assert.False(cargo.Validar());
        }

        [Theory]
        [InlineData(251)]
        public void NaoDeveAceitarDescricaoComQuantidadeCaracterInvalido(int quantidadeDeCaracteres)
        {
            var descricaoInvalido = _fake.Lorem.Random.AlphaNumeric(quantidadeDeCaracteres);
            var empresa = CargoBuilder.Novo().ComDescricao(descricaoInvalido).Build();

            Assert.False(empresa.Validar());
        }
    }
}
