using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Garage.Email.Abstractions;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Garage.Email.SendGrid
{
    /// <summary>
    /// Represents an implementation of <see cref="IEmailService" /> using SendGrid.
    /// </summary>
    public class SendGridEmailService : IEmailService
    {
        private readonly ISendGridClient _sendGridClient;
        private readonly EmailOptions _emailOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="SendGridEmailService"/> class.
        /// </summary>
        /// <param name="sendGridClient">The SendGrid client</param>
        /// <param name="emailOptions">The email service configuration</param>
        public SendGridEmailService(ISendGridClient sendGridClient, EmailOptions emailOptions)
        {
            _sendGridClient = sendGridClient ?? throw new ArgumentNullException(nameof(sendGridClient));
            _emailOptions = emailOptions ?? throw new ArgumentNullException(nameof(emailOptions));
        }

        /// <summary>
        /// Sends the email using SendGrid.
        /// </summary>
        /// <param name="toEmail">The email address of the receiver</param>
        /// <param name="toName">The name of the receiver</param>
        /// <param name="subject">The email subject</param>
        /// <param name="plainTextContent">The plain text email content</param>
        /// <param name="htmlContent">The HTML email content</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>A task</returns>
        /// <exception cref="EmailException">The exception is thrown when the SendGrid api fails.</exception>
        public async Task SendAsync(
            string toEmail,
            string toName,
            string subject,
            string plainTextContent,
            string htmlContent,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(toEmail))
            {
                throw new ArgumentNullException(nameof(toEmail));
            }

            if (string.IsNullOrEmpty(toName))
            {
                throw new ArgumentNullException(nameof(toName));
            }

            if (string.IsNullOrEmpty(subject))
            {
                throw new ArgumentNullException(nameof(subject));
            }

            if (string.IsNullOrEmpty(plainTextContent))
            {
                throw new ArgumentNullException(nameof(plainTextContent));
            }

            if (string.IsNullOrEmpty(htmlContent))
            {
                throw new ArgumentNullException(nameof(htmlContent));
            }

            var from = new EmailAddress(_emailOptions.FromEmail, _emailOptions.FromName);
            var to = new EmailAddress(toEmail, toName);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await _sendGridClient.SendEmailAsync(msg, cancellationToken);
            if (response.StatusCode != HttpStatusCode.Accepted && response.StatusCode != HttpStatusCode.OK)
            {
                var responseBody = await response.Body.ReadAsStringAsync();
                var exception = new EmailException("Fail to send email");
                exception.Data[nameof(response.StatusCode)] = response.StatusCode;
                exception.Data[nameof(response.Body)] = responseBody;
                throw exception;
            }
        }
    }
}
