namespace EOS2.Infrastructure.Interfaces.Repository
{
    using System.Collections.Generic;
    using EOS2.Model;

    public interface ICriteria<TEntity> where TEntity : class, IEntity
    {
        IList<TEntity> MatchQueryFrom(IRepository<TEntity> ds);
    }
}
