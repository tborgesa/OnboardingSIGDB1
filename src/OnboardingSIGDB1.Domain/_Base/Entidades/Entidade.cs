using FluentValidation;
using FluentValidation.Results;

namespace OnboardingSIGDB1.Domain._Base.Entidades
{
    public abstract class Entidade<TId, TEntidade> :
        AbstractValidator<TEntidade>
        where TId : struct
        where TEntidade : Entidade<TId, TEntidade>
    {
        public TId Id { get; protected set; }

        public ValidationResult ValidationResult { get; protected set; }

        public abstract bool Validar();

        protected Entidade()
        {
            Id = default(TId);
            ValidationResult = new ValidationResult();
        }
    }
}
