using Microsoft.AspNetCore.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC_Buisness_Logic_Task.Models
{
    public class Manufacturer
    {
        public int Id { get; set; }
        [MinLength(3),MaxLength(50)]
        [Required(ErrorMessage = "Please enter Name!")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string Name { get; set; }
        public List<Model> Models { get; set; }
        public List<Vehicle> Vehicles { get; set; }
    }
}
