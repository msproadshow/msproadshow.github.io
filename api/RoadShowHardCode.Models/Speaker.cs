namespace RoadShowHardCode.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// The speaker.
    /// </summary>
    public class Speaker
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the photo.
        /// </summary>
        public string Photo { get; set; }

        /// <summary>
        /// Gets or sets the about.
        /// </summary>
        public string About { get; set; }

        /// <summary>
        /// Gets or sets the facebook.
        /// </summary>
        public string Facebook { get; set; }

        /// <summary>
        /// Gets or sets the twitter.
        /// </summary>
        public string Twitter { get; set; }

        /// <summary>
        /// Gets or sets the linked in.
        /// </summary>
        public string LinkedIn { get; set; }

        /// <summary>
        /// Gets or sets the speeches.
        /// </summary>
        public virtual ICollection<Speech> Speeches { get; set; }
    }
}