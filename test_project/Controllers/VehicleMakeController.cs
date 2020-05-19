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
using AutoMapper.QueryableExtensions;

namespace test_project.Controllers
{
    public class VehicleMakeController : Controller
    {
        
        private readonly IVehicleMake _vehicleMakeService;
        private readonly IMapper _mapper;

        public VehicleMakeController( IVehicleMake vehicleMakeService, IMapper mapper)
        {
            _vehicleMakeService = vehicleMakeService;
            _mapper = mapper;
    }

        // GET: VehicleMake
        public async Task<IActionResult> Index(Sorting sort, Filtering filter,  int? page)
        {
            
            ViewBag.CurrentSort = sort.SortOrder;
            ViewBag.sortByName = string.IsNullOrEmpty(sort.SortOrder) ? "name_desc" : "";
            ViewBag.sortByAbrv = sort.SortOrder == "abrv_desc" ? "abrv_asc" : "abrv_desc";

            ViewBag.CurrentFilter = filter.SearchString;
           
            var ListOfvehicleMakes = await _vehicleMakeService.GetVehicleMakes(sort, filter, page);

            var mapVehicleMake = _mapper.Map<List<VehicleMakeVM>>(ListOfvehicleMakes);
            var count = ListOfvehicleMakes.Count;


            var PageIndex = ListOfvehicleMakes.PageIndex ?? 1;
            var PageSize = ListOfvehicleMakes.PageSize;
            var vehiclemakes = new PaginatedList<VehicleMakeVM>(
                                     mapVehicleMake,
                                     count,
                                     PageIndex,
                                     PageSize);
           

            return View(vehiclemakes);
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
            return base.View(_mapper.Map<VehicleMakeVM>(vehicleMake));
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
            var vehicleMakeCreate = _mapper.Map<VehicleMakeVM>(vehicleMake);
            return View(vehicleMakeCreate);
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
            var vehicleMakeEdit = _mapper.Map<VehicleMakeVM>(vehicleMakeID);
            return View(vehicleMakeEdit);
        }

        // POST: VehicleMake/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Abrv")] VehicleMake vehicleMake)
        {
            var vehicleMakeID = await _vehicleMakeService.GetVehicleMake(id);
            
            if (id != vehicleMake.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                  await _vehicleMakeService.EditVehicleMake(vehicleMake);
                return RedirectToAction(nameof(Index));
            }
            var vehicleMakeEdit = _mapper.Map<VehicleMakeVM>(vehicleMakeID);
            return View(vehicleMakeEdit);
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
