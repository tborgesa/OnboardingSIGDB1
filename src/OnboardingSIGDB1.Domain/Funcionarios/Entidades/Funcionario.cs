using FluentValidation;
using OnboardingSIGDB1.Domain._Base.Entidades;
using OnboardingSIGDB1.Domain._Base.Helpers;
using OnboardingSIGDB1.Domain._Base.Resources;
using OnboardingSIGDB1.Domain.Empresas.Entidades;
using OnboardingSIGDB1.Domain.Funcionarios.Resources;
using System;
using System.Collections.Generic;

namespace OnboardingSIGDB1.Domain.Funcionarios.Entidades
{
    public class Funcionario : Entidade<int, Funcionario>
    {
        public string Nome { get; private set; }
        public string Cpf { get; private set; }
        public DateTime? DataDeContratacao { get; private set; }

        public int? EmpresaId { get; private set; }
        public Empresa Empresa { get; private set; }

        public List<CargoDoFuncionario> ListaDeCargos { get; private set; }

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

        internal bool ValidarOVinculoComEmpresa()
        {
            RuleFor(_ => _.EmpresaId)
                 .Null()
                 .WithMessage(FuncionarioResources.FuncionarioJaEstaVinculadoAEmpresa);

            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }

        public void AlterarNome(string nome)
        {
            Nome = nome;
        }

        public void AlterarCpf(string cpf)
        {
            Cpf = cpf.RemoverMascaraDoCpf();
        }

        public void AlterarDataDeContratacao(DateTime dataDeContratacao)
        {
            DataDeContratacao = dataDeContratacao;
        }

        public void VincularComEmpresa(Empresa empresa)
        {
            Empresa = empresa;
            EmpresaId = empresa.Id;
        }
    }
}
