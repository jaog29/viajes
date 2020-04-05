using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Viajes.Common.Models;
using Viajes.Web.Data.Entities;

namespace Viajes.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        public TripResponse ToTripResponse(TripEntity tripEntity)
        {

            return new TripResponse
            {
                Id = tripEntity.Id,
                DestinyCity = tripEntity.DestinyCity,
                StartDate = tripEntity.StartDateTrip,
                EndDate = tripEntity.EndDateTrip,
                TripDetails = tripEntity.TripDetails?.Select(t => new TripDetailResponse
                {


                    Id = t.Id,
                    Origin = t.Origin,
                    Description = t.Description,
                    PicturePath = t.PicturePath,
                 
                    Costs = t.Costs?.Select(td => new CostResponse
                    {

                        Id = td.Id,
                        Value = td.Value,
                        Category = td.Category,
                        CreatedDate = td.CreatedDate

                    }).ToList(),
                    
                }).ToList(),
                User = ToUserResponse(tripEntity.User)
            
             
            };
        }

     


        public UserResponse ToUserResponse(UserEntity user)
        {
            if (user == null)
            {
                return null;
            }

            return new UserResponse
            {
              
                Document = user.Document,
                Email = user.Email,
                FirstName = user.FirstName,
                Id = user.Id,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                UserType = user.UserType
            };
        }
        public List<TripResponseWithUser> ToTripResponse(List<TripEntity> tripEntities)
        {
            return tripEntities.Select(t => new TripResponseWithUser
            {
                Id = t.Id,
                DestinyCity = t.DestinyCity,
                StartDate = t.StartDateTrip,
                EndDate = t.EndDateTrip,
                
                TripDetails = t.TripDetails.Select(td => new TripDetailResponse
                {


                    Id = td.Id,
                    Origin = td.Origin,
                    Description = td.Description,
                    PicturePath = td.PicturePath,
                    Trip = ToTripResponse2(td.Trip),
                    Costs = td.Costs.Select(tc => new CostResponse
                    {

                        Id = tc.Id,
                        Value = tc.Value,
                        Category = tc.Category,
                        CreatedDate = tc.CreatedDate
                        
                    }).ToList()

                }).ToList()


            }).ToList();
        }

        private TripResponse ToTripResponse2(TripEntity trips)
        {
            return new TripResponse
            {
               Id = trips.Id,
               StartDate=trips.StartDateTrip,
               EndDate=trips.EndDateTrip,
               DestinyCity=trips.DestinyCity,
               
                User = ToUserResponse(trips.User)
            };
        }
    }
}
