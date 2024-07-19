using SqlScriptGenerator.WindowFunctionsService;
using System.Text;

namespace DockerStatsParser.WindowFunctionsService;

public static class WindowFunctionsParserGod
{
    public static void Start(string statsLogs)
    {
        StatsModel model = ExtractValues(statsLogs);

        Console.WriteLine(statsLogs);

    }

    public static StatsModel ExtractValues(string statsLogs)
    {
        StatsModel statsModel = new ();

        string separator = 
            """CONTAINER ID   NAME                       CPU %     MEM USAGE / LIMIT     MEM %     NET I/O           BLOCK I/O   PIDS """;

        var onlyLogs = statsLogs.Split(separator);

        string firstLogString = onlyLogs[0];

        statsModel.ContainerId = firstLogString.Substring(0, 11);

        int startIndex = 15;
        int endIndex = firstLogString.IndexOf(' ', startIndex);
        statsModel.Name = firstLogString.Substring(startIndex, endIndex - 1);

        Parallel.ForEach(onlyLogs, x =>
        {
            
        });

        return statsModel;
    }
}
