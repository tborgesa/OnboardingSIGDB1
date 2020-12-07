using OnboardingSIGDB1.Domain.Cargos.Entidades;
using OnboardingSIGDB1.Domain.Funcionarios.Entidades;
using OnboardingSIGDB1.Domain.Test._Comum;
using System;

namespace OnboardingSIGDB1.Domain.Test._Builders
{
    public class CargoDoFuncionarioBuilder
    {
        private static DateTime _dataDeVinvulo;
        private static Funcionario _funcionario;
        private static Cargo _cargo;

        public static CargoDoFuncionarioBuilder Novo()
        {
            var onboardingSIGDB1faker = OnboardingSIGDB1FakerBuilder.Novo().Build();
            _dataDeVinvulo = onboardingSIGDB1faker.QualquerDataDoUltimoAno();

            var empresa = EmpresaBuilder.Novo().ComId(onboardingSIGDB1faker.Id()).Build();
            _funcionario = FuncionarioBuilder.Novo().ComId(onboardingSIGDB1faker.Id()).ComEmpresa(empresa).Build();
            _cargo = CargoBuilder.Novo().ComId(onboardingSIGDB1faker.Id()).Build();
            return new CargoDoFuncionarioBuilder();
        }

        internal CargoDoFuncionarioBuilder ComFuncionario(Funcionario funcionario)
        {
            _funcionario = funcionario;
            return this;
        }

        internal CargoDoFuncionarioBuilder ComCargo(Cargo cargo)
        {
            _cargo = cargo;
            return this;
        }

        internal CargoDoFuncionarioBuilder ComDataDeVinculo(DateTime dataDeVinculo)
        {
            _dataDeVinvulo = dataDeVinculo;
            return this;
        }

        public CargoDoFuncionario Build()
        {
            return new CargoDoFuncionario(_funcionario, _cargo, _dataDeVinvulo);
        }
    }
}
