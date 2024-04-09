using System.ComponentModel.DataAnnotations;
public class Turbine
{
    [Required]
    public readonly int Capacity;

    [Required]
    public readonly int ProductionCost;

    [Required, StringLength(50), Key]
    public readonly string Name;
    private bool running;


    public Turbine(string name, int capacity, int productionCost)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Capacity = Math.Max(capacity, 0);
        ProductionCost = Math.Max(productionCost, 0);
        running = false;
    }

    public bool isRunning => running;

    public int GetCurrentProduction()
    {
        return running ? Capacity : 0;
    }

    public Task Start()
    {
        running = true;
        return Task.CompletedTask;
    }

    public Task Stop()
    {
        running = false;
        return Task.CompletedTask;
    }
}