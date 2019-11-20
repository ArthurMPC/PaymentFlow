using Moq;
using PaymentFlow.Domain.Interfaces.Services;
using PaymentFlow.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PaymentFlow.Domain.Tests.Service
{
    public class QueueServiceTest
    {

        private Mock<IQueueService> mockQueue;

        private IQueueService GetService()
        {
            mockQueue = new Mock<IQueueService>();

            mockQueue.Setup(x => x.QueueMessage(It.IsAny<string>(), It.IsAny<object>()));
            mockQueue.Setup(x => x.GetMessagesCount(It.IsAny<string>())).Returns(new int { });
            mockQueue.Setup(x => x.DequeueMessage<object>(It.IsAny<string>())).Returns(new object { });

            return mockQueue.Object;
        }

        [Fact]
        public void QueueMessage_Test()
        {
            IQueueService queueService = GetService();

            queueService.QueueMessage("teste", new object());
        }

        [Fact]
        public void DequeMessage_Test()
        {
            IQueueService queueService = GetService();

            queueService.DequeueMessage<object>("teste");
        }

        [Fact]
        public void GetMessageCount()
        {
            IQueueService queueService = GetService();

            queueService.GetMessagesCount("teste");
        }

    }
}
