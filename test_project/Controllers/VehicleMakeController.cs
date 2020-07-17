using System;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_service.Models;
using Project_service.Service;
using AutoMapper;
using test_project.Models.ViewModels;
using Project_service.PagingFIlteringSorting;
namespace test_project.Controllers
{
    public class VehicleMakeController : Controller
    {
        
        private readonly IVehicleMakeService _vehicleMakeService;
        private readonly IMapper _mapper;
        public VehicleMakeController(IVehicleMakeService vehicleMakeService, IMapper mapper )
        {
            _vehicleMakeService = vehicleMakeService;
            _mapper = mapper;
    }

        // GET: VehicleMake
        public async Task<IActionResult> Index(Sort sorting, FIlter filtering, int? page)
        {


            ViewBag.CurrentSort = sorting.SortOrder;
            ViewBag.sortByName = string.IsNullOrEmpty(sorting.SortOrder) ? "name_desc" : "";
            ViewBag.sortByAbrv = sorting.SortOrder == "abrv_desc" ? "abrv_asc" : "abrv_desc";
            ViewBag.CurrentFilter = filtering.SearchString;

           //PaginatedList of VehicleMakes
            var listOfvehicleMakes = await _vehicleMakeService.GetVehicleMakes(sorting, filtering,page);


            IPaginatedList<VehicleMakeVM> vehicleMakes = new PaginatedList<VehicleMakeVM>(
                _mapper.Map<List<VehicleMakeVM>>(listOfvehicleMakes), //Items
                                     listOfvehicleMakes.Count,                             //Count
                                     listOfvehicleMakes.PageIndex ?? 1,                    //PageIndex
                                     listOfvehicleMakes.PageSize);

            ////PaginatedList of VehicleMakeVM (ViewModel)
            //var vehiclemakes = IPaginatedList<VehicleMakeVM>(
            //                         _mapper.Map<List<VehicleMakeVM>>(listOfvehicleMakes), //Items
            //                         listOfvehicleMakes.Count,                             //Count
            //                         listOfvehicleMakes.PageIndex ?? 1,                    //PageIndex
            //                         listOfvehicleMakes.PageSize);                         //PageSize
           

            return View(vehicleMakes);
        }

        // GET: VehicleMake/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleMake = await _vehicleMakeService.GetVehicleMake(id);
            if (vehicleMake == null)
            {
                return NotFound();
            }
            return View(_mapper.Map<VehicleMakeVM>(vehicleMake));
        }

        // GET: VehicleMake/Create
        public IActionResult Create()
        {
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Abrv")] VehicleMake vehicleMake)
        {
            if (ModelState.IsValid)
            {
                 await _vehicleMakeService.CreateVehicleMake(vehicleMake);

                return RedirectToAction(nameof(Index));
            }
            
            return View(_mapper.Map<VehicleMakeVM>(vehicleMake));
        }

        // GET: VehicleMake/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleMakeID = await _vehicleMakeService.GetVehicleMake(id);
            
            if (vehicleMakeID == null)
            {
                return NotFound();
            }
            
            return View(_mapper.Map<VehicleMakeVM>(vehicleMakeID));
        }

        // POST: VehicleMake/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,Name,Abrv")] VehicleMake vehicleMake)
        {
            var vehicleMakeID = await _vehicleMakeService.GetVehicleMake(id);
           
            if (id != vehicleMakeID.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _vehicleMakeService.EditVehicleMake(vehicleMake);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                    return RedirectToAction(nameof(Index));
            }

            return View(_mapper.Map<VehicleMakeVM>(vehicleMake));
        }

        // GET: VehicleMake/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleMake = await _vehicleMakeService.GetVehicleMake(id);
            if (vehicleMake == null)
            {
                return NotFound();
            }

            return View(vehicleMake);
        }

        // POST: VehicleMake/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
           await _vehicleMakeService.DeleteVehicleMake(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
