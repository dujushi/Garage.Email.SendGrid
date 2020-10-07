# Send emails with SendGrid
[SendGrid](https://sendgrid.com) is a popular email service provider. [The free plan](https://sendgrid.com/pricing) gives you 100 emails/day to play around with the service. [The SendGrid Nuget Package](https://github.com/sendgrid/sendgrid-csharp) makes C# integration easy and smooth. This article will show you how to integrate SendGrid with a Asp.NET Core Web API project.

### Dependency Injection
Install nuget pakcage `SendGrid.Extensions.DependencyInjection`.

You need an api key to register the service.
```
services.AddSendGrid(options =>
{
    options.ApiKey = apiKey;
});
```

### Send emails
You can now inject `ISendGridClient` to wherever you need to send emails.
```
var from = new EmailAddress(_emailOptions.FromEmail, _emailOptions.FromName);
var to = new EmailAddress(toEmail, toName);
var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
var response = await _sendGridClient.SendEmailAsync(msg, cancellationToken);
```

### Error handling
The possible errors are stated in [this document](https://sendgrid.com/docs/API_Reference/Web_API_v3/Mail/errors.html).

### Demo
https://github.com/dujushi/Garage.Email.SendGrid
