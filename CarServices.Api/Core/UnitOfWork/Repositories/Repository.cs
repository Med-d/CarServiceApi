using System.Linq.Expressions;
using CarServices.Api.Core.UnitOfWork.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace CarServices.Api.Core.UnitOfWork.Repositories;

public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : class
{
    protected readonly CarServiceDbContext Context;

    public Repository(CarServiceDbContext context)
    {
        this.Context = context;
    }

    #region CRUD

    public async Task<TEntity?> Get(string key) => 
        await Context.Set<TEntity>().FindAsync(key);

    public async Task<IEnumerable<TEntity>> GetAll() => 
        await Context.Set<TEntity>().ToListAsync();

    public async Task<TEntity?> Find(Expression<Func<TEntity, bool>> predicate) =>
        await Context.Set<TEntity>().FirstOrDefaultAsync(predicate);

    public async Task<IEnumerable<TEntity>> GetWhere(Expression<Func<TEntity, bool>> predicate) => 
        await Context.Set<TEntity>().Where(predicate).ToListAsync();

    public void Add(TEntity entity) => 
        Context.Set<TEntity>().Add(entity);

    public void AddMany(IEnumerable<TEntity> entity) => 
        Context.Set<TEntity>().AddRange(entity);

    public void Update(TEntity entity) => 
        Context.Set<TEntity>().Update(entity);

    public void Delete(TEntity entity) => 
        Context.Set<TEntity>().Remove(entity);

    #endregion
}