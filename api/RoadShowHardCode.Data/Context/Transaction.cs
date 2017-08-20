namespace RoadShowHardCode.Data.Context
{
    using System.Data.Entity;

    /// <summary>
    /// The transaction.
    /// </summary>
    public class Transaction : IDbContextTransaction
    {
        /// <summary>
        /// The transaction.
        /// </summary>
        private readonly DbContextTransaction transaction;

        /// <summary>
        /// Initializes a new instance of the <see cref="Transaction"/> class.
        /// </summary>
        /// <param name="transaction">
        /// The transaction.
        /// </param>
        public Transaction(DbContextTransaction transaction)
        {
            this.transaction = transaction;
        }

        /// <summary>
        /// The commit.
        /// </summary>
        public void Commit()
        {
            this.transaction.Commit();
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        public void Dispose()
        {
            this.transaction.Dispose();
        }

        /// <summary>
        /// The rollback.
        /// </summary>
        public void Rollback()
        {
            this.transaction.Rollback();
        }
    }
}