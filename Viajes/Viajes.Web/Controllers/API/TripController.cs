using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Viajes.Common.Models;
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
        [HttpGet("{destinyCity}")]
        public async Task<IActionResult> GetTripEntity([FromRoute] string destinyCity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<TripEntity> result = _context.Trips
         .Where(t => t.DestinyCity == destinyCity)
         .Include(t => t.User)
         .Include(t => t.TripDetails)
         .ThenInclude(t => t.Costs)
         .ToList<TripEntity>();

            List<TripResponse> list2 = new List<TripResponse>();
            foreach (TripEntity element in result)
            {
                list2.Add(_converterHelper.ToTripResponse(element));
            }

            return Ok(list2);

        }


    }
}
