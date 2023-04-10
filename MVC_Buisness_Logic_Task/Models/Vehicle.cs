using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace MVC_Buisness_Logic_Task.Models
{
    public class Vehicle
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter VIN!"),MinLength(17), MaxLength(17)]
        [RegularExpression("[A-HJ-NPR-Z0-9]{13}[0-9]{4}", ErrorMessage = "Invalid Vehicle Identification Number Format.")]
        public string VIN { get; set; }
        [EmailAddress,Required]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "Please enter ModelYear!"), Range(1900,2050)]  
        public int ModelYear { get; set;}
        [Required(ErrorMessage = "Please enter Colour!"), MinLength(3),MaxLength(50)]  
        public string Colour { get; set;}
        [DataType(DataType.Date)]   
        [Required(ErrorMessage = "Please enter PurchaseDate!")]  
        public DateTime PurchaseDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? SaleDate { get; set; }
       
        public Model Model { get; set; }
        public int ModelId { get; set; }
       
        public Manufacturer Manufacturer { get; set; }
        public int ManufacturerId { get; set; }

    }
}
