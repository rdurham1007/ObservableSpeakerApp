using Microsoft.Extensions.Configuration;

namespace CommonComponents.MassTransit
{
    public class MassTransitOptions
    {
        public MassTransitOptions(IConfiguration configuration)
        {
            var section = configuration.GetSection("MassTransit");
            
            Host = section.GetValue<string>("Host") ?? throw new ArgumentNullException(nameof(Host));
            VirtualHost = section.GetValue<string>("VirtualHost") ?? "/";
            Username = section.GetValue<string>("Username") ?? throw new ArgumentNullException(nameof(Username));
            Password = section.GetValue<string>("Password") ?? throw new ArgumentNullException(nameof(Password));
        }

        public string Host { get; }
        public string VirtualHost { get; }
        public string Username { get; }
        public string Password { get; }
    }
}