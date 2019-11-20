using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentFlow.Domain.Entities;
using PaymentFlow.Domain.Interfaces;
using PaymentFlow.Domain.Interfaces.Application;
using PaymentFlow.Domain.Interfaces.Repositories;
using PaymentFlow.Domain.Interfaces.Services;

namespace PaymentFlow.Service.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ITransactionAppService _transactionAppService;
        private readonly ICashFlowService _cashFlowService;

        public PaymentController(IQueueService queueService, ITransactionAppService transactionAppService, ICashFlowService cashFlowService)
        {
            _transactionAppService = transactionAppService;
            _cashFlowService = cashFlowService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody]Transaction request)
        {
            try
            {
                request.TransactionDate = DateTime.Now;
                await _transactionAppService.QueueDailyTransactionWithPositionValidation(request);

                return Created("", request);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<CashFlow>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Get()
        {
            try
            {
                List<CashFlow> cashFLow = _cashFlowService.GetCashFow(_transactionAppService.GetMonthTransaction());

                return Ok(cashFLow);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

    }
}