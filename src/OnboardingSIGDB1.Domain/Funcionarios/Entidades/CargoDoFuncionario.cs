﻿using FluentValidation;
using OnboardingSIGDB1.Domain._Base.Entidades;
using OnboardingSIGDB1.Domain._Base.Resources;
using OnboardingSIGDB1.Domain.Cargos.Entidades;
using OnboardingSIGDB1.Domain.Funcionarios.Resources;
using System;
using System.Linq;

namespace OnboardingSIGDB1.Domain.Funcionarios.Entidades
{
    public class CargoDoFuncionario : Entidade<int, CargoDoFuncionario>
    {
        public int FuncionarioId { get; private set; }
        public virtual Funcionario Funcionario { get; private set; }

        public int CargoId { get; private set; }
        public virtual Cargo Cargo { get; private set; }

        public DateTime DataDeVinculo { get; private set; }

        protected CargoDoFuncionario() { }

        public CargoDoFuncionario(Funcionario funcionario, Cargo cargo, DateTime dataDeVinculo)
        {
            Funcionario = funcionario;
            FuncionarioId = funcionario == null ? 0 : funcionario.Id;

            Cargo = cargo;
            CargoId = cargo == null ? 0 : cargo.Id;

            DataDeVinculo = dataDeVinculo;
        }

        private bool DeveSerCargoUnico(Funcionario funcionario)
        {
            return funcionario == null || !funcionario.ListaDeCargos.Any(_ => _.CargoId == CargoId);
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

            RuleFor(_ => Funcionario)
               .Must(DeveSerCargoUnico)
               .WithMessage(FuncionarioResources.FuncionarioJaFoiVinculadoNoCargo);

            RuleFor(_ => Funcionario)
               .Must(_ => _?.EmpresaId > Constantes.Numero0)
               .WithMessage(FuncionarioResources.FuncionarioSemEmpresaVinculado);

            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
