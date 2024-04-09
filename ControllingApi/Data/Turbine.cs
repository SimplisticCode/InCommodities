using System.ComponentModel.DataAnnotations;

namespace ControllingApi.Data;

public class Turbine(string name, int capacity, int productionCost)
{
    [Required]
    public readonly int Capacity = Math.Max(capacity, 0);

    [Required]
    public readonly int ProductionCost = Math.Max(productionCost, 0);

    [Required, StringLength(50), Key]
    public readonly string Name = name ?? throw new ArgumentNullException(nameof(name));
    private bool _running = false;


    public bool IsRunning => _running;

    public int GetCurrentProduction()
    {
        return _running ? Capacity : 0;
    }

    public Task Start()
    {
        _running = true;
        return Task.CompletedTask;
    }

    public Task Stop()
    {
        _running = false;
        return Task.CompletedTask;
    }
}