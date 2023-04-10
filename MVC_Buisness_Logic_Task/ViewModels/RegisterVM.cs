using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Buisness_Logic_Task.ViewModels
{
    public class RegisterVM
    {
        [Required,MinLength(3),MaxLength(50)]
        public string Name { get; set; }
        [Required,MinLength(3), MaxLength(50)]  
        public string Surname { get; set; }

        [Required]
        public string Email { get; set; }
        [Required, MinLength(5), MaxLength(20) ]
        public string Username { get; set; }

        [Required,DataType(DataType.Password)]

        public string Password { get; set; }

        [Required, DataType(DataType.Password),Compare(nameof(Password))]

        public string CheckPassword { get; set; }
    }
}
