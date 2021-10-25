namespace EOS2.Infrastructure.Interfaces.Repository
{
    using EOS2.Model;

    public interface IUnitOfWork
    {
        void SaveChanges();

        IRepository<TSet> GetRepository<TSet>() where TSet : class, IEntity;
    }
}