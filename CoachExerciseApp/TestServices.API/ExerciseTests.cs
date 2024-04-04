using Application.API.Services;
using Infrastructure.API.Data.DataAccess;
using Infrastructure.API.Models.DTO;
using Infrastructure.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestServices.API
{
    public class ExerciseTests
    {
        [Fact]
        public async Task GetList_ReturnsListOfExercises()
        {
            // Arrange
            var mockExerciseRepository = new Mock<IExerciseRepository>();
            var exercises = new List<Exercise>
            {
                new Exercise { Id = Guid.NewGuid(), Date = DateTime.Now.AddDays(-1), Status = "PENDING" },
                new Exercise { Id = Guid.NewGuid(), Date = DateTime.Now.AddDays(1), Status = "PENDING" }
            };
            mockExerciseRepository.Setup(repo => repo.GetAll()).ReturnsAsync(exercises);
            var exerciseService = new ExerciseService(mockExerciseRepository.Object);

            // Act
            var result = await exerciseService.GetList();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(exercises.Count, result.Count);
        }

        [Fact]
        public async Task AddExercise_ReturnsAddedExercise()
        {
            // Arrange
            var mockExerciseRepository = new Mock<IExerciseRepository>();
            var newExercise = new Exercise { Id = Guid.NewGuid(), ClientName = "TestClient", Description = "TestDescription", Date = DateTime.Now, Status = "PENDING" };
            mockExerciseRepository.Setup(repo => repo.Add(It.IsAny<Exercise>())).ReturnsAsync(newExercise);
            var exerciseService = new ExerciseService(mockExerciseRepository.Object);
            var addExerciseRequestDTO = new AddExerciseRequestDTO
            {
                ClientName = "TestClient",
                Description = "TestDescription",
                Date = DateTime.Now,
                Status = "PENDING"
            };

            // Act
            var result = await exerciseService.AddExercise(addExerciseRequestDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newExercise.ClientName, result.ClientName);
            Assert.Equal(newExercise.Description, result.Description);
            Assert.Equal(newExercise.Date, result.Date);
            Assert.Equal(newExercise.Status, result.Status);
        }

        [Fact]
        public async Task GetExerciseById_ReturnsExercise()
        {
            // Arrange
            var mockExerciseRepository = new Mock<IExerciseRepository>();
            var expectedExercise = new Exercise { Id = Guid.NewGuid(), ClientName = "TestClient", Description = "TestDescription", Date = DateTime.Now, Status = "PENDING" };
            mockExerciseRepository.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(expectedExercise);
            var exerciseService = new ExerciseService(mockExerciseRepository.Object);
            var exerciseId = Guid.NewGuid();

            // Act
            var result = await exerciseService.GetExerciseById(exerciseId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedExercise.Id, result.Id);
            Assert.Equal(expectedExercise.ClientName, result.ClientName);
            Assert.Equal(expectedExercise.Description, result.Description);
            Assert.Equal(expectedExercise.Date, result.Date);
            Assert.Equal(expectedExercise.Status, result.Status);
        }

        [Fact]
        public async Task UpdateExerciseStatus_UpdatesExerciseStatusAndFeedback()
        {
            // Arrange
            var mockExerciseRepository = new Mock<IExerciseRepository>();
            var exerciseId = Guid.NewGuid();
            var exerciseStatus = "COMPLETED";
            var exerciseFeedback = "Great job!";
            var updateExerciseStatusDTO = new UpdateExerciseStatusDTO
            {
                Id = exerciseId,
                Status = exerciseStatus,
                Feedback = exerciseFeedback
            };
            var initialExercise = new Exercise { Id = exerciseId, Status = "PENDING" };
            var updatedExercise = new Exercise { Id = exerciseId, Status = exerciseStatus, Feedback = exerciseFeedback };
            mockExerciseRepository.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync(initialExercise);
            mockExerciseRepository.Setup(repo => repo.Update(It.IsAny<Exercise>())).ReturnsAsync(updatedExercise);
            var exerciseService = new ExerciseService(mockExerciseRepository.Object);

            // Act
            var result = await exerciseService.UpdateExerciseStatus(updateExerciseStatusDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(exerciseId, result.Id);
            Assert.Equal(exerciseStatus, result.Status);
            Assert.Equal(exerciseFeedback, result.Feedback);
        }
    }
}
