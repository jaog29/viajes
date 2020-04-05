﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Viajes.Common.Models
{
   public class TripResponse { 
      public int Id { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime StartDateLocal => StartDate.ToLocalTime();

    public DateTime? EndDate { get; set; }

    public DateTime? EndDateLocal => EndDate?.ToLocalTime();

        public string DestinyCity { get; set; }

        public List<TripDetailResponse> TripDetails { get; set; }
        public UserResponse User { get; set; }

     
    }
}
