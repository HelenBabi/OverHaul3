using Overhaul.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overhaul.Application.Contracts
{
    public interface IInclude<TEntity> where TEntity : class, IEntity
    {
        T Execute<T>(T query) where T : IQueryable<TEntity>;
    }
}
