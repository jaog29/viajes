using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Viajes.Common.Models
{
    public class TripRequest
    {
   
        

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public Guid UserId { get; set; }
        
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
        public string Origin { get; set; }

        public string Description { get; set; }
        public string ReceiptPath { get; set; }
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public float Value { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [MaxLength(100, ErrorMessage = "The {0} field must have {1} characters.")]
        public string Category { get; set; }

        public DateTime CreatedDate { get; set; }

      

    }

}
