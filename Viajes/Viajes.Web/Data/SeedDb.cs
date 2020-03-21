using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Viajes.Web.Data.Entities;

namespace Viajes.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _dataContext;

        public SeedDb(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task SeedAsync()
        {
            await _dataContext.Database.EnsureCreatedAsync();
            await CheckViajeAsync();
        }
        private async Task CheckViajeAsync()
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
                        CreatedDate=DateTime.UtcNow.AddMinutes(30),
                    },
                    new CostEntity
                    {
                        Value=5000000,
                        Category="Hospedaje y Alimentacion",
                        CreatedDate=DateTime.UtcNow.AddMinutes(30),
                    },   new CostEntity
                    {
                        Value=1000000,
                        Category="Rumba",
                        CreatedDate=DateTime.UtcNow.AddMinutes(30),
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
                        CreatedDate=DateTime.UtcNow.AddMinutes(4600),
                    },
                    new CostEntity
                    {
                        Value=10000000,
                        Category="Hospedaje",
                        CreatedDate=DateTime.UtcNow.AddMinutes(4600),
                    },
                    new CostEntity
                    {
                        Value=50000000,
                        Category="Turismo",
                        CreatedDate=DateTime.UtcNow.AddMinutes(4600),
                    },   new CostEntity
                    {
                        Value=20000000,
                        Category="Ropa y Regalos",
                        CreatedDate=DateTime.UtcNow.AddMinutes(4600),
                    }
                }

            } }
            });
            _dataContext.Trips.Add(new TripEntity
            {
                StartDateTrip = DateTime.UtcNow,
                EndDateTrip = DateTime.UtcNow.AddMinutes(1200),
                DestinyCity = "Shangai",
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
                        CreatedDate=DateTime.UtcNow.AddMinutes(1200),
                    },
                    new CostEntity
                    {
                        Value=5000000,
                        Category="Pago de Hospedaje Casa Compañera",
                        CreatedDate=DateTime.UtcNow.AddMinutes(1200),
                    },
                    new CostEntity
                    {
                        Value=10000000,
                        Category="Viaticos y transporte",
                        CreatedDate=DateTime.UtcNow.AddMinutes(1200),
                    },   new CostEntity
                    {
                        Value=2000000000,
                        Category="Compra de inventario Empresa",
                        CreatedDate=DateTime.UtcNow.AddMinutes(1200),
                    }
                }

            } }
            });
            await _dataContext.SaveChangesAsync();

        }


    }
}
