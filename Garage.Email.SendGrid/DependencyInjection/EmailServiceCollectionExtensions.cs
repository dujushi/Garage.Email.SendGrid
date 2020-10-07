using System;
using Garage.Email.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SendGrid.Extensions.DependencyInjection;

namespace Garage.Email.SendGrid.DependencyInjection
{
    /// <summary>
    /// Extension methods for <see cref="IServiceCollection" />.
    /// </summary>
    public static class EmailServiceCollectionExtensions
    {
        /// <summary>
        /// Adds email service dependencies.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/></param>
        /// <param name="apiKey">The api key for SendGrid</param>
        /// <param name="configureOptions">A delegate to configure <see cref="EmailOptions"/></param>
        /// <returns>The <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddSendGridEmailService(this IServiceCollection services, string apiKey, Action<EmailOptions> configureOptions)
        {
            services.AddSendGrid(options =>
            {
                options.ApiKey = apiKey;
            });

            services.AddOptions<EmailOptions>()
                .Configure(configureOptions)
                .PostConfigure(options =>
                {
                    if (string.IsNullOrWhiteSpace(options.FromEmail))
                    {
                        ThrowInvalidConfigurationException(nameof(options.FromEmail));
                    }

                    if (string.IsNullOrWhiteSpace(options.FromName))
                    {
                        ThrowInvalidConfigurationException(nameof(options.FromName));
                    }
                });
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<EmailOptions>>().Value);
            services.AddSingleton<IEmailService, SendGridEmailService>();
            return services;
        }

        private static void ThrowInvalidConfigurationException(string configurationName)
        {
            var exception = new EmailException("Invalid configuration");
            exception.Data["ConfigurationName"] = configurationName;
            throw exception;
        }
    }
}
