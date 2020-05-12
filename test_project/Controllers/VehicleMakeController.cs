﻿using System;
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
using Project_service.Paging;

namespace test_project.Controllers
{
    public class VehicleMakeController : Controller
    {
        private readonly VehicleContext _context;
        private readonly IVehicleMake _vehicleMake;
        private readonly IMapper _mapper;

        public VehicleMakeController(VehicleContext context, IVehicleMake vehiclemake, IMapper mapper)
        {
            _context = context;
            _vehicleMake = vehiclemake;
            _mapper = mapper;
    }

        // GET: VehicleMake
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
           
            ViewBag.CurrentSort = sortOrder;
            ViewBag.sortByName = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.sortByAbrv = sortOrder== "abrv_desc" ? "abrv_asc" : "abrv_desc";

            ViewBag.CurrentFilter = searchString;

            var vehicleMakes = await _vehicleMake.GetVehicleMakes(sortOrder,currentFilter,searchString,page);

            var listOfVehicleMakes = _mapper.Map<PaginatedList<VehicleMake>, PaginatedList<VehicleMakeVM>>(vehicleMakes);

            return View(listOfVehicleMakes);
        }

        // GET: VehicleMake/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleMake = await _vehicleMake.GetVehicleMake(id);
            if (vehicleMake == null)
            {
                return NotFound();
            }

            return View(vehicleMake);
        }

        // GET: VehicleMake/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VehicleMake/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Abrv")] VehicleMake vehicleMake)
        { 
            
            if (ModelState.IsValid)
            {
              await _vehicleMake.CreateVehicleMake(vehicleMake);
               
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleMake);
        }

        // GET: VehicleMake/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleMake = await _vehicleMake.GetVehicleMake(id);
            if (vehicleMake == null)
            {
                return NotFound();
            }
            return View(vehicleMake);
        }

        // POST: VehicleMake/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Abrv")] VehicleMake vehicleMake)
        {
            if (id != vehicleMake.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _vehicleMake.EditVehicleMake(vehicleMake);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleMakeExists(vehicleMake.Id))
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
            return View(vehicleMake);
        }

        // GET: VehicleMake/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleMake = await _vehicleMake.GetVehicleMake(id);
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
           await _vehicleMake.DeleteVehicleMake(id);
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleMakeExists(int id)
        {
            return _context.VehicleMakes.Any(e => e.Id == id);
        }
    }
}
