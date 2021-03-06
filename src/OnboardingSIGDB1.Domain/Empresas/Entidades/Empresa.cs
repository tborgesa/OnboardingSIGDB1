﻿using FluentValidation;
using OnboardingSIGDB1.Domain._Base.Entidades;
using OnboardingSIGDB1.Domain._Base.Helpers;
using OnboardingSIGDB1.Domain._Base.Resources;
using OnboardingSIGDB1.Domain.Empresas.Resources;
using OnboardingSIGDB1.Domain.Funcionarios.Entidades;
using System;
using System.Collections.Generic;

namespace OnboardingSIGDB1.Domain.Empresas.Entidades
{
    public class Empresa : Entidade<int, Empresa>
    {
        public string Nome { get; private set; }
        public string Cnpj { get; private set; }
        public DateTime? DataDeFundacao { get; private set; }

        public virtual List<Funcionario> ListaDeFuncionarios { get; private set; }

        public Empresa(string nome, string cnpj, DateTime? dataDeFundacao)
        {
            Nome = nome;
            Cnpj = cnpj.RemoverMascaraDoCnpj();
            DataDeFundacao = dataDeFundacao;
            ListaDeFuncionarios = new List<Funcionario>();
        }

        public void AlterarNome(string nome)
        {
            Nome = nome;
        }

        public void AlterarCnpj(string cnpj)
        {
            Cnpj = cnpj.RemoverMascaraDoCnpj();
        }

        public void AlterarDataDeFundacao(DateTime? dataDeFundacao)
        {
            DataDeFundacao = dataDeFundacao;
        }

        private bool DeveSerCnpjValido(string cnpj)
        {
            return cnpj.ValidarCnpj();
        }

        public override bool Validar()
        {
            RuleFor(_ => _.Nome)
                .NotNull()
                .NotEmpty()
                .MaximumLength(Constantes.Numero150);

            RuleFor(_ => _.Cnpj)
                .Must(DeveSerCnpjValido)
                .WithMessage(Resource.FormatarResource(Resource.MensagemDeCampoInvalido, EmpresaResources.Cnpj));

            RuleFor(_ => _.DataDeFundacao)
                .Must(_ => _ > DateTime.MinValue)
                .When(_ => _.DataDeFundacao != null)
                .WithMessage(Resource.FormatarResourceToLowerValor2(Resource.MensagemDeCampoInvalido, EmpresaResources.DataDeFundacao));

            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }

    }
}
