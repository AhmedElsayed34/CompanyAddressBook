using CompanyAddressBook.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CompanyAddressBook.Data.Repositories
{
    /// <summary>
    /// used to made CRUD operation
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity<int>
    {
        /// <summary>
        /// add record
        /// </summary>
        /// <param name="newEntity"></param>
        /// <returns></returns>
        TEntity Add(TEntity newEntity);

        /// <summary>
        /// add record with check duplication in db based on columnName parameter
        /// </summary>
        /// <param name="entities">Entities</param>
        /// <param name="columnName">The column name that check duplication based on it</param>
        /// <returns></returns>
        TEntity Add(TEntity newEntity, string columnName);

        /// <summary>
        /// add records
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// add range records with check duplication in db based on columnName parameter
        /// </summary>
        /// <param name="entities">Entities</param>
        /// <param name="columnName">The column name that check duplication based on it</param>
        /// <returns></returns>
        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities, string columnName);

        /// <summary>
        /// delete the record
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(int id);

        /// <summary>
        /// get all 
        /// </summary>
        /// <param name="predicate">where clause</param>
        /// <param name="includes">joins</param>
        /// <returns></returns>
        List<TEntity> GetAll(Func<TEntity, bool> predicate = null, params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// get the selected record
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetById(int id);

        /// <summary>
        /// get the selected record
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includes">joins</param>
        TEntity GetById(int id, params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// update the record
        /// </summary>
        /// <param name="updatedEntity"></param>
        /// <returns></returns>
        bool Update(TEntity updatedEntity);
    }
}
