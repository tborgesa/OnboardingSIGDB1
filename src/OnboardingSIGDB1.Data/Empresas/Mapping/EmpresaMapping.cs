using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnboardingSIGDB1.Domain._Base.Resources;
using OnboardingSIGDB1.Domain.Empresas.Entidades;

namespace OnboardingSIGDB1.Data.Empresas.Mapping
{
    public class EmpresaMapping : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            builder.Property(_ => _.Nome).HasMaxLength(Constantes.Numero150).IsRequired();
            builder.Property(_ => _.Cnpj).HasMaxLength(Constantes.Numero14).IsRequired();

            builder.Ignore(_ => _.ValidationResult);
            builder.Ignore(_ => _.CascadeMode);
        }
    }
}
