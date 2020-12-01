using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnboardingSIGDB1.Domain.Empresas.Entidades;

namespace OnboardingSIGDB1.Data.Empresas.Mapping
{
    public class EmpresaMapping : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            
        }
    }
}
