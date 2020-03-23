using System;
using System.Collections.Generic;
using System.Text;

namespace Viajes.Common.Models
{
    public class CostResponse
    {
        public int Id { get; set; }
        public float Value { get; set; }
        public string Category { get; set; }
        public DateTime CreatedDate { get; set; }
      
    }
}
