using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnboardingSIGDB1.Domain.Funcionarios.Entidades;

namespace OnboardingSIGDB1.Data.Funcionarios.Mapping
{
    public class CargoDoFuncionarioMapping : IEntityTypeConfiguration<CargoDoFuncionario>
    {
        public void Configure(EntityTypeBuilder<CargoDoFuncionario> builder)
        {
            builder.HasOne(_ => _.Funcionario)
                .WithMany(_ => _.ListaDeCargos)
                .HasForeignKey(_ => _.FuncionarioId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(_ => _.Cargo)
                .WithMany(_ => _.ListaDeFuncionarios)
                .HasForeignKey(_ => _.CargoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(_ => new { _.CargoId, _.FuncionarioId }).IsUnique();

            builder.Ignore(_ => _.ValidationResult);
            builder.Ignore(_ => _.CascadeMode);
        }
    }
}
