﻿using System.ComponentModel.DataAnnotations;

namespace CoachExerciseApp.UI.Models
{
    public class AddExerciseModel
    {
        public string ClientName { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }

        public AddExerciseModel()
        {
            Status = "PENDING";
        }
    }
}
