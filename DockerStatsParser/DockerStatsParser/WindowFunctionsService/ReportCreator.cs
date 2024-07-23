using SqlScriptGenerator.WindowFunctionsService;

namespace DockerStatsParser.WindowFunctionsService;

public class ReportCreator(
        StatsModel model,
        int funcCount,
        int tagsCount,
        int levelsCount,
        int componentsCount,
        List<int> calculationTimes)
{
    private readonly string MemUsageMeasurement = "MiB";
    private readonly string NetIOMeasurement = "MB";
    private readonly string BlockIMeasurement = "B";
    private readonly string BlockOMeasurement = "B";

    public string Create()
    {
        return
            $"""            
            Name:        {model.Name}

            FunctionsCount:   {funcCount}
            TagsCount:        {tagsCount}
            LevelsCount:      {levelsCount}
            ComponentsCount:  {componentsCount}

            AVG:

            Calculation time, ms:{Math.Round(calculationTimes.Average(), 2)}

            CPU Percentage:      {Math.Round(model.CPUPercentage.Average(), 2)}
            MemUsage {MemUsageMeasurement}:        {Math.Round(model.MemUsageMib.Average(), 3)}
            MemUsage Percentage: {Math.Round(model.MemUsagePercentage.Average(), 2)}
            NetI {NetIOMeasurement}:             {Math.Round(model.NetIGb.Average(), 2)}
            NetO {NetIOMeasurement}:             {Math.Round(model.NetOGb.Average(), 2)}
            BlockI {BlockIMeasurement}:            {Math.Round(model.BlockI.Average(), 2)}
            BlockO {BlockOMeasurement}:            {Math.Round(model.BlockO.Average(), 2)}
            Pids:                {Math.Round(model.Pids.Average(), 2)}

            MIN:

            Calculation time, ms:{calculationTimes.Min()}
            
            CPU Percentage:      {Math.Round(model.CPUPercentage.Min(), 2)}
            MemUsage {MemUsageMeasurement}:        {Math.Round(model.MemUsageMib.Min(), 3)}
            MemUsage Percentage: {Math.Round(model.MemUsagePercentage.Min(), 2)}
            NetI {NetIOMeasurement}:             {Math.Round(model.NetIGb.Min(), 2)}
            NetO {NetIOMeasurement}:             {Math.Round(model.NetOGb.Min(), 2)}
            BlockI {BlockIMeasurement}:            {Math.Round(model.BlockI.Min(), 2)}
            BlockO {BlockOMeasurement}:            {Math.Round(model.BlockO.Min(), 2)}
            Pids:                {model.Pids.Min()}

            MAX:

            Calculation time, ms:{calculationTimes.Max()}
            
            CPU Percentage:      {Math.Round(model.CPUPercentage.Max(), 2)}
            MemUsage {MemUsageMeasurement}:        {Math.Round(model.MemUsageMib.Max(), 2)}
            MemUsage Percentage: {Math.Round(model.MemUsagePercentage.Max(), 2)}
            NetI {NetIOMeasurement}:             {Math.Round(model.NetIGb.Max(), 2)}
            NetO {NetIOMeasurement}:             {Math.Round(model.NetOGb.Max(), 2)}
            BlockI {BlockIMeasurement}:            {Math.Round(model.BlockI.Max(), 2)}
            BlockO {BlockOMeasurement}:            {Math.Round(model.BlockO.Max(), 2)}
            Pids:                {model.Pids.Max()}

            String of logs processed: {model.CPUPercentage.Count()}
            """;
    }
}
