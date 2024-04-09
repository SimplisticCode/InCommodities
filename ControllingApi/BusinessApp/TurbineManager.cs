using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControllingApi.Data;
using ControllingApi.Repository;
using Microsoft.Extensions.Logging;

public class TurbineManager : ITurbineManager
{
    // Market price of electricity
    private int _marketPrice;
    // Needed capacity of the turbines
    private int _needed_capacity;
    // Produced capacity of the turbines
    private int _produced_capacity;

    private ITurbineRepository _turbineRepository;
    private ILogger<TurbineManager> _logger;

    public TurbineManager(ITurbineRepository turbineRepository, ILogger<TurbineManager> logger)
    {
        _turbineRepository = turbineRepository;
        _logger = logger;
        _marketPrice = 0;
        _needed_capacity = 0;
        _produced_capacity = 0;
    }

    public async Task<TurbineReport> GetTurbineReportAsync()
    {
        var turbines = await _turbineRepository.GetAllTurbines();
        var turbineDTOs = turbines.Select(t => new TurbineDto(t.Name, t.GetCurrentProduction())).ToList();
        return new TurbineReport(turbineDTOs, _needed_capacity, _produced_capacity, _marketPrice);
    }

    public async Task ControlAsync()
    {
        // Stop all turbines - to start from scratch (not optimal in real life, but for simplicity here)
        await _turbineRepository.StopAllTurbines();
        _produced_capacity = 0;
        // Get all turbines less than the given cost
        List<Turbine> turbinesWithCostLessThanMarketPrice = await _turbineRepository.TurbinesWithCostLessThan(_marketPrice);
        // Sort the turbines by price in ascending order - the cheapest first
        turbinesWithCostLessThanMarketPrice.Sort((t1, t2) => t1.ProductionCost.CompareTo(t2.ProductionCost));
        // Start turbines until the needed capacity is reached or there are no more turbines (not guaranteed to reach the needed capacity)
        foreach (Turbine turbine in turbinesWithCostLessThanMarketPrice)
        {
            if (_produced_capacity + turbine.Capacity <= _needed_capacity)
            {
                await turbine.Start();
                _produced_capacity += turbine.Capacity;
            }
        }
    }

    public async Task IncreaseCapacity(int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Amount cannot be negative");
        }
        _needed_capacity += amount;
        _logger.LogInformation($"Increased needed capacity to {_needed_capacity}");
        await ControlAsync();
    }

    public async Task DecreaseCapacity(int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Amount cannot be negative");
        }
        _needed_capacity = Math.Max(0, _needed_capacity - amount); // The capacity cannot be negative
        _logger.LogInformation($"Decreased needed capacity to {_needed_capacity}");
        await ControlAsync();
    }

    public async Task SetMarketPrice(int price)
    {
        if (price < 0)
        {
            throw new ArgumentException("Price cannot be negative");
        }
        if (price > 1000)
        {
            throw new ArgumentException("Price seems unreasonably high");
        }
        _marketPrice = price;
        await ControlAsync();
    }
}