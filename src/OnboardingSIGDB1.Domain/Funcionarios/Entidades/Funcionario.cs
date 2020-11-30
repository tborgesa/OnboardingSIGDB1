﻿using FluentValidation;
using OnboardingSIGDB1.Domain._Base.Entidades;
using OnboardingSIGDB1.Domain._Base.Helpers;
using OnboardingSIGDB1.Domain._Base.Resources;
using OnboardingSIGDB1.Domain.Funcionarios.Resources;
using System;

namespace OnboardingSIGDB1.Domain.Funcionarios.Entidades
{
    public class Funcionario : Entidade<int, Funcionario>
    {
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public DateTime? DataDeContratacao { get; private set; }

        public Funcionario(string nome, string cpf, DateTime? dataDeContratacao)
        {
            Nome = nome;
            Cpf = cpf.RemoverMascaraDoCpf();
            DataDeContratacao = dataDeContratacao;
        }

        private bool DeveSerCpfValido(string cpf)
        {
            return cpf.ValidarCpf();
        }

        public override bool Validar()
        {
            RuleFor(_ => _.Nome)
                 .NotNull()
                 .NotEmpty()
                 .MaximumLength(Constantes.Numero150);

            RuleFor(_ => _.Cpf)
                .Must(DeveSerCpfValido)
                .WithMessage(Resource.FormatarResource(Resource.MensagemDeCampoInvalido, FuncionarioResources.Cpf));

            RuleFor(_ => _.DataDeContratacao)
                .Must(_ => _ > DateTime.MinValue)
                .When(_ => _.DataDeContratacao != null)
                .WithMessage(Resource.FormatarResourceToLowerValor2(Resource.MensagemDeCampoInvalido, FuncionarioResources.DataDeContratacao));

            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
