using System.Collections.Generic;

public interface ITurbineRepository
{
    Task StopAllTurbines();
    Task StartTurbine(string name);
    Task StopTurbine(string name);
    Task AddTurbine(Turbine turbine);
    Task RemoveTurbine(string name);
    Task<List<Turbine>> TurbinesWithCostLessThan(int cost);
    Task<List<Turbine>> TurbinesWithCapacityLessThan(int capacity);
    Task<List<Turbine>>  GetAllTurbines();
}
