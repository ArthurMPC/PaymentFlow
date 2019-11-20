using Moq;
using PaymentFlow.Domain.Entities;
using PaymentFlow.Domain.Interfaces.Application;
using PaymentFlow.Domain.Interfaces.Services;
using PaymentFlow.Service.API.Controllers;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PaymentFlow.Service.API.Tests.Controllers
{
    public class PaymentControllerTest
    {

        private PaymentController GetController()
        {

            Mock<IQueueService> mockQueue = new Mock<IQueueService>();
            
            mockQueue.Setup(x => x.QueueMessage(It.IsAny<string>(), It.IsAny<object>()));
            mockQueue.Setup(x => x.GetMessagesCount(It.IsAny<string>())).Returns(new int { });
            mockQueue.Setup(x => x.DequeueMessage<object>(It.IsAny<string>())).Returns(new object { });

            Mock<ITransactionAppService> mockTransactionService = new Mock<ITransactionAppService>();

            mockTransactionService.Setup(x => x.Add(It.IsAny<Transaction>())).Returns(Task.FromResult(new Transaction { }));
            mockTransactionService.Setup(x => x.Delete(It.IsAny<string>()));
            mockTransactionService.Setup(x => x.GetAll()).Returns(Task.FromResult(new List<Transaction>().AsEnumerable()));
            mockTransactionService.Setup(x => x.GetById(It.IsAny<string>())).Returns(Task.FromResult(new Transaction { }));
            mockTransactionService.Setup(x => x.GetMonthTransaction()).Returns(new List<Transaction>());

            Mock<ICashFlowService> mockCash = new Mock<ICashFlowService>();

            mockCash.Setup(x => x.GenerateBalance(It.IsAny<decimal>(), It.IsAny<decimal>(), It.IsAny<decimal>())).Returns(new decimal());
            mockCash.Setup(x => x.GetCashFow(It.IsAny<List<Transaction>>())).Returns(new List<CashFlow>());

            return new PaymentController(mockQueue.Object, mockTransactionService.Object, mockCash.Object);
        }

        [Fact]
        public async void Post_test()
        {
            PaymentController controller = GetController();

            await controller.Post(new Transaction());

        }

        [Fact]
        public void Get_test()
        {
            PaymentController controller = GetController();

            controller.Get();

        }

    }
}
