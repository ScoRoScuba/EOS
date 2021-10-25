namespace EOS2.Repository
{
    using System;
    using System.Collections.Generic;

    using EOS2.Infrastructure.Interfaces.Repository;
    using EOS2.Model;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDataContext dataContext;

        private readonly Dictionary<Type, object> repositories;

        public UnitOfWork(IDataContext dataContext)
        {
            this.dataContext = dataContext;
            repositories = new Dictionary<Type, object>();
        }

        public void SaveChanges()
        {
            dataContext.SaveChanges();
        }

        public IRepository<TSet> GetRepository<TSet>() where TSet : class, IEntity
        {        
            if (repositories.ContainsKey(typeof(TSet))) return repositories[typeof(TSet)] as IRepository<TSet>;

            var repository = new UOWRepository<TSet>(dataContext);

            repositories.Add(typeof(TSet), repository);

            return repository;
        }
    }
}
