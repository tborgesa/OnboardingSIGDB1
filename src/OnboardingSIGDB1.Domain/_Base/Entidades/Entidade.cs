using FluentValidation;
using FluentValidation.Results;

namespace OnboardingSIGDB1.Domain._Base.Entidades
{
    public abstract class Entidade<TId, TEntity> :
        AbstractValidator<TEntity>
        where TId : struct
        where TEntity : Entidade<TId, TEntity>
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
