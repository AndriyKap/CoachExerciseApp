using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.API.Services.Interfaces;
using Infrastructure.API.Data.DataAccess;
using Infrastructure.API.Models.DTO;
using Infrastructure.Models;

namespace Application.API.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly IExerciseRepository exerciseRepository;

        public ExerciseService(IExerciseRepository exerciseRepository)
        { 
            this.exerciseRepository = exerciseRepository;
        }

        public async Task<List<Exercise>> GetList()
        {
            try
            {
                var all = await exerciseRepository.GetAll();
                var filteredList = all.Where(item => item != null).ToList();

                var currentDate = DateTime.Now;
                foreach (var exercise in filteredList)
                {
                    if (exercise.Date < currentDate && exercise.Status == "PENDING")
                    {
                        exercise.Status = "MISSED";
                        await exerciseRepository.Update(exercise);
                    }
                }

                return filteredList;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Exercise> AddExercise(AddExerciseRequestDTO exercise)
        {
            // Map or Convert DTO to Domain Model
            var exerciseDomainModel = new Exercise
            {
                ClientName = exercise.ClientName,
                Description = exercise.Description,
                Date = exercise.Date,
                Status = exercise.Status
            };

            var addedExercise = await exerciseRepository.Add(exerciseDomainModel);

            return addedExercise;
        }

        public async Task<Exercise> GetExerciseById(Guid id)
        { 
            return await exerciseRepository.GetById(id);
        }

        public async Task<Exercise> UpdateExerciseStatus(UpdateExerciseStatusDTO exersiceDTO)
        {
            var foundExercise = await GetExerciseById(exersiceDTO.Id);

            foundExercise.Status = exersiceDTO.Status;
            foundExercise.Feedback = exersiceDTO.Feedback;

            var updatedExercise = await exerciseRepository.Update(foundExercise);
            return updatedExercise;
        }

    }
}
