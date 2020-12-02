namespace OnboardingSIGDB1.Domain._Base.Interfaces
{
    public interface IUnitOfWork
    {
        void Commit();
        void RoolBack();
    }
}
