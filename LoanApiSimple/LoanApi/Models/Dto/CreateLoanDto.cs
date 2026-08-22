using System.ComponentModel.DataAnnotations;

namespace LoanApi.Models.Dto
{
    public class CreateLoanDto
    {
        public LoanType LoanType { get; set; }
        [Range(1, 1000000)] public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        [Range(1, 360)] public int PeriodInMonths { get; set; }
    }
}
