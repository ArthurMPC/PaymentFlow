using Microsoft.Extensions.Configuration;
using PaymentFlow.Domain.Entities;
using PaymentFlow.Domain.Interfaces.Repositories;
using PaymentFlow.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentFlow.Consolidator.Agent.Task
{
    public class ConsolidationTask
    {

        readonly IQueueService _queueService;
        readonly IRepositoryTransaction _repositoryTransaction;
        readonly IConfiguration _configuration;
        readonly string queueName;

        public ConsolidationTask(IQueueService queueService, IRepositoryTransaction repositoryTransaction, IConfiguration configuration)
        {
            _queueService = queueService;
            _repositoryTransaction = repositoryTransaction;
            _configuration = configuration;

            queueName = _configuration.GetSection("RabbitMQ").GetSection("QueueName").Value;
        }

        public void DoTask()
        {
            while (true)
            {

                int messagesInQueue = _queueService.GetMessagesCount(queueName);

                if (messagesInQueue > 0)
                {
                    for (int messageIndex = 0; messageIndex < messagesInQueue; messageIndex++)
                    {
                        Transaction transaction = _queueService.DequeueMessage<Transaction>(queueName);

                        _repositoryTransaction.Add(transaction);
                    }
                }
                else
                    System.Threading.Thread.Sleep(10000);
                
            }

        }

    }
}
