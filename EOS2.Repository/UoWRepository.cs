namespace EOS2.Repository
{
    using System;
    using System.Data.Entity;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    using EOS2.Infrastructure.Interfaces.Repository;
    using EOS2.Model;

    // ReSharper disable once InconsistentNaming
    [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "UOW", Justification = "Is valid abbreviation for Unit Of Work")]
    public class UOWRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        public UOWRepository(IDataContext dataContext)
        {
            if (dataContext == null) throw new ArgumentNullException("dataContext");

            this.DataContext = dataContext;         
        }

        protected IDataContext DataContext { get; private set; }

        public virtual int Add(TEntity entity)
        {
            DataContext.Set<TEntity>().Add(entity);
            return entity.Id;
        }

        public virtual void Remove(TEntity entity)
        {
            DataContext.Entry(entity).State = EntityState.Deleted;
        }

        public virtual System.Collections.Generic.IList<TEntity> GetAll()
        {
            return DataContext.Set<TEntity>().ToList();            
        }

        public virtual void Update(TEntity entity)
        {
            DataContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual int Count()
        {
            return DataContext.Set<TEntity>().Count();
        }

        public virtual TEntity Find(System.Linq.Expressions.Expression<System.Func<TEntity, bool>> predicate)
        {
            return DataContext.Set<TEntity>().SingleOrDefault(predicate);
        }

        public System.Collections.Generic.IList<TEntity> FindAll(System.Linq.Expressions.Expression<System.Func<TEntity, bool>> predicate)
        {
            return DataContext.Set<TEntity>().Where(predicate).ToList();
        }

        public virtual System.Collections.Generic.IList<TEntity> Match(ICriteria<TEntity> criteria)
        {
            if (criteria == null) throw new ArgumentNullException("criteria");

            return criteria.MatchQueryFrom(this);
        }
    }
}
