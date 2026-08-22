using System.ComponentModel.DataAnnotations;

namespace LoanApi.Models.Dto
{
    public class BlockUserDto
    {
        [Range(1, 365)]
        public int NumberOfDays { get; set; }
    }
}
