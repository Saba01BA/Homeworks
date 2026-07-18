using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Adress
    {
        [Required(ErrorMessage ="The City field can not be Empty")]
        public string City { get; set; } = string.Empty;
        [Required(ErrorMessage = "The Country field can not be Empty")]

        public string Country { get; set; } = string.Empty;
        [Required(ErrorMessage = "The Home Number field can not be Empty")]

        public string HomeNumber { get; set; } = string.Empty;
    }
}
