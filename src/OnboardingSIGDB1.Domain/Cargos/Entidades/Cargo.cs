using FluentValidation;
using OnboardingSIGDB1.Domain._Base.Entidades;
using OnboardingSIGDB1.Domain._Base.Resources;
using OnboardingSIGDB1.Domain.Funcionarios.Entidades;
using System.Collections.Generic;

namespace OnboardingSIGDB1.Domain.Cargos.Entidades
{
    public class Cargo : Entidade<int, Cargo>
    {
        public string Descricao { get; private set; }
        public List<CargoDoFuncionario> ListaDeFuncionarios { get; private set; }

        public Cargo(string descricao)
        {
            Descricao = descricao;
        }

        public override bool Validar()
        {
            RuleFor(_ => _.Descricao)
               .NotNull()
               .NotEmpty()
               .MaximumLength(Constantes.Numero250);

            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }

        public void AlterarDescricao(string descricao)
        {
            Descricao = descricao;
        }
    }
}
