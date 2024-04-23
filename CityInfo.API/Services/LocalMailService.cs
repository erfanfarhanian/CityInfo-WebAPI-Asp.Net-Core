namespace CityInfo.API.Services
{
    public class LocalMailService : IMailService
    {
        // Just for test. This is not the real code for mail service!
        private readonly string _mailTo = string.Empty;
        private readonly string _mailFrom = string.Empty;

        public LocalMailService(IConfiguration configuration)
        {
            _mailTo = configuration["mailSetting:mailToAddress"];
            _mailFrom = configuration["mailSetting:mailFromAddress"];
        }

        // Just as a test. It is not real
        public void Send(string subject, string message)
        {
            Console.WriteLine($"Mail From {_mailFrom} To {_mailTo}, "
                + $"With {nameof(LocalMailService)}");
            Console.WriteLine($"Subject {subject}");
            Console.WriteLine($"Message {message}");
        }

    }
}
