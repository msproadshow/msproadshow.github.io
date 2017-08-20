namespace RoadShowHardCode.Data.Configurations
{
    using System.Data.Entity.ModelConfiguration;

    using RoadShowHardCode.Models;

    /// <summary>
    /// The city configuration.
    /// </summary>
    public class CityConfiguration : EntityTypeConfiguration<City>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CityConfiguration"/> class.
        /// </summary>
        public CityConfiguration()
        {
            this.Property(e => e.Name).IsRequired().HasMaxLength(100);
        }
    }
}