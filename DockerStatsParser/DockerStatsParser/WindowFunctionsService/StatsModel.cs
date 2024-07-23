namespace SqlScriptGenerator.WindowFunctionsService;

public class StatsModel
{
    public string Name { get; set; } = "window-functions-service";
    public List<double> CPUPercentage { get; set; } = new();
    public List<double> MemUsageMib { get; set; } = new();
    public List<double> MemUsagePercentage { get; set; } = new();
    public List<double> NetIGb { get; set; } = new();
    public List<double> NetOGb { get; set; } = new();
    public List<double> BlockI { get; set; } = new();
    public string BlockIType { get; set; } = "B";
    //public string BlockIType { get; set; } = "kb";
    public List<double> BlockO { get; set; } = new();
    public string BlockOType { get; set; } = "B";
    public List<int> Pids { get; set; } = new();
}