using OnboardingSIGDB1.Domain.Cargos.Entidades;
using OnboardingSIGDB1.Domain.Funcionarios.Entidades;
using OnboardingSIGDB1.Domain.Test._Builders;
using OnboardingSIGDB1.Domain.Test._Comum;
using System;
using Xunit;

namespace OnboardingSIGDB1.Domain.Test.Funcionarios
{
    public class CargoDoFuncionarioTestes
    {
        private readonly Funcionario _funcionario;
        private readonly Cargo _cargo;
        private readonly DateTime _dataDeVinvulo;

        private readonly OnboardingSIGDB1Faker _onboardingSIGDB1faker;

        public CargoDoFuncionarioTestes()
        {
            _onboardingSIGDB1faker = OnboardingSIGDB1FakerBuilder.Novo().Build();
            _dataDeVinvulo = _onboardingSIGDB1faker.QualquerDataDoUltimoAno();

            _funcionario = FuncionarioBuilder.Novo().ComId(_onboardingSIGDB1faker.Id()).Build();
            _cargo = CargoBuilder.Novo().ComId(_onboardingSIGDB1faker.Id()).Build();
        }

        [Fact]
        public void DeveCriarOCargoDoFuncionario()
        {
            var cargoDoFuncionario = new CargoDoFuncionario(_funcionario, _cargo, _dataDeVinvulo);

            Assert.Equal(_funcionario, cargoDoFuncionario.Funcionario);
            Assert.Equal(_funcionario.Id, cargoDoFuncionario.FuncionarioId);

            Assert.Equal(_cargo, cargoDoFuncionario.Cargo);
            Assert.Equal(_cargo.Id, cargoDoFuncionario.CargoId);

            Assert.Equal(_dataDeVinvulo, cargoDoFuncionario.DataDeVinculo);
        }

        [Fact]
        public void NaoDeveAceitarVinculoSemFuncionario()
        {
            Funcionario funcionarioInvalido = null;

            var cargoDoFuncionario = new CargoDoFuncionario(funcionarioInvalido, _cargo, _dataDeVinvulo);

            Assert.False(cargoDoFuncionario.Validar());
        }

        [Fact]
        public void NaoDeveAceitarVinculoComFuncionarioSemCadastro()
        {
            var funcionarioInvalido = FuncionarioBuilder.Novo().Build();

            var cargoDoFuncionario = new CargoDoFuncionario(funcionarioInvalido, _cargo, _dataDeVinvulo);

            Assert.False(cargoDoFuncionario.Validar());
        }

        [Fact]
        public void NaoDeveAceitarVinculoSemCargo()
        {
            Cargo cargoInvalido = null;

            var cargoDoFuncionario = new CargoDoFuncionario(_funcionario, cargoInvalido, _dataDeVinvulo);

            Assert.False(cargoDoFuncionario.Validar());
        }

        [Fact]
        public void NaoDeveAceitarVinculoComCargoSemCadastro()
        {
            var cargoInvalido = CargoBuilder.Novo().Build();

            var cargoDoFuncionario = new CargoDoFuncionario(_funcionario, cargoInvalido, _dataDeVinvulo);

            Assert.False(cargoDoFuncionario.Validar());
        }

        [Fact]
        public void NaoDeveAceitarVinculoComDataInvalida()
        {
            var dataInvalida = DateTime.MinValue;

            var cargoDoFuncionario = new CargoDoFuncionario(_funcionario, _cargo, dataInvalida);

            Assert.False(cargoDoFuncionario.Validar());
        }
    }
}
