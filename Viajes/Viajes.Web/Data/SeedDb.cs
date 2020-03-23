using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Viajes.Common.Enums;
using Viajes.Web.Data.Entities;
using Viajes.Web.Helpers;

namespace Viajes.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext dataContext,
            IUserHelper userHelper)
        {
            _dataContext = dataContext;
            _userHelper = userHelper;
        }
        public async Task SeedAsync()
        {
            await _dataContext.Database.EnsureCreatedAsync();
            await CheckRolesAsync();
            var admin = await CheckUserAsync("1152715410", "Julian", "Orozco", "julianorozco16@gmail.com", "3045241918", UserType.Admin);
            var user1 = await CheckUserAsync("99042914408", "Julian", "Orozco", "julianorozco16@hotmail.com", "3045241918",UserType.User);
            var user2 = await CheckUserAsync("43836163", "Julian", "Orozco", "juliaoro@bancolombia.com.co", "3045241918", UserType.User);
            await CheckViajeAsync(admin,user1,user2);
        }
        private async Task<UserEntity> CheckUserAsync(
          string document,
          string firstName,
          string lastName,
          string email,
          string phone,
          UserType userType)
        {
            var user = await _userHelper.GetUserByEmailAsync(email);
            if (user == null)
            {
                user = new UserEntity
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Document = document,
                    UserType = userType
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());
            }

            return user;
        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }


        private async Task CheckViajeAsync(UserEntity Admin,
            UserEntity user1, UserEntity user2)
        {
            if (_dataContext.Trips.Any())
            {
                return;
            }

            _dataContext.Trips.Add(new TripEntity
            {
                StartDateTrip = DateTime.UtcNow,
                EndDateTrip = DateTime.UtcNow.AddMinutes(30),
                DestinyCity = "Minnesota",
                User=Admin,
                TripDetails = new List<TripDetailEntity>
               
                {
                    new TripDetailEntity
                {
                Origin="Medellin",
                Description="Se realiza el viaje para cerrar negocios y visitar la familia",
                ReceiptPath="gsfgg/cdrgt/veg.jpg",
                Costs=new List<CostEntity>
                {
                    new CostEntity
                    {
                        Value=2000000,
                        Category="VUELOS",
                        CreatedDate=DateTime.UtcNow.AddMinutes(30)
                    },
                    new CostEntity
                    {
                        Value=5000000,
                        Category="Hospedaje y Alimentacion",
                        CreatedDate=DateTime.UtcNow.AddMinutes(30)
                    },   new CostEntity
                    {
                        Value=1000000,
                        Category="Rumba",
                        CreatedDate=DateTime.UtcNow.AddMinutes(30)
                    }
                }
            }
                }
            }); _dataContext.Trips.Add(new TripEntity
            {
                StartDateTrip = DateTime.UtcNow,
                EndDateTrip = DateTime.UtcNow.AddMinutes(30),
                DestinyCity = "Minnesota",
                User = Admin,
                TripDetails = new List<TripDetailEntity>

                {
                    new TripDetailEntity
                {
                Origin="Medellin",
                Description="Se realiza el viaje para un acompañamiento y publicidad de la marca",
                ReceiptPath="gsfgg/cdrgt/veg.jpg",
                Costs=new List<CostEntity>
                {
                    new CostEntity
                    {
                        Value=4500000,
                        Category="VUELOS",
                        CreatedDate=DateTime.UtcNow.AddMinutes(30)
                    },
                    new CostEntity
                    {
                        Value=1000000,
                        Category="comida",
                        CreatedDate=DateTime.UtcNow.AddMinutes(30)
                    },   new CostEntity
                    {
                        Value=5000000,
                        Category="Regalos",
                        CreatedDate=DateTime.UtcNow.AddMinutes(30)
                    }
                }
            }
                }
            });
            _dataContext.Trips.Add(new TripEntity
            {
                StartDateTrip = DateTime.UtcNow,
                EndDateTrip = DateTime.UtcNow.AddMinutes(4600),
                DestinyCity = "Miami",
                User = user1,
                TripDetails = new List<TripDetailEntity>
                {new TripDetailEntity
                {
                Origin="Bogota",
                Description="Se realiza el viaje por vacaciones y turismo",
                ReceiptPath="gsfgg/cdrgt/veg.jpg",
                Costs=new List<CostEntity>
                {
                    new CostEntity
                    {
                        Value=10000000,
                        Category="Vuelos",
                        CreatedDate=DateTime.UtcNow.AddMinutes(4600)
                    },
                    new CostEntity
                    {
                        Value=10000000,
                        Category="Hospedaje",
                        CreatedDate=DateTime.UtcNow.AddMinutes(4600)
                    },
                    new CostEntity
                    {
                        Value=50000000,
                        Category="Turismo",
                        CreatedDate=DateTime.UtcNow.AddMinutes(4600)
                    },   new CostEntity
                    {
                        Value=20000000,
                        Category="Ropa y Regalos",
                        CreatedDate=DateTime.UtcNow.AddMinutes(4600)
                    }
                }

            } }
            });
            _dataContext.Trips.Add(new TripEntity
            {
                StartDateTrip = DateTime.UtcNow,
                EndDateTrip = DateTime.UtcNow.AddMinutes(1200),
                DestinyCity = "Shangai",
                User = user2,
                TripDetails = new List<TripDetailEntity>
                {new TripDetailEntity
                {
                Origin="Medellin",
                Description="Se realiza el viaje para cerrar negocio de exportacion",
                ReceiptPath="gsfgg/cdrgt/veg.jpg",
                Costs=new List<CostEntity>
                {
                    new CostEntity
                    {
                        Value=15000000,
                        Category="Vuelos",
                        CreatedDate=DateTime.UtcNow.AddMinutes(1200)
                    },
                    new CostEntity
                    {
                        Value=5000000,
                        Category="Pago de Hospedaje Casa Compañera",
                        CreatedDate=DateTime.UtcNow.AddMinutes(1200)
                    },
                    new CostEntity
                    {
                        Value=10000000,
                        Category="Viaticos y transporte",
                        CreatedDate=DateTime.UtcNow.AddMinutes(1200)
                    },   new CostEntity
                    {
                        Value=2000000000,
                        Category="Compra de inventario Empresa",
                        CreatedDate=DateTime.UtcNow.AddMinutes(1200)
                    }
                }

            } }
            });
            await _dataContext.SaveChangesAsync();

        }


    }
}
