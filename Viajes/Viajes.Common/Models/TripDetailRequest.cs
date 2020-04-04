using System;
using System.Collections.Generic;
using System.Text;

namespace Viajes.Common.Models
{
    public class TripDetailRequest
    {
        public int Id { get; set; }
        public string Origin { get; set; }
        public string Description { get; set; }
        public string PicturePath { get; set; }

        public List<CostResponse> Costs { get; set; }
    }
}
