using System;
using System.Collections.Generic;
using System.Text;

namespace BankingApp.Models
{
    public class CardDetails
    {
        public string CardNumber { get; set; } = string.Empty;
        public string ExpirationDate { get; set; } = string.Empty;
        public string Cvc { get; set; } = string.Empty;
    }
}
