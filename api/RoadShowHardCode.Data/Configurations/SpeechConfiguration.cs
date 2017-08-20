namespace RoadShowHardCode.Data.Configurations
{
    using System.Data.Entity.ModelConfiguration;

    using RoadShowHardCode.Models;

    /// <summary>
    /// The speech configuration.
    /// </summary>
    public class SpeechConfiguration : EntityTypeConfiguration<Speech>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpeechConfiguration"/> class.
        /// </summary>
        public SpeechConfiguration()
        {
            this.Property(e => e.Name).IsRequired().HasMaxLength(1000);
            this.Property(e => e.Description).IsRequired();
        }
    }
}