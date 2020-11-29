using Bogus;
using OnboardingSIGDB1.Domain._Base.Resources;
using OnboardingSIGDB1.Domain.Cargos.Entidades;
using OnboardingSIGDB1.Domain.Test.Builders;
using OnboardingSIGDB1.Domain.Test.Common;
using Xunit;

namespace OnboardingSIGDB1.Domain.Test.Cargos
{
    public class CargoTestes
    {
        private readonly Faker _faker;
        private readonly string _descricao;

        public CargoTestes()
        {
            _faker = FakerBuilder.Novo().Build();
            _descricao = _faker.Lorem.Random.AlphaNumeric(Constantes.Numero250);
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
        [InlineData(Constantes.Numero251)]
        public void NaoDeveAceitarDescricaoComQuantidadeCaracterInvalido(int quantidadeDeCaracteres)
        {
            var descricaoInvalido = _faker.Lorem.Random.AlphaNumeric(quantidadeDeCaracteres);
            var empresa = CargoBuilder.Novo().ComDescricao(descricaoInvalido).Build();

            Assert.False(empresa.Validar());
        }
    }
}
