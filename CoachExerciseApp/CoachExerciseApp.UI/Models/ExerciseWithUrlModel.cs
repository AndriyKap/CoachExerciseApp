namespace CoachExerciseApp.UI.Models
{
    public class ExerciseWithUrlModel
    {
        public Guid Id { get; set; }
        public string ClientName { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string UniqueUrl { get; set; }
        public string? Feedback { get; set; }

        public ExerciseWithUrlModel()
        {
            Status = "PENDING";
        }
    }
}
