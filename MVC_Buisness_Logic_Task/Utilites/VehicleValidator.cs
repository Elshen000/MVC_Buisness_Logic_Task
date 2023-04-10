using MVC_Buisness_Logic_Task.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Buisness_Logic_Task.Utilites
{
    public static class VehicleValidator
    {
        public static string CheckVehicle(this Vehicle vehicle)
        {
            vehicle.VIN = vehicle.VIN.ToUpper().Trim();

            if (vehicle.ModelYear > int.Parse(DateTime.Now.AddYears(1).ToString("yyyy")))
            {
                return "ModelYear";
            }

            if (DateTime.Compare(DateTime.Now, vehicle.PurchaseDate) < 0)
            {
                return "PurchaseDate";
            }


            return "Success";
        }

        public static Vehicle Search(string VIN, IEnumerable<Vehicle> vehicles)
        {
            var res = vehicles.FirstOrDefault(x => x.VIN == VIN);
            return res;
        }
    }
}
