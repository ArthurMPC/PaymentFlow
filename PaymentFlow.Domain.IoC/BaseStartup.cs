using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace PaymentFlow.Domain.IoC
{
    public class BaseStartup
    {

        public IConfigurationRoot ConfigurationRoot;
        public IServiceProvider ServiceProvider;

        public void Setup(string environmentName)
        {
            ConfigurationRoot = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        }

    }
}
