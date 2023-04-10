using Microsoft.CodeAnalysis.Host.Mef;
using MVC_Buisness_Logic_Task.Abstarctions.Repositories;
using MVC_Buisness_Logic_Task.DAL;
using MVC_Buisness_Logic_Task.Models;
using System.Collections.Generic;
using System.Linq;

namespace MVC_Buisness_Logic_Task.Abstarctions.Services
{
    public class VehicleService
    {
        IBaseRepository<Vehicle> _db;
        private readonly AppDbContext _appDb;

        public VehicleService(IBaseRepository<Vehicle> db, AppDbContext appDb)
        {
            _db = db;
            _appDb = appDb;
        }


        public void CreateVehicle(Vehicle item)
        {
            item.VIN = item.VIN.ToUpper();
            if (Search(item.VIN) != null)
            {
                return;
            }

            _db.Create(item);

        }


        public Vehicle Search(string VIN)
        {
            List<Vehicle> vehicles = _appDb.Vehicles.ToList();
            var res = vehicles.FirstOrDefault(x => x.VIN == VIN);
            return res;
        }
    }

    
}
