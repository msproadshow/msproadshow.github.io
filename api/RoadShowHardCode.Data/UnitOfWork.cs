namespace RoadShowHardCode.Data
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    using RoadShowHardCode.Data.Context;
    using RoadShowHardCode.Data.Repository;

    /// <summary>
    /// The marketing unit of work.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// The _context.
        /// </summary>
        protected readonly IDbContext Context;

        /// <summary>
        /// The _disposed.
        /// </summary>
        private bool disposed;

        /// <summary>
        /// The repositories.
        /// </summary>
        private Hashtable repositories;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        public UnitOfWork()
            : this(new DatabaseContext())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public UnitOfWork(DatabaseContext context)
        {
            this.Context = context;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        ~UnitOfWork()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// The begin transaction.
        /// </summary>
        /// <returns>
        /// The <see cref="IDbContextTransaction"/>.
        /// </returns>
        public IDbContextTransaction BeginTransaction() => this.Context.BeginTransaction();

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc />
        public IRepository<T> Repository<T>()
            where T : class
        {
            var type = typeof(T).Name;

            if (this.repositories == null)
            {
                this.repositories = new Hashtable();
            }

            if (!this.repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);

                var repositoryInstance =
                    Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), this.Context);

                this.repositories.Add(type, repositoryInstance);
            }

            return (IRepository<T>)this.repositories[type];
        }

        /// <inheritdoc />
        public void Save()
        {
            this.Context.SaveChanges();
        }

        /// <inheritdoc />
        public int SqlCommand(string sql, params object[] parameters) => ((DatabaseContext)this.Context).Database.ExecuteSqlCommand(sql, parameters);

        /// <inheritdoc />
        public IEnumerable<T> SqlQuery<T>(string sql, params object[] parameters) => ((DatabaseContext)this.Context).Database.SqlQuery<T>(sql, parameters);

        /// <summary>
        /// The dispose.
        /// </summary>
        /// <param name="disposing">
        /// The disposing.
        /// </param>
        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.Context.Dispose();
                }
            }

            this.disposed = true;
        }
    }
}