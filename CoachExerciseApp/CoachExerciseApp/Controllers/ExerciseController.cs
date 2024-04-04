using Application.API.Services.Interfaces;
using Infrastructure.API.Models.DTO;
using Infrastructure.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Models;

namespace CoachExerciseApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        private IExerciseService exerciseService;
        public ExerciseController(IExerciseService exerciseService)
        {
            this.exerciseService = exerciseService;
        }

        // GET ALL EXERCISES
        // GET: https://localhost:portnumber/api/exercise
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var exercises = await exerciseService.GetList();

            return Ok(exercises);
        }

        // GET SINGLE EXERCISE (By Id)
        // GET: https://localhost:portnumber/api/exercise/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var exercise = await exerciseService.GetExerciseById(id);
            if (exercise == null)
            {
                return NotFound();
            }

            return Ok(exercise);
        }

        // POST To Create New Exercise 
        // POST: https://localhost:portnumber/api/exercise
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddExerciseRequestDTO addExerciseRequestDTO)
        {
            // Map or Convert DTO to Domain Model & Use Domain Model to create Exercise
            var exerciseDomainModel = await exerciseService.AddExercise(addExerciseRequestDTO);

            //Map Domain model back to DTO
            var exerciseDTO = new ExerciseDTO
            { 
                Id = exerciseDomainModel.Id,
                ClientName = exerciseDomainModel.ClientName,
                Description = exerciseDomainModel.Description,
                Date = exerciseDomainModel.Date
            };

            return CreatedAtAction(nameof(GetById), new { id = exerciseDTO.Id }, exerciseDTO);
        }

        // PUT To Update an existing exercise 
        // PUT: https://localhost:portnumber/api/exercise/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateStatus([FromRoute] Guid id, [FromBody] UpdateExerciseStatusDTO updateExerciseStatusDTO)
        {
            var updatedExercise = await exerciseService.UpdateExerciseStatus(updateExerciseStatusDTO);

            if (updatedExercise == null)
            {
                return NotFound();
            }

            var exerciseDTO = new UpdateExerciseStatusDTO
            { 
                Id = updatedExercise.Id,
                Status = updatedExercise.Status,
                Feedback = updatedExercise.Feedback
            };


            return Ok(exerciseDTO);
        }
    }
}
