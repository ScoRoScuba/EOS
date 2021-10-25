namespace EOS2.Repository.Eventing
{
    using System;
    using System.Data.Entity;
    using System.Threading.Tasks;

    using EOS2.Infrastructure.Interfaces;
    using EOS2.Infrastructure.Interfaces.Repository;
    using EOS2.Model;

    public class EventingRepository<TEntity> : Repository<TEntity> where TEntity : class, IEntity
    {
        private readonly IEventingContext eventContext;

        public EventingRepository(IEventingContext eventContext, IDataContext dataContext)
            : base(dataContext)
        {
            this.eventContext = eventContext;
        }

        public override int Add(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            GenerateEvent("Add", entity);
       
            return base.Add(entity);
        }

        public override int Update(TEntity entity)
        {
            return base.Update(entity);
        }

        public override int Remove(TEntity entity)
        {
            return base.Remove(entity);
        }

        public override async Task<int> AddAsync(TEntity entity)
        {
            return await base.AddAsync(entity);
        }

        public override async Task<int> UpdateAsync(TEntity entity)
        {
            return await base.UpdateAsync(entity);
        }

        public override async Task<int> RemoveAsync(TEntity entity)
        {
            return await base.RemoveAsync(entity);
        }

        internal static IEventData BuildEventData(string eventTypeName, TEntity entity)
        {
            if (string.IsNullOrWhiteSpace(eventTypeName)) throw new ArgumentNullException("eventTypeName");
            if (entity == null) throw new ArgumentNullException("entity");

            return new BlankEvent();
        }

        internal void GenerateEvent(string eventPrefix, TEntity entity)
        {
            var nameOfType = entity.GetType().Name;
            var eventTypeName = eventPrefix + nameOfType;

            var eventData = BuildEventData(eventTypeName, entity);

            eventContext.StoreEvent(eventData);
        }
    }
}
