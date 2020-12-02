using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnboardingSIGDB1.Domain._Base.Resources;
using OnboardingSIGDB1.Domain.Funcionarios.Entidades;

namespace OnboardingSIGDB1.Data.Funcionarios.Mapping
{
    public class FuncionarioMapping : IEntityTypeConfiguration<Funcionario>
    {
        public void Configure(EntityTypeBuilder<Funcionario> builder)
        {
            builder.Property(_ => _.Nome).HasMaxLength(Constantes.Numero150).IsRequired();
            builder.Property(_ => _.Cpf).HasMaxLength(Constantes.Numero11).IsRequired();

            builder.Ignore(_ => _.ValidationResult);
            builder.Ignore(_ => _.CascadeMode);
        }
    }
}
