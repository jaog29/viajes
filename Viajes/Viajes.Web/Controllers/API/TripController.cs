using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Viajes.Web.Data.Entities;
using Viajes.Web.Helpers;

namespace Viajes.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConverterHelper _converterHelper;

        public TripController(DataContext context,
            IConverterHelper converterHelper)
        {
            _context = context;
            _converterHelper = converterHelper;

        }


        // GET: api/Trip/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaxiEntity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            TripEntity tripEntity = await _context.Trips
                .Include(t => t.User)
                .Include(t => t.TripDetails)
                .ThenInclude(t => t.Costs)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tripEntity == null)
            {
                
                return NotFound();
            }

            return Ok(_converterHelper.ToTripResponse(tripEntity));
        }
    }
}
