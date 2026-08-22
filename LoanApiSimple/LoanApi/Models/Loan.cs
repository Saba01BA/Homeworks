using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LoanApi.Models
{
    public class Loan
    {
        public int Id { get; set; }
        public LoanType LoanType { get; set; }

        [Range(1, 1000000)]
        public decimal Amount { get; set; }

        public Currency Currency { get; set; }

        [Range(1, 360)]
        public int PeriodInMonths { get; set; }

        public LoanStatus Status { get; set; } = LoanStatus.Pending;
        public int UserId { get; set; }
        [JsonIgnore]
        public User? User { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum LoanType { FastLoan, AutoLoan, InstallmentPlan }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Currency { GEL, USD, EUR }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum LoanStatus { Pending, Approved, Rejected }
}
