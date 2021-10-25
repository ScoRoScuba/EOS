namespace EOS2.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using EOS2.Infrastructure.Interfaces.Repository;
    using EOS2.Model;

    public class Repository<TEntity> : UOWRepository<TEntity>, IRepositoryAsync<TEntity> where TEntity : class, IEntity
    {
        public Repository(IDataContext dataContext) : base(dataContext)
        {
        }

        public override int Add(TEntity entity)
        {
            var id = base.Add(entity);

            this.DataContext.SaveChanges();

            return id;
        }

        public override void Update(TEntity entity)
        {
            base.Update(entity);
            this.DataContext.SaveChanges();
        }

        public override void Remove(TEntity entity)
        {
            base.Remove(entity);
            this.DataContext.SaveChanges();          
        }

        public async Task<int> AddAsync(TEntity entity)
        {
            var result = base.Add(entity);
            await DataContext.SaveChangesAsync();
            return result;
        }

        public async Task<int> UpdateAsync(TEntity entity)
        {
            base.Update(entity);            
            return await DataContext.SaveChangesAsync();
        }

        public virtual async Task<int> RemoveAsync(TEntity entity)
        {
            base.Remove(entity);
            return await DataContext.SaveChangesAsync();            
        }

        public async Task<IList<TEntity>> GetAllAsync()
        {
            return await DataContext.Set<TEntity>().ToListAsync();
        }

        public async Task<int> CountAsync()
        {
            return await DataContext.Set<TEntity>().CountAsync();
        }

        public async Task<IList<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DataContext.Set<TEntity>().Where(predicate).ToListAsync();            
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DataContext.Set<TEntity>().SingleOrDefaultAsync(predicate);
        }        
    }
}
