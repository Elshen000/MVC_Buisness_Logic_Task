using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MVC_Buisness_Logic_Task.DAL;
using MVC_Buisness_Logic_Task.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Buisness_Logic_Task.Controllers
{
    public class HomeController : Controller
    {
        #region DbContext
        private readonly AppDbContext _db;
        public HomeController(AppDbContext db)
        {
            _db = db;
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index()
        {
            List<Vehicle> vehicles = await _db.Vehicles.Include(v => v.Manufacturer).Include(v => v.Model).ToListAsync();
            return View(vehicles);
        }
        #endregion



    }
}
