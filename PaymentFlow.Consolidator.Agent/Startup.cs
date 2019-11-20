using Microsoft.Extensions.DependencyInjection;
using PaymentFlow.Domain.IoC;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentFlow.Consolidator.Agent
{
    public class Startup : BaseStartup
    {
        public Startup()
        {
            string environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            Setup(environmentName);

            IoC ioc = new IoC(ConfigurationRoot);
            ServiceProvider = ioc.ServicesProvider;
        }
        
    }
}
