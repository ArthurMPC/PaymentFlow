using PaymentFlow.Domain.Entities;
using PaymentFlow.Domain.Interfaces.Repositories;
using PaymentFlow.Domain.Interfaces.Services;
using PaymentFlow.Infra.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaymentFlow.Application.Services
{
    public class CashFlowService : ICashFlowService
    {

        public decimal GenerateBalance(decimal totalEntries, decimal totalExits, decimal totalChages)
        {
            return (totalEntries - totalExits - totalChages);
        }

        public List<CashFlow> GetCashFow(List<Transaction> transactionList)
        {
            List<CashFlow> cashFlowList = new List<CashFlow>();

            if (null != transactionList && transactionList.Count > 0)
            {             
                DateTime date = DateTime.Now.Date;
                DateTime endDate = DateTime.Now.AddDays(30).Date;

                decimal lastDayPosition = 0m;

                while (date <= endDate)
                {
                    CashFlow cashFlow = new CashFlow();
                    cashFlow.Entries = new List<Entrie>();
                    cashFlow.Exits = new List<Exit>();
                    cashFlow.Charges = new List<Charge>();

                    List<Transaction> dailyLaunchs = transactionList.Where(x => x.LaunchDate.Date == date.Date).ToList();

                    if (dailyLaunchs.Count == 0)
                    {
                        date = date.AddDays(1);
                        continue;
                    }
                        
                    for (int j = 0; j < dailyLaunchs.Count; j++)
                    {
                        if (dailyLaunchs[j].Kind == Domain.Enums.TransactionKind.Exit)
                            cashFlow.Exits.Add(new Exit { Date = dailyLaunchs[j].TransactionDate, Value = dailyLaunchs[j].Value });

                        if (dailyLaunchs[j].Kind == Domain.Enums.TransactionKind.Entrie)
                            cashFlow.Entries.Add(new Entrie { Date = dailyLaunchs[j].TransactionDate, Value = dailyLaunchs[j].Value });

                        cashFlow.Charges.Add(new Charge { Date = dailyLaunchs[j].TransactionDate, Value = dailyLaunchs[j].Charges });
                    }

                    cashFlow.Total = GenerateBalance(
                        dailyLaunchs.Where(x => x.Kind == Domain.Enums.TransactionKind.Entrie).Sum(x => x.Value),
                        dailyLaunchs.Where(x => x.Kind == Domain.Enums.TransactionKind.Exit).Sum(x => x.Value),
                        dailyLaunchs.Sum(x => x.Charges));

                    cashFlow.Total += ApplyInterest(lastDayPosition, cashFlow.Total);

                    if (lastDayPosition != 0m)
                    {
                        cashFlow.PositionOfTheDay = ((cashFlow.Total * 100) / (lastDayPosition == 0m ? 1 : lastDayPosition)) - 100;

                        if (cashFlow.Total > lastDayPosition && cashFlow.PositionOfTheDay < 0)
                            cashFlow.PositionOfTheDay = cashFlow.PositionOfTheDay * -1m;
                    }
                    else
                        cashFlow.PositionOfTheDay = 100;

                    cashFlow.Date = date;
                    lastDayPosition = cashFlow.Total;

                    date = date.AddDays(1);
                    cashFlowList.Add(cashFlow);
                }
            }

            return cashFlowList;
        }

        public decimal ApplyInterest(decimal lastDayValue, decimal currentDayValue)
        {
            decimal interest = 0m;

           if((lastDayValue + currentDayValue) < 0m || currentDayValue < 0m)
                interest = -currentDayValue*0.0083m;

            return interest;
        }

    }
}
