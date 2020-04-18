using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Viajes.Common.Models
{
    public class CostTripRequest
    {
        [Required]  
        public int  TripId { get; set; }
        [Required]
        public float Value { get; set; }
        [Required]
        public string Category { get; set; }
        public DateTime CreatedDate { get; set; }
        
    }
}
