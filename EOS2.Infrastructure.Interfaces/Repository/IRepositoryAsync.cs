namespace EOS2.Infrastructure.Interfaces.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using EOS2.Model;

    public interface IRepositoryAsync<TEntity> where TEntity : class, IEntity
    {
        Task<int> AddAsync(TEntity entity);

        Task<int> RemoveAsync(TEntity entity);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "This is a method declaration and not Property"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Generic Interface Declaration")]
        Task<IList<TEntity>> GetAllAsync();

        Task<int> UpdateAsync(TEntity entity);

        Task<int> CountAsync();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Design Choice")]
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Generic Interface Declaration")]
        Task<IList<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
