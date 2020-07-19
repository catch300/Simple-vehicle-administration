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
        private ISorting _sorting;
        private IFiltering _filtering;
        private IPaginatedList<VehicleMakeVM> _vehicleMakes;

        public VehicleMakeController(IVehicleMakeService vehicleMakeService, IMapper mapper, ISorting sorting, IFiltering filtering, IPaginatedList<VehicleMakeVM> vehicleMakes)
        {
            _vehicleMakeService = vehicleMakeService;
            _mapper = mapper;
            _sorting = sorting;
            _filtering = filtering;
            _vehicleMakes = vehicleMakes;
        }

        // GET: VehicleMake
        public async Task<IActionResult> Index(Sorting sort, Filtering filter, int? page)
        {
             _sorting = new Sorting(sort.SortOrder);
             _filtering= new Filtering(filter.SearchString, filter.CurrentFilter);

            ViewBag.CurrentSort = _sorting.SortOrder;
            ViewBag.sortByName = string.IsNullOrEmpty(_sorting.SortOrder) ? "name_desc" : "";
            ViewBag.sortByAbrv = _sorting.SortOrder == "abrv_desc" ? "abrv_asc" : "abrv_desc";
            ViewBag.CurrentFilter = _filtering.SearchString;

           //PaginatedList of VehicleMakes
            var listOfvehicleMakes = await _vehicleMakeService.GetVehicleMakes(_sorting, _filtering,page);

             _vehicleMakes = new PaginatedList<VehicleMakeVM>(
                                     _mapper.Map<List<VehicleMakeVM>>(listOfvehicleMakes),      //Items
                                     listOfvehicleMakes.Count,                                  //Count
                                     listOfvehicleMakes.PageIndex ?? 1,                         //PageIndex
                                     listOfvehicleMakes.PageSize);                              //PageSize

            return View(_vehicleMakes);
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
