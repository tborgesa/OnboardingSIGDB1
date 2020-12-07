using FluentValidation;
using OnboardingSIGDB1.Domain._Base.Entidades;
using OnboardingSIGDB1.Domain._Base.Resources;
using OnboardingSIGDB1.Domain.Cargos.Entidades;
using OnboardingSIGDB1.Domain.Funcionarios.Resources;
using System;

namespace OnboardingSIGDB1.Domain.Funcionarios.Entidades
{
    public class CargoDoFuncionario : Entidade<int, CargoDoFuncionario>
    {
        public int FuncionarioId { get; private set; }
        public Funcionario Funcionario { get; private set; }

        public int CargoId { get; private set; }
        public Cargo Cargo { get; private set; }

        public DateTime DataDeVinculo { get; private set; }

        private CargoDoFuncionario() { }

        public CargoDoFuncionario(Funcionario funcionario, Cargo cargo, DateTime dataDeVinculo)
        {
            Funcionario = funcionario;
            FuncionarioId = funcionario == null ? 0 : funcionario.Id;

            Cargo = cargo;
            CargoId = cargo == null ? 0 : cargo.Id;

            DataDeVinculo = dataDeVinculo;
        }

        public override bool Validar()
        {
            RuleFor(_ => _.CargoId)
                 .GreaterThan(Constantes.Numero0);

            RuleFor(_ => _.FuncionarioId)
                 .GreaterThan(Constantes.Numero0);

            RuleFor(_ => _.DataDeVinculo)
                .Must(_ => _ > DateTime.MinValue)
                .WithMessage(Resource.FormatarResourceToLowerValor2(Resource.MensagemDeCampoInvalido, FuncionarioResources.DataDeVinculo));

            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
