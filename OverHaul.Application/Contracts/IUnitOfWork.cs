using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Overhaul.Application.AutoFac;
using Overhaul.Domain.Common;

namespace Overhaul.Application.Contracts
{
    public interface IUnitOfWork : IDisposable,IScopedDependency
    {
        IRepository<TEntity> Repository<TEntity>() where TEntity : class, IEntity;
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        Task<int> SaveChangesAsync();
        int SaveChanges();
    }
}
