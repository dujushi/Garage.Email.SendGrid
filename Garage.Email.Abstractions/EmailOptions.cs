namespace Garage.Email.Abstractions
{
    /// <summary>
    /// Represents settings for the email service.
    /// </summary>
    public class EmailOptions
    {
        /// <summary>
        /// Gets or sets the email address of the sender.
        /// </summary>
        public string FromEmail { get; set; }

        /// <summary>
        /// Gets or sets the name of the sender.
        /// </summary>
        public string FromName { get; set; }
    }
}
