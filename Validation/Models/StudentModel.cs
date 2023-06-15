using System.ComponentModel.DataAnnotations;

namespace Validation.Models
{
    public class StudentModel
    {
        public int RollNumber { get; set; }

        [Required(ErrorMessage = "Please enter name")]
        //[MinLength(2)]
        //[MaxLength(50)]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name should be 2 to 50 characters long")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Please enter mobile number")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Please enter email")]
        // [RegularExpression("^([a-zA-Z0-9_\\-\\.]+)@([a-zA-Z0-9_\\-\\.]+)\\.([a-zA-Z]{2,5})$", ErrorMessage = "Please enter valid email")]
        [EmailAddress(ErrorMessage = "Please enter valid email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter age")]
        [Range(1, 150, ErrorMessage = "Please enter valid age")]
        public int Age { get; set; }
        //Akash Change
    }
}
