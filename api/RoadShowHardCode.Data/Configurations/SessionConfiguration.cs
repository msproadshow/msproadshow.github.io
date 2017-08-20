namespace RoadShowHardCode.Data.Configurations
{
    using System.Data.Entity.ModelConfiguration;

    using RoadShowHardCode.Models;

    /// <summary>
    /// The session configuration.
    /// </summary>
    public class SessionConfiguration : EntityTypeConfiguration<Session>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SessionConfiguration"/> class.
        /// </summary>
        public SessionConfiguration()
        {
            this.Property(e => e.Name).IsRequired().HasMaxLength(100);
        }
    }
}