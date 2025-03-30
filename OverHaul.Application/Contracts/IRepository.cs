using Overhaul.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Overhaul.Application.Contracts;

public interface IRepository<TEntity> where TEntity : class, IEntity
{
    void Add(TEntity entity);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken);
    void AddRange(IEnumerable<TEntity> entities);
    Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
    void Attach(TEntity entity);
    void Delete(TEntity entity);
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);
    void Delete(Expression<Func<TEntity, bool>> predicate);
    Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
    void Delete(IQueryable<TEntity> entities);
    Task DeleteAsync(IQueryable<TEntity> entities, CancellationToken cancellationToken);
    void Detach(TEntity entity);
    Task<TEntity> GetByIdAsync(object id);
    TEntity Get(Expression<Func<TEntity, bool>> predicate, IInclude<TEntity>? include = null);
    TResult Get<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector, IInclude<TEntity>? include = null);
    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null);
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken, IInclude<TEntity>? include = null);
    Task<TResult> GetAsync<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector, CancellationToken cancellationToken, IInclude<TEntity>? include = null);
    IQueryable<TEntity> GetQuery(IInclude<TEntity>? include = null);
    IQueryable<TResult> GetQuery<TResult>(Expression<Func<TEntity, TResult>> selector, IInclude<TEntity>? include = null);
    void LoadCollection<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty) where TProperty : class;
    Task LoadCollectionAsync<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty, CancellationToken cancellationToken) where TProperty : class;
    void Update(TEntity entity);
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    void Update(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TEntity>> updateFactory);
    Task UpdateAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TEntity>> updateFactory, CancellationToken cancellationToken);
    IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null);
        void RemoveRange(IEnumerable<TEntity> entities);
}