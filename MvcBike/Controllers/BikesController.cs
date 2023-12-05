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
public async Task<IActionResult> Index(string bikeRating, string searchString, string sortOrder)
    {
        // Use LINQ to get list of companys.
        IQueryable<string> ratingQuery = from m in _context.Bike
                                        orderby m.Rating
                                        select m.Rating;
        var bikes = from m in _context.Bike
                    select m;

        if (!string.IsNullOrEmpty(searchString))
        {
           bikes = bikes.Where(s => s.Model!.Contains(searchString));
        }

        if (!string.IsNullOrEmpty(bikeRating))
        {
            bikes = bikes.Where(x => x.Company!.Contains(bikeRating));
        }

        ViewData["SearchModel"] = searchString;
        ViewData["SearchRating"] = bikeRating;

       

        
        bikes = SortBikes(bikes,sortOrder);

        var bikeRatingVM = new BikeRatingViewModel
        {
            Rating = new SelectList(await ratingQuery.Distinct().ToListAsync()),
            Bikes = await bikes.ToListAsync()
        };

        return View(bikeRatingVM);
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

private bool BikeExists(int id)
        {
            return _context.Bike.Any(e => e.Id == id);
        }

           //HideSelected stuff goes below here
    public async Task<IActionResult> HideSelected(int[] selectedBikes) 
    {
        if (selectedBikes != null && selectedBikes.Length > 0)
        {
            var bikesToHide = await _context.Bike
                .Where(m => selectedBikes.Contains(m.Id))
                .ToListAsync();

            foreach (var bike in bikesToHide)
            {
                bike.IsHidden = true;
            }

            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Hidden() 
    {
        var hiddenBikes = await _context.Bike.Where(m => m.IsHidden).ToListAsync();
        return View(hiddenBikes); 
    }

    public async Task<IActionResult> ReturnHiddenBikes() 
{
    var hiddenBikes = await _context.Bike.Where(m => m.IsHidden).ToListAsync();

    foreach (var bike in hiddenBikes)
    {
        bike.IsHidden = false;
    }

    await _context.SaveChangesAsync();

    return RedirectToAction(nameof(Index));
}

    // GET: Movies/DeleteAll
    public async Task<IActionResult> DeleteAll() 
        {
            return View();
        }
   // POST: Movies/DeleteAll
    [HttpPost, ActionName("DeleteAll")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmedAll() 
        {
            var allBikes = _context.Bike.ToList();
            if (allBikes.Count > 0) 
            {
                _context.Bike.RemoveRange(allBikes);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));

        }
    }
}
