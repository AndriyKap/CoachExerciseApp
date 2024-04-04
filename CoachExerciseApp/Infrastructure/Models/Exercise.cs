using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class Exercise
    {
        public Guid Id { get; set; }

        [Required]
        public string ClientName { get; set; }
        public string Description { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [EnumDataType(typeof(ExerciseStatus))]
        public string Status { get; set; }

        public string? Feedback { get; set; }
    }


    public enum ExerciseStatus
    {
        [Display(Name = "PENDING")]
        PENDING,

        [Display(Name = "DONE")]
        DONE,

        [Display(Name = "MISSED")]
        MISSED
    }
}
