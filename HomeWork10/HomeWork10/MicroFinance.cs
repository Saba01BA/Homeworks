using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork10
{
    internal class MicroFinance : IFinanceOperations
    {
        decimal IFinanceOperations.CalculateLoanPercent(int month, decimal AmountPerMonth)
        {
            decimal commision = AmountPerMonth * month * 10 / 100; //ჯამური გადასახდელი თანხის 10 პროცენტი


            decimal TotalPay = month * AmountPerMonth + commision + month * 4; 




            return TotalPay;
        }

        bool IFinanceOperations.CheckUserHistory()
        {
            return true;
        }
    }
}
