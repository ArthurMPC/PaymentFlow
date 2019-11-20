using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentFlow.Domain.Interfaces.Services
{
    public interface IQueueService
    {

        void QueueMessage(string queueName, object messageContent);

        T DequeueMessage<T>(string queueName);

        int GetMessagesCount(string queueName);
    }
}
