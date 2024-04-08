public record TurbineReport(
                    List<TurbineDTO> Turbines,
                    int TargetProduction,
                    int CurrentProduction,
                    int PriceLimit)
{
    int ProductionDifference => TargetProduction - CurrentProduction;
}