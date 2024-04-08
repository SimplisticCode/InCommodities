using System.Threading.Tasks;

public interface ITurbineManager
{
    Task IncreaseCapacity(int amount);
    Task DecreaseCapacity(int amount);
    Task SetMarketPrice(int price);
    Task<TurbineReport> GetTurbineReportAsync();
}