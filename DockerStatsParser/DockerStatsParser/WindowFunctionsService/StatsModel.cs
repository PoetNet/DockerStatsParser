namespace SqlScriptGenerator.WindowFunctionsService;

public class StatsModel
{
    public string ContainerId { get; set; } = "955af9b7cf02";
    public string Name { get; set; } = "window-functions-service";
    public List<double> CPUPercentage { get; set; } = new();
    public List<double> MemUsageGib { get; set; } = new();
    public List<double> MemUsagePercentage { get; set; } = new();
    public List<double> NetIOGb { get; set; } = new();
    public List<double> BlockIO { get; set; } = new();
    public List<int> Pids { get; set; } = new();

}