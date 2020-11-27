using FluentValidation;
using OnboardingSIGDB1.Domain._Base.Entidades;
using OnboardingSIGDB1.Domain._Base.Helpers;
using OnboardingSIGDB1.Domain._Base.Resources;
using System;

namespace OnboardingSIGDB1.Domain.Empresas.Entidades
{
    public class Empresa : Entidade<int, Empresa>
    {
        public string Nome { get; private set; }
        public string Cnpj { get; private set; }
        public DateTime? DataDeFundacao { get; private set; }

        public Empresa(string nome, string cnpj, DateTime? dataDeFundacao)
        {
            Nome = nome;
            Cnpj = cnpj.RemoverMascaraDoCnpj();
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
                .MaximumLength(Constantes.QuantidadeDeCaracteres150);

            RuleFor(_ => _.Cnpj)
                .Must(DeveSerCnpjValido)
                .WithMessage(EmpresaResources.CnpjInvalido);

            RuleFor(_ => _.DataDeFundacao)
                .Must(_ => _ > DateTime.MinValue)
                .When(_ => _.DataDeFundacao != null);

            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
