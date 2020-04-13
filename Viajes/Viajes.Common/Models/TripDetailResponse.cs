using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Viajes.Common.Models
{
    public class TripDetailResponse
    {
        public int Id { get; set; }
        public string Origin { get; set; }
        public string Description { get; set; }
        public string PicturePath { get; set; }
        public TripResponse Trip { get; set; }
        public float TotalCost => Costs == null ? 0 : Costs.Sum(t => t.Value);
        public int CostsCount => Costs == null ? 0 : Costs.Count;
        public List<CostResponse> Costs{ get; set; }
        public string PictureFullPath => string.IsNullOrEmpty(PicturePath)
     ? "https://viajeswebproject1.azurewebsites.net//images/noimage.png"
     : $"https://viajeswebproject1.azurewebsites.net{PicturePath.Substring(1)}";


    }
}
