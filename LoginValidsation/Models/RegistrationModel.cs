using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace LoginValidsation.Models
{
    public class RegistrationModel
    {
        public int id { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string UserType { get; set; }
        public long mobile { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        public DateTime? DOB { get; set; }
        public string Password { get; set; }
        
    }
}
