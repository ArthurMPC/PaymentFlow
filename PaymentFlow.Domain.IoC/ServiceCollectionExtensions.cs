using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using PaymentFlow.Application.Services;
using PaymentFlow.Domain.Interfaces.Application;
using PaymentFlow.Domain.Interfaces.Repositories;
using PaymentFlow.Domain.Interfaces.Services;
using PaymentFlow.Domain.Service;
using PaymentFlow.Domain.Services;
//using PaymentFlow.Domain.Service;
using PaymentFlow.Infra.Data.Context;
using PaymentFlow.Infra.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentFlow.Domain.IoC
{
    public static class ServiceCollectionExtensions
    {

        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);

            services.AddContexts(configuration);
            services.AddRepositories();
            services.AddDomainServices();
        }


        public static void AddContexts(this IServiceCollection services, IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("MongoPaymentFlow"));
            var database = client.GetDatabase("PaymentFlow");

            services.AddSingleton(database);
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IRepositoryTransaction, RepositoryTransaction>();
        }

        public static void AddDomainServices(this IServiceCollection services)
        {
            services.AddTransient<IQueueService, QueueService>();
            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<ITransactionAppService, TransactionAppService>();
            services.AddTransient<ICashFlowService, CashFlowService>();
        }

    }
}
