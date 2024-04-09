public record TurbineReport(
    List<TurbineDTO> Turbines,
    int TargetProduction,
    int CurrentProduction,
    int PriceLimit)
{
    public List<TurbineDTO> Turbines { get; init; } = Turbines ?? throw new ArgumentNullException(nameof(Turbines));

    public int TargetProduction { get; init; } = TargetProduction > 0 ? TargetProduction
        : throw new ArgumentOutOfRangeException(nameof(TargetProduction), "Must be positive");

    public int CurrentProduction { get; init; } = CurrentProduction > 0 ? CurrentProduction
        : throw new ArgumentOutOfRangeException(nameof(CurrentProduction), "Must be positive");

    public int PriceLimit { get; init; } = PriceLimit > 0 ? PriceLimit
        : throw new ArgumentOutOfRangeException(nameof(PriceLimit), "Must be positive");

    public int ProductionDifference => TargetProduction - CurrentProduction;
}