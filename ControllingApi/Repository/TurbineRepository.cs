using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


/// <summary>
/// Represents a DAL for managing turbines - using an in-memory list (Could be replaced with a database).
/// </summary>
public class TurbineRepository : ITurbineRepository
{
    // List of turbines to manage
    private List<Turbine> turbines;


    /// <summary>
    /// Initializes a new instance of the <see cref="TurbineRepository"/> class.
    /// </summary>
    public TurbineRepository()
    {
        turbines = new List<Turbine>(){
            new Turbine("A", 2, 15),
            new Turbine("B", 2, 5),
            new Turbine("C", 6, 5),
            new Turbine("D", 6, 5),
            new Turbine("E", 5, 3)
        };
    }

    /// <summary>
    /// Stops all turbines.
    /// </summary>
    /// <returns></returns>
    public async Task StopAllTurbines()
    {
        foreach (var turbine in turbines)
        {
            if (turbine.isRunning)
                await turbine.Stop();
        }
    }

    /// <summary>
    /// Starts all turbines.
    /// </summary>
    /// <returns></returns>
    public async Task StartAllTurbines()
    {
        foreach (var turbine in turbines)
        {
            if (!turbine.isRunning)
                await turbine.Start();
        }
    }

    /// <summary>
    /// Starts a turbine with the given name.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Task StartTurbine(string name)
    {
        var optionType = FindTurbine(name);
        if (optionType.HasValue)
        {
            Console.WriteLine($"Starting turbine {name}");
            optionType.Value.Start();
        }
        return Task.CompletedTask;
    }

    /// <summary>
    /// Stops a turbine with the given name.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Task StopTurbine(string name)
    {
        var optionType = FindTurbine(name);
        if (optionType.HasValue)
        {
            Console.WriteLine($"Stopping turbine {name}");
            optionType.Value.Stop();
        }
        return Task.CompletedTask;
    }


    /// <summary>
    /// Adds a turbine to the repository.
    /// </summary>
    /// <param name="turbine">The turbine to add.</param>
    public async Task AddTurbine(Turbine turbine)
    {
        await Task.Run(() => turbines.Add(turbine));
    }

    /// <summary>
    /// Removes a turbine from the repository.
    /// </summary>
    /// <param name="name">The name of the turbine to remove.</param>
    public Task RemoveTurbine(string name)
    {
        var optionType = FindTurbine(name);
        if (optionType.HasValue)
        {
            turbines.Remove(optionType.Value);
        }
        return Task.CompletedTask;
    }

    public Task<List<Turbine>> TurbinesWithCostLessThan(int cost)
    {
        return Task.FromResult(turbines.FindAll(t => t.ProductionCost < cost));
    }

    public Task<List<Turbine>> TurbinesWithCapacityLessThan(int capacity)
    {
        return Task.FromResult(turbines.FindAll(t => t.Capacity < capacity));
    }

    public Task<List<Turbine>> GetAllTurbines()
    {
        return Task.FromResult(turbines);
    }

    private Option<Turbine> FindTurbine(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return Option<Turbine>.None<Turbine>();
        }
        var turbine = turbines.FirstOrDefault(t => t.Name == name);
        return turbine != null ? Option<Turbine>.Some(turbine) : Option<Turbine>.None<Turbine>();
    }

}