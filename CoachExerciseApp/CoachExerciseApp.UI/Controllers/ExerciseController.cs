using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using CoachExerciseApp.UI.Models.DTO;
using CoachExerciseApp.UI.Models;
using System.Text.Json;
using System.Text;
using System.Reflection;
using Infrastructure.API.Models.DTO;

namespace CoachExerciseApp.UI.Controllers
{
    public class ExerciseController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;
        public ExerciseController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<ExerciseWithUrlModel> response = new List<ExerciseWithUrlModel>();
            try
            {
                // Get all Exercises from Web API
                var client = httpClientFactory.CreateClient();

                var httpResponseMessage = await client.GetAsync("https://localhost:7000/api/exercise");

                httpResponseMessage.EnsureSuccessStatusCode();

                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<ExerciseWithUrlModel>>());

                foreach (var exercise in response)
                {
                    exercise.UniqueUrl = $"https://localhost:7002/Exercise/ExerciseById/{exercise.Id}";
                }
            }
            catch(Exception ex)
            {
                // Log the exception
                throw;
            }


            return View(response);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddExerciseModel model)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7000/api/exercise"),
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<ExerciseDTO>();

            if (response is not null)
            { 
                return RedirectToAction("Index", "Exercise");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ExerciseById(Guid id)
        {
            var client = httpClientFactory.CreateClient();

            var response = await client.GetFromJsonAsync<ExerciseDTO>($"https://localhost:7000/api/exercise/{id.ToString()}");

            if (response is not null)
            {
                return View(response);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> MarkExercise(Guid id, string status, string feedback)
        {
            var client = httpClientFactory.CreateClient();

            var updateStatusRequest = new UpdateExerciseStatusDTO
            {
                Id = id,
                Status = status,
                Feedback = feedback
            };

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7000/api/exercise/{updateStatusRequest.Id}"),
                Content = new StringContent(JsonSerializer.Serialize(updateStatusRequest), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);

            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<UpdateExerciseStatusDTO>();

            if (response is not null)
            {
                return RedirectToAction("ExerciseById", "Exercise", new { id = id });
            }
            return RedirectToAction("ExerciseById", "Exercise");
        }
    }
}
