namespace EOS2.Infrastructure.Interfaces.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using EOS2.Model;

    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        int Add(TEntity entity);

        void Remove(TEntity entity);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "This is a method declaration and not Property")]
        IList<TEntity> GetAll();

        void Update(TEntity entity);

        int Count();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Design Choice for Generics")]
        TEntity Find(Expression<Func<TEntity, bool>> predicate);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Generic Interface Declaration")]
        IList<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate);

        IList<TEntity> Match(ICriteria<TEntity> criteria);
    }
}
