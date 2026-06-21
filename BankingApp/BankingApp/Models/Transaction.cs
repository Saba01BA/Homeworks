using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApp.Models
{
    public class Transaction
    {
        public string Type { get; set; } = string.Empty;
        public double AmountGEL { get; set; }
        public double AmountUSD { get; set; }
        public double AmountEUR { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
