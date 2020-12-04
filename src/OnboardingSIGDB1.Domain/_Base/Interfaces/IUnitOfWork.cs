using System.Threading.Tasks;

namespace OnboardingSIGDB1.Domain._Base.Interfaces
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
        Task RoolBackAsync();
    }
}
