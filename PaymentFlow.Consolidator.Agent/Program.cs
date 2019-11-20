using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentFlow.Consolidator.Agent.Task;
using PaymentFlow.Domain.Interfaces.Repositories;
using PaymentFlow.Domain.Interfaces.Services;
using PaymentFlow.Infra.Data.Context;

namespace PaymentFlow.Consolidator.Agent
{
    class Program
    {
        static void Main(string[] args)
        {
            Startup startup = new Startup();

            var service = startup.ServiceProvider;
            
            ConsolidationTask task = new ConsolidationTask(service.GetService<IQueueService>(), service.GetService<IRepositoryTransaction>(), service.GetService<IConfiguration>());

            task.DoTask();

        }
    }
}
