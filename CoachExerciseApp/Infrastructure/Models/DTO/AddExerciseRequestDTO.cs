using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.API.Models.DTO
{
    public class AddExerciseRequestDTO
    {
        public string ClientName { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }

        public AddExerciseRequestDTO()
        {
            Status = "PENDING";
        }
    }
}
