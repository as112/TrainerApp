using Microsoft.EntityFrameworkCore;
using MyTrainer.Models;

namespace MyTrainer.Data;

public class TrainingAppContext : DbContext
{
    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<Set> Sets { get; set; }
    public DbSet<Workout> Workouts { get; set; }
    public DbSet<Schedule> Schedules { get; set; }

    public TrainingAppContext()
    {
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    
    protected override async void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var dbPath = string.Empty;
        var documentsPath = string.Empty;
        if (DeviceInfo.Platform == DevicePlatform.Android)
        {
            documentsPath = await GetExternalDocumentsPathAsync();
        }
        else if(DeviceInfo.Platform == DevicePlatform.WinUI)
        {
            documentsPath = "D:\\";
        }

        dbPath = Path.Combine(documentsPath, "trainingapp.db");
        optionsBuilder.UseSqlite($"Filename={dbPath}");
    }
    private async Task<string> GetExternalDocumentsPathAsync()
    {
        var status = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();

        if (status != PermissionStatus.Granted)
        {
            status = await Permissions.RequestAsync<Permissions.StorageWrite>();
            if (status != PermissionStatus.Granted)
            {
                throw new UnauthorizedAccessException("Storage write permission not granted.");
            }
        }

        var documentsPath = string.Empty;

#if ANDROID
        documentsPath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDocuments).AbsolutePath;
#endif

        return documentsPath;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Workout>()
            .HasMany(w => w.Exercises)
            .WithOne(e => e.Workout)
            .HasForeignKey(e => e.WorkoutId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Exercise>()
            .HasMany(w => w.Sets)
            .WithOne(e => e.Exercise)
            .HasForeignKey(w => w.ExerciseId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Exercise>()
            .HasData(
                new Exercise
                {
                    Name = "Жим лежа в наклонной скамье (в машине Смитта)",
                    Description = "угол скамьи 30, на верх груди",
                    MuscleGroup = "Грудь",
                    Url = "https://www.lyfta.app/ru/exercise/smith-incline-bench-press-6uh"
                },
                new Exercise
                {
                    Name = "Жим в Хамере",
                    Description = "на верх груди",
                    MuscleGroup = "Грудь",
                    Url = "https://www.lyfta.app/ru/exercise/lever-chest-press-0t"
                },
                new Exercise
                {
                    Name = "Сведение рук (бабочка)",
                    Description = "на верх груди",
                    MuscleGroup = "Грудь",
                    Url = "https://www.lyfta.app/ru/exercise/lever-seated-fly-18"
                },
                new Exercise
                {
                    Name = "Жим лежа в гризонтальной скамье",
                    Description = "можно в Смитте",
                    MuscleGroup = "Грудь",
                    Url = "https://www.lyfta.app/ru/exercise/bench-press-7"
                },
                new Exercise
                {
                    Name = "Жим гантелей лежа",
                    Description = "",
                    MuscleGroup = "Грудь",
                    Url = "https://www.lyfta.app/ru/exercise/dumbbell-bench-press-84"
                },
                new Exercise
                {
                    Name = "Подъемы по 1 гантеле перед собой",
                    Description = "на серднюю дельту",
                    MuscleGroup = "Плечи",
                    Url = "https://www.lyfta.app/ru/exercise/standing-alternate-raise-05"
                },
                new Exercise
                {
                    Name = "Жим в блочном тренажере горизонтальным хватом",
                    Description = "на переднюю дельту",
                    MuscleGroup = "Плечи",
                    Url = "https://www.lyfta.app/ru/exercise/lever-seated-shoulder-press-fg"
                },
                new Exercise
                {
                    Name = "Обратная бабочка",
                    Description = "на заднюю дельту",
                    MuscleGroup = "Плечи",
                    Url = "https://www.lyfta.app/ru/exercise/lever-seated-reverse-fly-12"
                },
                new Exercise
                {
                    Name = "Боковой подъем гантели",
                    Description = "на серднюю дельту",
                    MuscleGroup = "Плечи",
                    Url = "https://www.lyfta.app/ru/exercise/dumbbell-lateral-raise-6iq"
                },
                new Exercise
                {
                    Name = "Махи 2-х гантель перед собой одновременно",
                    Description = "на переднюю дельту",
                    MuscleGroup = "Плечи",
                    Url = "https://www.lyfta.app/ru/exercise/front-raise-8u"
                },
                new Exercise
                {
                    Name = "Протяжка к груди в Смите широким хватом",
                    Description = "на серднюю дельту",
                    MuscleGroup = "Плечи",
                    Url = "https://www.lyfta.app/ru/exercise/smith-upright-row-24"
                },
                new Exercise
                {
                    Name = "Горизонтальная Тяга в треражере",
                    Description = "Акцент на заднюю дельту, локти выше",
                    MuscleGroup = "Плечи",
                    Url = "https://www.lyfta.app/ru/exercise/lever-seated-row-4s"
                },
                new Exercise
                {
                    Name = "Протяжка к груди в Смите узким хватом",
                    Description = "на серднюю дельту",
                    MuscleGroup = "Плечи",
                    Url = "https://www.lyfta.app/ru/exercise/smith-upright-row-24"
                },
                new Exercise
                {
                    Name = "Отведения рук в стороны (бабочка)",
                    Description = "на серднюю дельту",
                    MuscleGroup = "Плечи",
                    Url = ""
                },
                new Exercise
                {
                    Name = "Боковой подъем гантелей в наклоне",
                    Description = "на заднюю дельту",
                    MuscleGroup = "Плечи",
                    Url = "https://www.lyfta.app/ru/exercise/rear-fly-9n"
                },
                new Exercise
                {
                    Name = "Французский жим",
                    Description = "",
                    MuscleGroup = "Трицепс",
                    Url = "https://www.lyfta.app/ru/exercise/barbell-lying-triceps-extension-skull-crusher-60e"
                },
                new Exercise
                {
                    Name = "Разгибание одной руки с верхнего блока прямым хватом",
                    Description = "В кроссовере",
                    MuscleGroup = "Трицепс",
                    Url = "https://www.lyfta.app/ru/exercise/cable-one-arm-tricep-pushdown-7ga"
                },
                new Exercise
                {
                    Name = "Отжимания узким хватом на брусьях",
                    Description = "",
                    MuscleGroup = "Трицепс",
                    Url = "https://www.lyfta.app/ru/exercise/assisted-triceps-dip--69z"
                },
                new Exercise
                {
                    Name = "Разгибание одной руки в наклоне с опорой на скамью",
                    Description = "",
                    MuscleGroup = "Трицепс",
                    Url = "https://www.lyfta.app/ru/exercise/dumbbell-kickback-6ip"
                },
                new Exercise
                {
                    Name = "Тяга вертикального блока узким хватом",
                    Description = "",
                    MuscleGroup = "Спина",
                    Url = "https://www.lyfta.app/ru/exercise/cable-lateral-pulldown-with-v-bar-7e7"
                },
                new Exercise
                {
                    Name = "Тяга горизонтального блока узким хватом",
                    Description = "",
                    MuscleGroup = "Спина",
                    Url = "https://www.lyfta.app/ru/exercise/cable-seated-row-with-v-bar-85g"
                },
                new Exercise
                {
                    Name = "Тяга вертикального блока параллельным хватом",
                    Description = "",
                    MuscleGroup = "Спина",
                    Url = "https://www.lyfta.app/ru/exercise/cable-wide-neutral-grip-pulldown-91u"
                },
                new Exercise
                {
                    Name = "Рычажная тяга по одной руке параллельным хватом",
                    Description = "",
                    MuscleGroup = "Спина",
                    Url = "https://www.lyfta.app/ru/exercise/lever-alternating-narrow-grip-seated-row-0r"
                },
                new Exercise
                {
                    Name = "Тяга гантелей к груди стоя в наклоне",
                    Description = "Одной рукой",
                    MuscleGroup = "Спина",
                    Url = "https://www.lyfta.app/ru/exercise/one-arm-row-3u"
                },
                new Exercise
                {
                    Name = "Жим в платформе",
                    Description = "45 градусов",
                    MuscleGroup = "Ноги",
                    Url = "https://www.lyfta.app/ru/exercise/sled-45-leg-press-1o"
                },
                new Exercise
                {
                    Name = "Сгибания ног лежа",
                    Description = "",
                    MuscleGroup = "Ноги",
                    Url = "https://www.lyfta.app/ru/exercise/lever-lying-leg-curl-6pq"
                },
                new Exercise
                {
                    Name = "Ягодичный мост",
                    Description = "Акцент на ягодицы",
                    MuscleGroup = "Ноги",
                    Url = "https://www.lyfta.app/ru/exercise/dumbbell-glute-bridge-8nz"
                },
                new Exercise
                {
                    Name = "Гиперэкстензия",
                    Description = "",
                    MuscleGroup = "Ноги",
                    Url = "https://www.lyfta.app/ru/exercise/hyperextension-h9"
                },
                new Exercise
                {
                    Name = "Сгибания в тренажере",
                    Description = "",
                    MuscleGroup = "Пресс",
                    Url = "https://www.lyfta.app/ru/exercise/lever-total-abdominal-crunch-7kh"
                },
                new Exercise
                {
                    Name = "Сгибания 2-х рук в тренажере Скотта",
                    Description = "",
                    MuscleGroup = "Бицепс",
                    Url = "https://www.lyfta.app/ru/exercise/seated-preacher-curl-9z"
                },
                new Exercise
                {
                    Name = "Сгибание рук с упором в стену (гантели)",
                    Description = "",
                    MuscleGroup = "Бицепс",
                    Url = "https://www.lyfta.app/ru/exercise/dumbbell-biceps-curl-6hm"
                }
            );
    }
}
