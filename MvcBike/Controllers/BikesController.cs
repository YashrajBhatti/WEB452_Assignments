using System;
using System.Collections.Generic;
using System.Linq;
using MvcBike.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcBike.Models;

namespace MvcBike.Controllers
{
    public class BikesController : Controller
    {
        private readonly MvcBikeContext _context;

        public BikesController(MvcBikeContext context)
        {
            _context = context;
        }

        // GET: bikes
public async Task<IActionResult> Index(string bikeCompany, string searchString, string sortOrder)
    {
        // Use LINQ to get list of companys.
        IQueryable<string> companyQuery = from m in _context.Bike
                                        orderby m.Company
                                        select m.Company;
        var bikes = from m in _context.Bike
                    select m;

        if (!string.IsNullOrEmpty(searchString))
        {
           bikes = bikes.Where(s => s.Model!.Contains(searchString));
        }

        

        if (!string.IsNullOrEmpty(bikeCompany))
        {
            bikes = bikes.Where(x => x.Company!.Contains(bikeCompany));
        }

        ViewData["SearchModel"] = searchString;
        ViewData["SearchCompany"] = bikeCompany;

       

        
        bikes = SortBikes(bikes,sortOrder);

        var bikeCompanyVM = new BikeCompanyViewModel
        {
            Company = new SelectList(await companyQuery.Distinct().ToListAsync()),
            Bikes = await bikes.ToListAsync()
        };

        return View(bikeCompanyVM);
    }

        private IQueryable<Bike> SortBikes(IQueryable<Bike> bikes, string sortOrder)
        {

            ViewData["ModelSort"] = String.IsNullOrEmpty(sortOrder) ? "model_desc" : "";
            ViewData["DateSort"] = sortOrder == "launchDate" ? "launchDate_desc" : "launchDate";
            ViewData["CompanySort"] = sortOrder == "company" ? "company_desc" : "company";
            ViewData["PriceSort"] = sortOrder == "price" ? "price_desc" : "price";
            ViewData["RatingSort"] = sortOrder == "rating" ? "rating_desc" : "rating";

            switch (sortOrder)
            {
                case "model_desc":        
                    return (bikes.OrderByDescending(m => m.Model));

                case "launchDate":        
                
                    return (bikes.OrderBy(m => m.LaunchDate));
                
                case "launchDate_desc":        
                
                    return(bikes.OrderByDescending(m => m.LaunchDate));                

                case "company":        
                
                    return(bikes.OrderBy(m => m.Company));

                case "company_desc":        
                
                    return(bikes.OrderByDescending(m => m.Company));
                    
                case "price":       
                 
                    return(bikes.OrderBy(m => (double?)m.Price));

                case "price_desc":       
                
                    return(bikes.OrderByDescending(m =>(double?)m.Price ));
                    
                case "rating":        
                
                    return(bikes.OrderBy(m => m.Rating));

                case "rating_desc":        
                
                    return(bikes.OrderByDescending(m => m.Rating));
                  
                default:
                   return( bikes.OrderBy(m => m.Model));
            }


        }

        // GET: bikes/Details/5
        public async Task<IActionResult> Details(int? id, string? rating)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bike = await _context.Bike
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bike == null)
            {
                return NotFound();
            }

            return View(bike);
        }

        // GET: bikes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: bikes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Model,launchDate,company,Price,Rating")] Bike bike)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bike);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bike);
        }

        // GET: bikes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bike = await _context.Bike.FindAsync(id);
            if (bike == null)
            {
                ViewData["Id"] = id;
                return View("../Example/Index");
            }
            return View(bike);
        }

        // POST: bikes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Model,launchDate,company,Price,Rating")] Bike bike)
        {
            if (id != bike.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bike);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BikeExists(bike.Id))
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
            return View(bike);
        }

        // GET: bikes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bike = await _context.Bike
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bike == null)
            {
                return NotFound();
            }

            return View(bike);
        }

        // POST: bikes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bike = await _context.Bike.FindAsync(id);
            _context.Bike.RemoveRange(bike);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // GET: bikes/DeleteAll
        public async Task<IActionResult> DeleteAll()
        {
            return View();
        }

        // POST: bikes/DeleteAll
        [HttpPost, ActionName("DeleteAll")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedAll()
        {
             var allbikes = _context.Bike.ToList();
            if (allbikes.Count > 0) 
            {
                _context.Bike.RemoveRange(allbikes);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSelected(int[] selectedBikes)
        {

            var bikesToBeDeleted = _context.Bike.Where(m => selectedBikes.Contains(m.Id)).ToList();
            if (bikesToBeDeleted.Count > 0) 
            {
                _context.Bike.RemoveRange(bikesToBeDeleted);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
            
        }
        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> HideSelected(int[] selectedBikes)
{
    var bikesToBeHidden = _context.Bike.Where(m => selectedBikes.Contains(m.Id)).ToList();
    if (bikesToBeHidden.Count > 0) 
    {
        foreach (var bike in bikesToBeHidden)
        {
            bike.IsHidden = true; // Set the IsHidden flag to true
        }
        await _context.SaveChangesAsync();
    }

    // Passing the list of hidden bikes to the new view
    return View("HiddenBikes", bikesToBeHidden); 
}



        private bool BikeExists(int id)
        {
            return _context.Bike.Any(e => e.Id == id);
        }
    }
}
