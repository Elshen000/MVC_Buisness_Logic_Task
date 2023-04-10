using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC_Buisness_Logic_Task.Models
{
    public class Model
    {
        public int Id { get; set; }
        [Required,MaxLength(50)]
        public string Name { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public int ManufacturerId { get; set; }
        public List<Vehicle> Vehicles { get; set; }
    }
}
