using System.Linq.Expressions;

namespace CarServices.Api.Core.UnitOfWork.Repositories;

public interface IRepository<TEntity>
    where TEntity : class
{
    Task<TEntity?> Get(string key);
    Task<IEnumerable<TEntity>> GetAll();
    Task<TEntity?> Find(Expression<Func<TEntity, bool>> predicate);
    Task<IEnumerable<TEntity>> GetWhere(Expression<Func<TEntity, bool>> predicate);

    void Add(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}