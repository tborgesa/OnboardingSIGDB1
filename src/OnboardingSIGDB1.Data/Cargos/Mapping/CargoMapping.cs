using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnboardingSIGDB1.Domain._Base.Resources;
using OnboardingSIGDB1.Domain.Cargos.Entidades;

namespace OnboardingSIGDB1.Data.Cargos.Mapping
{
    public class CargoMapping : IEntityTypeConfiguration<Cargo>
    {
        public void Configure(EntityTypeBuilder<Cargo> builder)
        {
            builder.Property(_ => _.Descricao).HasMaxLength(Constantes.Numero250).IsRequired();

            builder.Ignore(_ => _.ValidationResult);
            builder.Ignore(_ => _.CascadeMode);
        }
    }
}
