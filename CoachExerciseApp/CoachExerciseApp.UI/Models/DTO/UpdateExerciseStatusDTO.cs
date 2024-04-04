using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.API.Models.DTO
{
    public class UpdateExerciseStatusDTO
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public string Feedback {  get; set; }
    }
}
