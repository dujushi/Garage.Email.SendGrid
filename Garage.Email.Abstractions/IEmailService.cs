using System.Threading;
using System.Threading.Tasks;

namespace Garage.Email.Abstractions
{
    /// <summary>
    /// Defines public methods provided by the email service.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Sends an email.
        /// </summary>
        /// <param name="toEmail">Email address of the receiver</param>
        /// <param name="toName">Name of the receiver</param>
        /// <param name="subject">The subject</param>
        /// <param name="plainTextContent">The plain text content</param>
        /// <param name="htmlContent">The HTML content</param>
        /// <param name="cancellationToken">An cancellation token</param>
        /// <returns>A task</returns>
        Task SendAsync(
            string toEmail,
            string toName,
            string subject,
            string plainTextContent,
            string htmlContent,
            CancellationToken cancellationToken = default);
    }
}
