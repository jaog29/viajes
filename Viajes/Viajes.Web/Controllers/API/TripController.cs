using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Viajes.Common.Models;
using Viajes.Web.Data.Entities;
using Viajes.Web.Helpers;

namespace Viajes.Web.Controllers.API
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IConverterHelper _converterHelper;

        public TripsController(DataContext context, IUserHelper userHelper, IConverterHelper converterHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _converterHelper = converterHelper;
        }

        [HttpPost]
        public async Task<IActionResult> PostTripEntity([FromBody] TripRequest tripRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserEntity userEntity = await _userHelper.GetUserAsync(tripRequest.UserId);
            if (userEntity == null)
            {
                return BadRequest("User doesn't exists.");
            }



            TripEntity TripEntity = new TripEntity
            {
                StartDateTrip = tripRequest.StartDateTrip,
                EndDateTrip = tripRequest.EndDateTrip,
                DestinyCity = tripRequest.DestinyCity,
                TripDetails = new List<TripDetailEntity>
                {
                    new TripDetailEntity
                    {
                        Origin =tripRequest.Origin,
                        Description=tripRequest.Description,
                        PicturePath=tripRequest.ReceiptPath,
                        Costs=new List<CostEntity>
                        {
                            new CostEntity
                            {
                                Value=tripRequest.Value,
                                Category=tripRequest.Category,
                                CreatedDate= DateTime.UtcNow
                            }

                        }


                    }
                },
                User = userEntity,
            };

            _context.Trips.Add(TripEntity);
            await _context.SaveChangesAsync();
            return Ok(_converterHelper.ToTripResponse(TripEntity));
        }
    }
}

