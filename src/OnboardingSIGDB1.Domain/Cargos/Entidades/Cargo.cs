using FluentValidation;
using OnboardingSIGDB1.Domain._Base.Entidades;
using OnboardingSIGDB1.Domain._Base.Resources;

namespace OnboardingSIGDB1.Domain.Cargos.Entidades
{
    public class Cargo : Entidade<int, Cargo>
    {
        public string Descricao { get; private set; }
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
    }
}
