namespace TestingHelpers.Extensions
{
    using System;
    using System.Data.Entity;
    using System.Diagnostics.CodeAnalysis;

    public static class TestingDatabaseExtensions
    {
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Reviewed. Suppression is OK here.")]
        public static void Clear<TEntity>(this DbSet<TEntity> dbSet) where TEntity : class
        {
            if (dbSet == null) throw new ArgumentNullException("dbSet");

            dbSet.RemoveRange(dbSet);
        }
    }
}
