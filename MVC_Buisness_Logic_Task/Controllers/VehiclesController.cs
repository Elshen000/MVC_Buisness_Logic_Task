using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Buisness_Logic_Task.Abstarctions.Repositories;
using MVC_Buisness_Logic_Task.DAL;
using MVC_Buisness_Logic_Task.Models;
using MVC_Buisness_Logic_Task.Utilites;

namespace MVC_Buisness_Logic_Task.Controllers
{
    public class VehiclesController : Controller
    {
        #region DbContext
        private readonly AppDbContext _context;
        IBaseRepository<Vehicle> _db;
        public VehiclesController(AppDbContext context,IBaseRepository<Vehicle>db)
        {
            _context = context;
            _db = db;
        }
        #endregion

        #region Index
        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
            List<Vehicle> vehicles = await _context.Vehicles.Include(v => v.Manufacturer).Include(v => v.Model).ToListAsync();
            return View(vehicles);
        }
        #endregion

        #region Detail
        // GET: Vehicles/Details/id
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles
                .Include(v => v.Manufacturer)
                .Include(v => v.Model)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }
        #endregion

        #region Create
        // GET: Vehicles/Create
        public IActionResult Create()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return NotFound();
            }
            if (_context.Manufacturers.Count()==0)
            {
                return RedirectToAction("Create","Manufacturers");
            }
            if (_context.Models.Count() == 0)
            {
                return RedirectToAction("Create", "Models");
            }
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name");
            ViewData["ModelId"] = new SelectList(_context.Models, "Id", "Name");
            return View();
        }

        // POST: Vehicles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VIN,UserEmail,ModelYear,Colour,PurchaseDate,SaleDate,ModelId,ManufacturerId")] Vehicle vehicle)
        {
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name", vehicle.ManufacturerId);
            ViewData["ModelId"] = new SelectList(_context.Models, "Id", "Name", vehicle.ModelId);
            if (!ModelState.IsValid)
            {
                return View(vehicle);
            }
            var res = vehicle.CheckVehicle();
            if (VehicleValidator.Search(vehicle.VIN, _db.GetAll()) != null)
            {
                ModelState.AddModelError("VIN", "VIN must be unique");
                return View(vehicle);
            }

            if (res == "ModelYear")
            {
                ModelState.AddModelError("ModelYear", "Less or equal to than the current year plus one.");
                return View(vehicle);
            }
            else if (res == "PurchaseDate")
            {
                ModelState.AddModelError("PurchaseDate", "On or before the current date/time.");
                return View(vehicle);
            }
            else
            {
                _db.Create(vehicle);
                return RedirectToAction(nameof(Index));
            }
           
            
        }
        #endregion

        #region Update
        // GET: Vehicles/Edit/id
        public async Task<IActionResult> Edit(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return NotFound();
            }
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name", vehicle.ManufacturerId);
            ViewData["ModelId"] = new SelectList(_context.Models, "Id", "Name", vehicle.ModelId);
            return View(vehicle);
        }

        // POST: Vehicles/Edit/id
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VIN,UserEmail,ModelYear,Colour,PurchaseDate,SaleDate,ModelId,ManufacturerId")] Vehicle vehicle)
        {
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "Id", "Name", vehicle.ManufacturerId);
            ViewData["ModelId"] = new SelectList(_context.Models, "Id", "Name", vehicle.ModelId);
            if (id != vehicle.Id)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(vehicle);
            }
            var res = vehicle.CheckVehicle();
            if (res == "ModelYear")
            {
                ModelState.AddModelError("ModelYear", "Less or equal to than the current year plus one.");
                return View(vehicle);
            }
            else if (res == "PurchaseDate")
            {
                ModelState.AddModelError("PurchaseDate", "On or before the current date/time.");
                return View(vehicle);
            }
            else
            {
                try
                {
                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
           
            
        }
        #endregion

        #region Delete
        // GET: Vehicles/Delete/id
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Vehicle vehicle = await _context.Vehicles
                .Include(v => v.Manufacturer)
                .Include(v => v.Model)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region VehicleExists
        private bool VehicleExists(int id)
        {
            return _context.Vehicles.Any(e => e.Id == id);
        }
        #endregion

        #region LoadModels
        public async Task<IActionResult> LoadModels(int manId)
        {
            List<Model> models = await _context.Models.Where(x => x.ManufacturerId == manId).Include(x => x.Manufacturer).Include(x => x.Vehicles).ToListAsync();
            return PartialView("_LoadModelsPartial", models);
        }
        #endregion





    }
}
