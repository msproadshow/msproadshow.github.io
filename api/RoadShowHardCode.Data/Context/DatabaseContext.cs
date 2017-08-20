namespace RoadShowHardCode.Data.Context
{
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;

    using RoadShowHardCode.Models;

    /// <summary>
    /// The database context.
    /// </summary>
    public class DatabaseContext : IdentityDbContext<User>, IDbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseContext"/> class.
        /// </summary>
        public DatabaseContext() : base("Database")
        {
        }

        /// <summary>
        /// Gets or sets the cities.
        /// </summary>
        public virtual IDbSet<City> Cities { get; set; }

        /// <summary>
        /// Gets or sets the sessions.
        /// </summary>
        public virtual IDbSet<Session> Sessions { get; set; }

        /// <summary>
        /// Gets or sets the speakers.
        /// </summary>
        public virtual IDbSet<Speaker> Speakers { get; set; }

        /// <summary>
        /// Gets or sets the speeches.
        /// </summary>
        public virtual IDbSet<Speech> Speeches { get; set; }

        /// <summary>
        /// The begin transaction.
        /// </summary>
        /// <returns>
        /// The <see cref="IDbContextTransaction"/>.
        /// </returns>
        public IDbContextTransaction BeginTransaction() => new Transaction(this.Database.BeginTransaction());

        /// <summary>
        /// The set.
        /// </summary>
        /// <typeparam name="T">
        /// Any reference type
        /// </typeparam>
        /// <returns>
        /// The <see cref="IDbSet{T}"/>.
        /// </returns>
        public new IDbSet<T> Set<T>() where T : class => base.Set<T>();

        /// <summary>
        /// The on model creating.
        /// </summary>
        /// <param name="modelBuilder">
        /// The model builder.
        /// </param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(typeof(DatabaseContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
