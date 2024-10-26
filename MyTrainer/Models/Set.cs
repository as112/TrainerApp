namespace MyTrainer.Models;

public class Set
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ExerciseId { get; set; }
    public Exercise? Exercise { get; set; }
    public int Index { get; set; }
    public double ActualWeight { get; set; }
    public int ActualReps { get; set; }
}
