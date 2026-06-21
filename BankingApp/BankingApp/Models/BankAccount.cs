using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApp.Models
{
    public class Balance
    {
        public double Gel { get; set; }
        public double Usd { get; set; }
        public double Eur { get; set; }
    }

    public class BankAccount
    {

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public Balance Balance { get; set; } = new Balance();
        public CardDetails CardDetails { get; set; } = new CardDetails();
        public string PinCode { get; set; } = string.Empty;
        public List<Transaction> TransactionHistory { get; set; } = new List<Transaction>();

    }
}

