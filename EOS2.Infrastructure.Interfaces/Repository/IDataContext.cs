namespace EOS2.Infrastructure.Interfaces.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Threading.Tasks;

    public interface IDataContext
    {
        Database Database { get; }

        DbContextConfiguration Configuration { get; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "This is override method and is Method not Function")]
        IEnumerable<DbEntityEntry> GetChangeTrackerEntries();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Set", Justification = "This is design choice to keep Interface in line with Entity Framework")]
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Set", Justification = "This is design choice to keep Interface in line with Entity Framework")]
        DbSet Set(Type type);

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        DbEntityEntry Entry(object entity);

        void Update<T>(T entityToUpdate) where T : class;

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
