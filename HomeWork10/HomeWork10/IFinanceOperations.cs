using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork10
{
    internal interface IFinanceOperations
    {
        internal decimal CalculateLoanPercent(int month, decimal AmountPerMonth);
        internal bool CheckUserHistory();
    }
}
