namespace RoadShowHardCode.Models
{
    /// <summary>
    /// The speech.
    /// </summary>
    public class Speech
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the city id.
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// Gets or sets the session id.
        /// </summary>
        public int SessionId { get; set; }

        /// <summary>
        /// Gets or sets the speaker id.
        /// </summary>
        public int SpeakerId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        public virtual City City { get; set; }

        /// <summary>
        /// Gets or sets the session.
        /// </summary>
        public virtual Session Session { get; set; }

        /// <summary>
        /// Gets or sets the speaker.
        /// </summary>
        public virtual Speaker Speaker { get; set; }
    }
}