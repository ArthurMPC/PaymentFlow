using PaymentFlow.Domain.Interfaces.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Moq;
using System.Threading.Tasks;
using PaymentFlow.Domain.Entities;
using PaymentFlow.Application.Services;
using PaymentFlow.Domain.Interfaces.Services;

namespace PaymentFlow.Application.Tests.Service
{
    public class TransactionAppServiceTest
    {

        private Moq.Mock<ITransactionService> mockTransactionService;

        private ITransactionAppService GetService()
        {
            mockTransactionService = new Moq.Mock<ITransactionService>();

            mockTransactionService.Setup(x => x.Add(It.IsAny<Transaction>())).Returns(Task.FromResult(new Transaction{ }));
            mockTransactionService.Setup(x => x.Delete(It.IsAny<string>()));
            mockTransactionService.Setup(x => x.GetAll()).Returns(Task.FromResult(new List<Transaction>().AsEnumerable()));
            mockTransactionService.Setup(x => x.GetById(It.IsAny<string>())).Returns(Task.FromResult(new Transaction { }));
            mockTransactionService.Setup(x => x.GetMonthTransaction()).Returns(new List<Transaction>());
            mockTransactionService.Setup(x => x.GetDailyTransaction(It.IsAny<DateTime>())).Returns(Task.FromResult(new List<Transaction>()));

            Mock<IQueueService> mockQueue = new Mock<IQueueService>();

            mockQueue.Setup(x => x.QueueMessage(It.IsAny<string>(), It.IsAny<object>()));
            mockQueue.Setup(x => x.GetMessagesCount(It.IsAny<string>())).Returns(new int { });
            mockQueue.Setup(x => x.DequeueMessage<object>(It.IsAny<string>())).Returns(new object { });

            return new TransactionAppService(mockTransactionService.Object, mockQueue.Object);
        }

        [Fact]
        public void QueueDailyTransactionWithPositionValidation_test()
        {
            ITransactionAppService appService = GetService();
            appService.QueueDailyTransactionWithPositionValidation(new Transaction { });
        }

        [Fact]
        public void QueueDailyTransactionWithPositionValidationWithExceptionOfOverdraft_test()
        {
            ITransactionAppService appService = GetService();

            Assert.ThrowsAsync<Exception>( async delegate { await appService.QueueDailyTransactionWithPositionValidation(new Transaction { Value = 30000m }); });
        }

        [Fact]
        public void Add_test()
        {
            ITransactionAppService appService = GetService();
            appService.Add(new Transaction() { Id = "0" } );
        }

        [Fact]
        public void Delete_test()
        {
            ITransactionAppService appService = GetService();
            appService.Delete("0");
        }

        [Fact]
        public void GetById_test()
        {
            ITransactionAppService appService = GetService();
            appService.GetById("0");
        }

        [Fact]
        public void GetAll_test()
        {
            ITransactionAppService appService = GetService();
            appService.GetAll();
        }

    }
}
