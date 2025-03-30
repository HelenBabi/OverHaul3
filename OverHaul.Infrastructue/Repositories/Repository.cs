using Microsoft.EntityFrameworkCore;
using Overhaul.Application.Contracts;
using Overhaul.Domain.Common;
using Overhaul.Infrastructuer.Data;
using System.Linq.Expressions;
using Org.BouncyCastle.Asn1;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;


namespace Core.Data;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
{
    private readonly ApplicationDbContext dbContext;
    private DbSet<TEntity> Entities => dbContext.Set<TEntity>();
    
    public Repository(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    #region Async Methods
    public virtual Task<TEntity> GetAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken,
        IInclude<TEntity>? include = null
    )
    {
        return GetQuery(include).Where(predicate).FirstOrDefaultAsync(cancellationToken);
    }

    public virtual async Task<TResult> GetAsync<TResult>(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TResult>> selector,
        CancellationToken cancellationToken,
        IInclude<TEntity>? include = null
    )
    {
        return await GetQuery(include).Where(predicate).Select(selector).FirstOrDefaultAsync(cancellationToken);
    }

    public virtual async Task AddAsync(
        TEntity entity,
        CancellationToken cancellationToken
    )
    {
        await Entities.AddAsync(entity, cancellationToken);

    }

    public virtual async Task AddRangeAsync(
        IEnumerable<TEntity> entities,
        CancellationToken cancellationToken
    )
    {
        await Entities.AddRangeAsync(entities, cancellationToken);
    }

    public virtual async Task UpdateAsync(
        TEntity entity,
        CancellationToken cancellationToken
    )
    {
        dbContext.Entry(entity).Property("RowVersion").OriginalValue = entity.RowVersion;
        Entities.Update(entity);
    }

    public virtual async Task UpdateAsync(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TEntity>> updateFactory,
        CancellationToken cancellationToken
    )
    {
        await GetQuery().Where(predicate).UpdateFromQueryAsync(updateFactory, cancellationToken);
    }

    public virtual async Task DeleteAsync(
        TEntity entity,
        CancellationToken cancellationToken
    )
    {
        dbContext.Entry(entity).Property("RowVersion").OriginalValue = entity.RowVersion;
        Entities.Remove(entity);
    }

    public async Task DeleteAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken
    )
    {
        await GetQuery().Where(predicate).DeleteFromQueryAsync(cancellationToken);
    }

    public virtual async Task DeleteAsync(
        IQueryable<TEntity> entities,
        CancellationToken cancellationToken
    )
    {
        Entities.RemoveRange(entities);
    }
    #endregion

    #region Sync Methods
    public virtual TEntity Get(
        Expression<Func<TEntity, bool>> predicate,
        IInclude<TEntity>? include = null
    )
    {
        return GetQuery(include).FirstOrDefault(predicate);
    }
    public async Task<TEntity> GetByIdAsync(object id)
    {
            return await Entities.FindAsync(id);
    }
    public virtual TResult Get<TResult>(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TResult>> selector,
        IInclude<TEntity>? include = null
    )
    {
        return GetQuery(include).Where(predicate).Select(selector).FirstOrDefault();
    }

    public virtual IQueryable<TEntity> GetQuery(IInclude<TEntity>? include = null)
    {
        var query = Entities.AsNoTracking().AsQueryable();
        return include != null ? include.Execute(query) : query;
    }
    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null)
    {
        IQueryable<TEntity> query = dbContext.Set<TEntity>();

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        return await query.ToListAsync();
    }
    public virtual IQueryable<TResult> GetQuery<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        IInclude<TEntity>? include = null
    )
    {
        var query = Entities.AsNoTracking().AsQueryable();
        return include != null ? include.Execute(query).Select(selector) : query.Select(selector);
    }

    public virtual void Add(TEntity entity)
    {
        Entities.Add(entity);

    }

    public virtual void AddRange(IEnumerable<TEntity> entities)
    {

        try
        {
            Entities.AddRange(entities);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            
        }

    }

    public virtual void Update(TEntity entity)
    {
        dbContext.Entry(entity).Property("RowVersion").OriginalValue = entity.RowVersion;
        dbContext.Entry(entity).State = EntityState.Modified;
        Entities.Update(entity);

    }

    public virtual void Update(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, TEntity>> updateFactory
    )
    {
        GetQuery().Where(predicate).UpdateFromQuery(updateFactory);
    }

    public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
    {
        GetQuery().Where(predicate).DeleteFromQuery();
    }

    public virtual void Delete(TEntity entity)
    {
        dbContext.Entry(entity).Property("RowVersion").OriginalValue = entity.RowVersion;
        Entities.Remove(entity);
    }

    public virtual void Delete(IQueryable<TEntity> entities)
    {
        Entities.RemoveRange(entities);
    }
    #endregion

    #region Attach & Detach
    public virtual void Detach(TEntity entity)
    {
        var entry = dbContext.Entry(entity);
        if (entry != null)
            entry.State = EntityState.Detached;
    }

    public virtual void Attach(TEntity entity)
    {
        if (dbContext.Entry(entity).State == EntityState.Detached)
            Entities.Attach(entity);
    }
    #endregion

    #region Explicit Loading
    public virtual async Task LoadCollectionAsync<TProperty>(
        TEntity entity,
        Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty,
        CancellationToken cancellationToken
    ) where TProperty : class
    {
        Attach(entity);
        var collection = dbContext.Entry(entity).Collection(collectionProperty);
        if (!collection.IsLoaded)
            await collection.LoadAsync(cancellationToken);
    }

    public virtual void LoadCollection<TProperty>(
        TEntity entity,
        Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty
    ) where TProperty : class
    {
        dbContext.Entry(entity).Collection(collectionProperty).Load();
        Attach(entity);
        var collection = dbContext.Entry(entity).Collection(collectionProperty);
        if (!collection.IsLoaded)
            collection.Load();
    }








    #endregion
    public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null)
    {
        IQueryable<TEntity> query = dbContext.Set<TEntity>().AsNoTracking();

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        return query.ToList();
    }
    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        dbContext.RemoveRange(entities);
    }
}