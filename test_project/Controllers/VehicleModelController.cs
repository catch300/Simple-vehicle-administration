using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_service.Models;
using Project_service.PagingFIlteringSorting;
using Project_service.Service;
using test_project.Models.ViewModels;

namespace test_project.Controllers
{
    public class VehicleModelController : Controller
    {
        private readonly VehicleContext _context;
        private readonly IVehicleModelService _vehicleModelService;
        private readonly IMapper _mapper;
        private ISorting _sort;
        private IFiltering _filter;
        private IPaginatedList<VehicleModelVM> _vehicleModels;


        public VehicleModelController(VehicleContext context, IVehicleModelService vehicleModel, IMapper mapper, ISorting sorting, IFiltering filtering, IPaginatedList<VehicleModelVM> vehicleModels)
        {
            _context = context;
            _vehicleModelService = vehicleModel;
            _mapper = mapper;
            _sort = sorting;
            _filter = filtering;
            _vehicleModels = vehicleModels;
        }

        // GET: VehicleModel
        public async Task<IActionResult> Index(Sorting sort, Filtering filter,  int? page)
        {
            _sort = new Sorting(sort.SortOrder);
            _filter = new Filtering(filter.SearchString, filter.CurrentFilter);

            ViewBag.CurrentSort = sort.SortOrder;
            ViewBag.sortByMake = sort.SortOrder == "make_desc" ? "make_asc" : "make_desc";
            ViewBag.sortByName = string.IsNullOrEmpty(sort.SortOrder) ? "name_desc" : "";
            ViewBag.sortByAbrv = sort.SortOrder == "abrv_desc" ? "abrv_asc" : "abrv_desc";
            ViewBag.CurrentFilter = filter.SearchString;

            //PaginatedList of VehicleModels
            var listOfVehicleModels = await _vehicleModelService.GetVehicleModels(sort, filter, page);
          
            //PaginatedList of VehicleModelVM (ViewModel)
            _vehicleModels = new PaginatedList<VehicleModelVM>(
                                     _mapper.Map<List<VehicleModelVM>>(listOfVehicleModels), //items
                                     listOfVehicleModels.Count,                             // count
                                     listOfVehicleModels.PageIndex ?? 1,                    //PageIndex
                                     listOfVehicleModels.PageSize);                         //PageSize

            return View(_vehicleModels);
        }

        // GET: VehicleModel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleModel = await _vehicleModelService.GetVehicleModel(id);
            if (vehicleModel == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<VehicleModelVM>(vehicleModel));
        }

        // GET: VehicleModel/Create
        public IActionResult Create()
        {   ViewData["MakeId"] = new SelectList(_context.VehicleMakes, "Id", "Name");
            return View();
        }

        // POST: VehicleModel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MakeId,Name,Abrv")] VehicleModel vehicleModel)
        {
            if (ModelState.IsValid)
            {
                await _vehicleModelService.CreateVehicleModel(vehicleModel);

                return RedirectToAction(nameof(Index));
            }
           
            ViewData["MakeId"] = new SelectList(_context.VehicleMakes, "Id", "Name", vehicleModel.MakeId);
            return View(_mapper.Map<VehicleModelVM>(vehicleModel));
        }

        // GET: VehicleModel/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleModelID = await _vehicleModelService.GetVehicleModel(id);
            if (vehicleModelID == null)
            {
                return NotFound();
            }

            ViewData["MakeId"] = new SelectList(_context.VehicleMakes, "Id", "Name", vehicleModelID.MakeId);
            return View(_mapper.Map<VehicleModelVM>(vehicleModelID));
        }

        // POST: VehicleModel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MakeId,Name,Abrv")] VehicleModel vehicleModel)
        {
            if (id != vehicleModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _vehicleModelService.EditVehicleModel(vehicleModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                     throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MakeId"] = new SelectList(_context.VehicleMakes, "Id", "Name", vehicleModel.MakeId);
            return View(vehicleModel);
        }

        // GET: VehicleModel/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleModel = await _vehicleModelService.GetVehicleModel(id);
            if (vehicleModel == null)
            {
                return NotFound();
            }

            return View(vehicleModel);
        }

        // POST: VehicleModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _vehicleModelService.DeleteVehicleModel(id);
            return RedirectToAction(nameof(Index));
        }

        
    }
}
