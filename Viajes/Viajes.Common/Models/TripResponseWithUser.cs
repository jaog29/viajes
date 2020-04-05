using System;
using System.Collections.Generic;
using System.Text;

namespace Viajes.Common.Models
{
    public class TripResponseWithUser : TripResponse
    {
        public TripResponse Trips { get; set; }
    }
}
