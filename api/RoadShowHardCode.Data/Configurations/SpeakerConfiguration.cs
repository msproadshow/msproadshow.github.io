namespace RoadShowHardCode.Data.Configurations
{
    using System.Data.Entity.ModelConfiguration;

    using RoadShowHardCode.Models;

    /// <summary>
    /// The speaker configuration.
    /// </summary>
    public class SpeakerConfiguration : EntityTypeConfiguration<Speaker>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpeakerConfiguration"/> class.
        /// </summary>
        public SpeakerConfiguration()
        {
            this.Property(e => e.Name).IsRequired().HasMaxLength(1000);
            this.Property(e => e.About);
            this.Property(e => e.Twitter).HasMaxLength(100);
            this.Property(e => e.Facebook).HasMaxLength(100);
            this.Property(e => e.LinkedIn).HasMaxLength(100);
            this.Property(e => e.Photo).HasMaxLength(1000);
        }
    }
}