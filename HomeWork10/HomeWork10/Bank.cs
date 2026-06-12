using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork10
{
    internal class Bank : IFinanceOperations
    {
        decimal IFinanceOperations.CalculateLoanPercent(int month, decimal AmountPerMonth)
        {
            
            decimal percent = 5;
            
            decimal percentTotal=AmountPerMonth* month * percent/100;
            return percentTotal;
        }

        bool IFinanceOperations.CheckUserHistory()
        {
            bool history = false;

            Random rnd = new Random();
            int num = rnd.Next(1, 101);
            if (num % 2 == 0)
            {
                history = true;
            }


            return history;
        }
    }
}
