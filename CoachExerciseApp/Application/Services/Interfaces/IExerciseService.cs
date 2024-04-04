using Infrastructure.API.Models.DTO;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.API.Services.Interfaces
{
    public interface IExerciseService
    {
        Task<List<Exercise>> GetList();
        Task<Exercise> AddExercise(AddExerciseRequestDTO exercise);
        Task<Exercise> GetExerciseById(Guid id);
        Task<Exercise> UpdateExerciseStatus(UpdateExerciseStatusDTO excersiceDTO);
    }
}
