using Microsoft.EntityFrameworkCore;

namespace OnboardingSIGDB1.Data._Contexto
{
    public class OnboardingSIGDB1Context : DbContext
    {
        public OnboardingSIGDB1Context(DbContextOptions<OnboardingSIGDB1Context> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(OnboardingSIGDB1Context).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
