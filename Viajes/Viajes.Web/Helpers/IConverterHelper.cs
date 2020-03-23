using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Viajes.Common.Models;
using Viajes.Web.Data.Entities;

namespace Viajes.Web.Helpers
{
    public interface IConverterHelper
    {
        TripResponse ToTripResponse(TripEntity tripEntity);

    }
}
