using CompanyAddressBook.Core.Entities;
using CompanyAddressBook.Data;
using CompanyAddressBook.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using static System.Net.Mime.MediaTypeNames;
using System.Numerics;
using System.Reflection;

namespace CompanyAddressBook.Core.Entities.Business.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity<int>
    {
        private readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public virtual TEntity Add(TEntity newEntity)
        {
            try
            {
                if (newEntity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                _context.Set<TEntity>().Add(newEntity);

                if (_context.SaveChanges() > 0)
                    return newEntity;

                else
                    return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <inheritdoc />
        public TEntity Add(TEntity newEntity, string columnName)
        {
            try
            {
                // Get the property info for the specified column name
                PropertyInfo propInfo = typeof(TEntity).GetProperty(columnName);

                // Check if there is any entity in the database with the same property value
                bool exists = _context.Set<TEntity>().Any(e => EF.Property<object>(e, columnName).Equals(propInfo.GetValue(newEntity)));

                if (!exists)
                {
                    _context.Set<TEntity>().Add(newEntity);
                    _context.SaveChanges();
                    return newEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <inheritdoc />
        public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
            _context.SaveChanges();
            return entities;
        }

        /// <inheritdoc />
        public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities, string columnName)
        {
            try
            {
                // Get the property info for the specified column name
                PropertyInfo propInfo = typeof(TEntity).GetProperty(columnName);

                // Get a list of existing values for the specified column from the database
                var existingValues = _context.Set<TEntity>().Select(e => propInfo.GetValue(e)).ToList();

                // Add only the non-duplicate entities to the database
                foreach (var entity in entities.Where(e => !existingValues.Contains(propInfo.GetValue(e))))
                {
                    _context.Set<TEntity>().Add(entity);
                }

                // Save changes to the database
                _context.SaveChanges();

                return entities;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <inheritdoc />
        public bool Delete(int id)
        {
            try
            {
                TEntity deletedEntity = _context.Set<TEntity>().Find(id);

                if (deletedEntity != null)
                {
                    /*deletedEntity.IsDeleted = true;
                    deletedEntity.DeletionDate = DateTime.Now;*/
                    _context.Set<TEntity>().Remove(deletedEntity);
                    _context.SaveChanges();
                    return true;
                }

                throw new KeyNotFoundException();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <inheritdoc />
        public List<TEntity> GetAll(Func<TEntity, bool> predicate = null, params Expression<Func<TEntity, object>>[] includes)
        {
            var response = _context.Set<TEntity>().AsQueryable();

            if (includes.FirstOrDefault() != null)
                response = response.Include(includes.FirstOrDefault());
            else if (predicate != null)
                return response.Where(predicate).ToList();

            return response.ToList();

        }

        /// <inheritdoc />
        public TEntity GetById(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        /// <inheritdoc />
        public TEntity GetById(int id, params Expression<Func<TEntity, object>>[] includes)
        {
            var response = _context.Set<TEntity>().AsQueryable();

            if (includes.FirstOrDefault() != null)
                response = response.Include(includes.FirstOrDefault());

            return response.FirstOrDefault(s=>s.Id == id);
        }

        /// <inheritdoc />
        public bool Update(TEntity updatedEntity)
        {
            try
            {
                var entity = _context.Set<TEntity>().Find(updatedEntity.Id);
                _context.Entry<TEntity>(entity).CurrentValues.SetValues(updatedEntity);

                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
