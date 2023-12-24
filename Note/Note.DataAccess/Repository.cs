﻿using System.Linq.Expressions;
using Note.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Note.DataAccess.Entities;
using Note.DataAccess;

namespace Note.DataAccess
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        public Repository(IDbContextFactory<NoteDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IQueryable<T> GetAll()
        {
            using var context = _contextFactory.CreateDbContext();
            return context.Set<T>();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter)
        {
            using var context = _contextFactory.CreateDbContext();
            return context.Set<T>().Where(filter).ToList();
        }

        public T? GetById(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            return context.Set<T>().FirstOrDefault(x => x.Id == id);
        }

        public T? GetById(Guid id)
        {
            using var context = _contextFactory.CreateDbContext();
            return context.Set<T>().FirstOrDefault(x => x.ExternalId == id);
        }

        public T Save(T entity)
        {
            using var context = _contextFactory.CreateDbContext();
            if (context.Set<T>().Any(x => x.Id == entity.Id))
            {
                entity.ModificationTime = DateTime.UtcNow;
                var result = context.Set<T>().Attach(entity);
                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();
                return result.Entity;
            }
            else
            {
                entity.ExternalId = Guid.NewGuid();
                entity.CreationTime = DateTime.UtcNow;
                entity.ModificationTime = entity.CreationTime;
                var result = context.Set<T>().Add(entity);
                context.SaveChanges();
                return result.Entity;
            }
        }

        public void Delete(T entity)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Set<T>().Attach(entity);
            context.Entry(entity).State = EntityState.Deleted;
            context.SaveChanges();
        }

        private readonly IDbContextFactory<NoteDbContext> _contextFactory;
    }
}