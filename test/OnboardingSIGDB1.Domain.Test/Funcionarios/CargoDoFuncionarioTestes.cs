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

            var empresa = EmpresaBuilder.Novo().ComId(_onboardingSIGDB1faker.Id()).Build();
            _funcionario = FuncionarioBuilder.Novo().ComId(_onboardingSIGDB1faker.Id()).ComEmpresa(empresa).Build();
            _cargo = CargoBuilder.Novo().ComId(_onboardingSIGDB1faker.Id()).Build();
        }

        [Fact]
        public void DeveCriarOCargoDoFuncionarioValido()
        {
            var cargoDoFuncionario = new CargoDoFuncionario(_funcionario, _cargo, _dataDeVinvulo);

            Assert.Equal(_funcionario, cargoDoFuncionario.Funcionario);
            Assert.Equal(_funcionario.Id, cargoDoFuncionario.FuncionarioId);

            Assert.Equal(_cargo, cargoDoFuncionario.Cargo);
            Assert.Equal(_cargo.Id, cargoDoFuncionario.CargoId);

            Assert.Equal(_dataDeVinvulo, cargoDoFuncionario.DataDeVinculo);

            Assert.True(cargoDoFuncionario.Validar());
        }

        [Fact]
        public void NaoDeveAceitarVinculoSemFuncionario()
        {
            Funcionario funcionarioInvalido = null;

            var cargoDoFuncionario = CargoDoFuncionarioBuilder.Novo().ComFuncionario(funcionarioInvalido).Build();

            Assert.False(cargoDoFuncionario.Validar());
        }

        [Fact]
        public void NaoDeveAceitarVinculoComFuncionarioSemCadastro()
        {
            var funcionarioInvalido = FuncionarioBuilder.Novo().Build();

            var cargoDoFuncionario = CargoDoFuncionarioBuilder.Novo().ComFuncionario(funcionarioInvalido).Build();

            Assert.False(cargoDoFuncionario.Validar());
        }

        [Fact]
        public void NaoDeveAceitarVinculoSemCargo()
        {
            Cargo cargoInvalido = null;

            var cargoDoFuncionario = CargoDoFuncionarioBuilder.Novo().ComCargo(cargoInvalido).Build();

            Assert.False(cargoDoFuncionario.Validar());
        }

        [Fact]
        public void NaoDeveAceitarVinculoComCargoSemCadastro()
        {
            var cargoInvalido = CargoBuilder.Novo().Build();

            var cargoDoFuncionario = CargoDoFuncionarioBuilder.Novo().ComCargo(cargoInvalido).Build();

            Assert.False(cargoDoFuncionario.Validar());
        }

        [Fact]
        public void NaoDeveAceitarVinculoComDataInvalida()
        {
            var dataInvalida = DateTime.MinValue;

            var cargoDoFuncionario = CargoDoFuncionarioBuilder.Novo().ComDataDeVinculo(dataInvalida).Build();

            Assert.False(cargoDoFuncionario.Validar());
        }

        [Fact]
        public void NaoDeveAceitarCargoRepetidosParaOMesmoFuncionario()
        {
            var cargoDoFuncionario = CargoDoFuncionarioBuilder.Novo().Build();
            cargoDoFuncionario.Funcionario.AdicionarCargo(cargoDoFuncionario);

            Assert.False(cargoDoFuncionario.Validar());
        }

        [Fact]
        public void NaoDeveAceitarFuncionarioSemEmpresa()
        {
            var funcionarioSemEmpresa = FuncionarioBuilder.Novo().ComId(_onboardingSIGDB1faker.Id()).Build();

            var cargoDoFuncionario = CargoDoFuncionarioBuilder.Novo().ComFuncionario(funcionarioSemEmpresa).Build();

            Assert.False(cargoDoFuncionario.Validar());
        }
    }
}
