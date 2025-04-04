﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Data;
using Microsoft.EntityFrameworkCore.Storage;
using Overhaul.Application.Contracts;
using Overhaul.Domain.Common;
using Overhaul.Infrastructuer.Data;

namespace Overhaul.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction _transaction;
        private readonly Dictionary<Type, object> _repositories = new();

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class, IEntity
        {
            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
            {
                var repositoryInstance = new Repository<TEntity>(_context);
                _repositories[type] = repositoryInstance;
            }

            return (IRepository<TEntity>)_repositories[type];
        }

        public async Task BeginTransactionAsync()
        {
            _transaction ??= await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                if (_transaction != null)
                {
                    await _transaction.CommitAsync();
                }
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                _transaction.Dispose();
                _transaction = null;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }


    }
}
