namespace MyTrainer.Models;

public class Exercise 
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string MuscleGroup { get; set; } = string.Empty;
    public string Url { get; set; } = "#";
    public int Index { get; set; }
    public int Reps { get; set; }
    public int SetCount { get; set; }
    public List<Set>? Sets { get; set; }
    public bool IsComplited { get; set; } = false;
    public Guid? WorkoutId { get; set; }
    public Workout Workout { get; set; }

    public override bool Equals(object? obj)
    {
        return obj is Exercise exercise &&
               Id.Equals(exercise.Id) &&
               Name == exercise.Name &&
               Description == exercise.Description &&
               MuscleGroup == exercise.MuscleGroup;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, Description, MuscleGroup);
    }
}
