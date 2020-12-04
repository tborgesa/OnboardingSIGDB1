using Microsoft.EntityFrameworkCore;
using OnboardingSIGDB1.Domain._Base.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace OnboardingSIGDB1.Data._Contexto
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OnboardingSIGDB1Context _onboardingSIGDB1Context;

        public UnitOfWork(OnboardingSIGDB1Context onboardingSIGDB1Context)
        {
            _onboardingSIGDB1Context = onboardingSIGDB1Context;
        }

        public async Task CommitAsync()
        {
            await _onboardingSIGDB1Context.SaveChangesAsync();
        }

        public async Task RoolBackAsync()
        {
            var entries = _onboardingSIGDB1Context.ChangeTracker
                .Entries()
                .Where(_ => _.State != EntityState.Unchanged)
                .ToList();

            for (var i = 0; i < entries.Count; i++)
            {
                var entry = entries.ElementAt(i);
                switch (entry.State)
                {
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                }
            }

            await Task.CompletedTask;

        }
    }
}
