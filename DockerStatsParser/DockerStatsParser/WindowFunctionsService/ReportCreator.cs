using SqlScriptGenerator.WindowFunctionsService;

namespace DockerStatsParser.WindowFunctionsService;

public class ReportCreator(
        StatsModel model,
        int funcCount,
        int tagsCount,
        int levelsCount,
        int componentsCount)
{
    public string Create()
    {
        return
            $"""            
            ContainerId: {model.ContainerId}
            Name:        {model.Name}

            FunctionsCount:   {funcCount}
            TagsCount:        {tagsCount}
            LevelsCount:      {levelsCount}
            ComponentsCount:  {componentsCount}

            AVG:

            CPUPercentage:      {Math.Round(model.CPUPercentage.Average(), 2)}
            MemUsageGib:        {Math.Round(model.MemUsageGib.Average(), 2)}
            MemUsagePercentage: {Math.Round(model.MemUsagePercentage.Average(), 2)}
            NetIOGb:            {Math.Round(model.NetIOGb.Average(), 2)}
            Pids:               {Math.Round(model.Pids.Average(), 2)}

            MIN:

            CPUPercentage:      {Math.Round(model.CPUPercentage.Min(), 2)}
            MemUsageGib:        {Math.Round(model.MemUsageGib.Min(), 2)}
            MemUsagePercentage: {Math.Round(model.MemUsagePercentage.Min(), 2)}
            NetIOGb:            {Math.Round(model.NetIOGb.Min(), 2)}
            Pids:               {model.Pids.Min()}

            MAX:

            CPUPercentage:      {Math.Round(model.CPUPercentage.Max(), 2)}
            MemUsageGib:        {Math.Round(model.MemUsageGib.Max(), 2)}
            MemUsagePercentage: {Math.Round(model.MemUsagePercentage.Max(), 2)}
            NetIOGb:            {Math.Round(model.NetIOGb.Max(), 2)}
            Pids:               {model.Pids.Max()}

            String of logs processed: {model.CPUPercentage.Count()}
            """;
    }
}
