using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentFlow.Domain.IoC;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentFlow.Consolidator.Agent
{
    public class IoC
    {
        public IServiceProvider ServicesProvider;

        public IoC(IConfiguration configuration)
        {
            IServiceCollection services = new ServiceCollection();

            services.RegisterServices(configuration);

            ServicesProvider = services.BuildServiceProvider();
        }
    }
}
