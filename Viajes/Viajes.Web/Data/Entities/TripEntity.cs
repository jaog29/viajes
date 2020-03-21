using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Viajes.Web.Data.Entities
{
    public class TripEntity
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Start Date Trip")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = false)]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public DateTime StartDateTrip { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "End Date Trip")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = false)]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public DateTime EndDateTrip { get; set; }
        [Required(ErrorMessage = "The field {0} is mandatory.")]

        public string DestinyCity { get; set; }

        public UserEntity User { get; set; }
        public ICollection<TripDetailEntity> TripDetails { get; set; }

    }
}
