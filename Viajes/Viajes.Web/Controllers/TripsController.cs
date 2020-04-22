using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Viajes.Web.Data.Entities;

namespace Viajes.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TripsController : Controller
    {
        private readonly DataContext _context;

        public TripsController(DataContext context)
        {
            _context = context;
        }

        // GET: Trips
        public async Task<IActionResult> Index()
        {
            return View(await _context.Trips.ToListAsync());
        }

        // GET: Trips/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /* TripEntity tripEntity = await _context.Trips
                 .FirstOrDefaultAsync(m => m.Id == id);*/
            var TripEntity = await _context.Trips
            .Include(t => t.User)
            .Include(t => t.TripDetails)
            .ThenInclude(t => t.Costs)
            .FirstOrDefaultAsync(m => m.Id == id);

            if (TripEntity == null)
            {
                return NotFound();
            }

            return View(TripEntity);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TripEntity tripEntity)
        {
            if (ModelState.IsValid)
            {
              ;
                _context.Add(tripEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tripEntity);
        }

        // GET: Trips/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TripEntity tripEntity = await _context.Trips.FindAsync(id);
            if (tripEntity == null)
            {
                return NotFound();
            }
            return View(tripEntity);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TripEntity tripEntity)
        {
            if (id != tripEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
               
                _context.Update(tripEntity);
       
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tripEntity);
        }

        // GET: Trips/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TripEntity tripEntity = await _context.Trips
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tripEntity == null)
            {
                return NotFound();
            }
         
            _context.Trips.Remove(tripEntity);
         
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Trips/Delete/5

       /* private bool TripEntityExists(int id)
        {
            return _context.Trips.Any(e => e.Id == id);
        }*/
    }
}
