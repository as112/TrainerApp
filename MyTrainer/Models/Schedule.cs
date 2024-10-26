namespace MyTrainer.Models;

public class Schedule
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateOnly Date { get; set; }
    public Guid WorkoutId { get; set; }
}
