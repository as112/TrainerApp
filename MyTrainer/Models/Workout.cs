namespace MyTrainer.Models;

public class Workout
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public List<Exercise> Exercises { get; set; } = new();
    public WorkoutState WorkoutState { get; set; } = WorkoutState.None;
    public Guid? ScheduleId { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime FinishedAt { get; set; }
}
public enum WorkoutState : int
{
    None,
    InWaiting,
    InProcess,
    Complited
}