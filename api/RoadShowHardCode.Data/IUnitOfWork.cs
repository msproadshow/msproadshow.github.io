namespace RoadShowHardCode.Data
{
    using System;
    using System.Collections.Generic;

    using RoadShowHardCode.Data.Context;
    using RoadShowHardCode.Data.Repository;

    /// <summary>
    /// The UnitOfWork interface.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// The begin transaction.
        /// </summary>
        /// <returns>
        /// The <see cref="IDbContextTransaction"/>.
        /// </returns>
        IDbContextTransaction BeginTransaction();

        /// <summary>
        /// The repository.
        /// </summary>
        /// <typeparam name="T">
        /// Any reference type
        /// </typeparam>
        /// <returns>
        /// The <see cref="IRepository{T}"/>.
        /// </returns>
        IRepository<T> Repository<T>() where T : class;

        /// <summary>
        /// The save.
        /// </summary>
        void Save();

        /// <summary>
        /// The sql query.
        /// </summary>
        /// <param name="sql">
        /// The sql.
        /// </param>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        /// <typeparam name="T">
        /// Any type
        /// </typeparam>
        /// <returns>
        /// The <see cref="IEnumerable{T}"/>.
        /// </returns>
        IEnumerable<T> SqlQuery<T>(string sql, params object[] parameters);

        /// <summary>
        /// Run SQL query.
        /// </summary>
        /// <param name="sql">
        /// The SQL query.
        /// </param>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        /// <returns><see cref="int"/> with number of affected rows</returns>
        int SqlCommand(string sql, params object[] parameters);
    }
}