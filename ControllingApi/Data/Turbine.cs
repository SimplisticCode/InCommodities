using System.ComponentModel.DataAnnotations;
public class Turbine
{
    [Required]
    public int Capacity { get; set; }

    [Required]
    public int ProductionCost { get; set; }

    [Required, StringLength(50), Key]
    public string Name { get; set; }

    private bool running;


    public Turbine(string name, int capacity, int productionCost)
    {
        Name = name;
        Capacity = capacity;
        ProductionCost = productionCost;
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