namespace CoachExerciseApp.UI.Models.DTO
{
    public class ExerciseDTO
    {
        public Guid Id { get; set; }
        public string ClientName { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string? Feedback { get; set; }

        public ExerciseDTO()
        {
            Status = "PENDING";
        }
    }
}
