using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Viajes.Web.Data.Entities
{
    public class TripDetailEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Origin { get; set; }

        public string Description { get; set; }
        public string ReceiptPath { get; set; }
        public TripEntity Trip { get; set; }
        public ICollection<CostEntity> Costs { get; set; }

    }
}
