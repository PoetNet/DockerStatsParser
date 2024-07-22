using SqlScriptGenerator.WindowFunctionsService;
using System.Globalization;
using System.Text;

namespace DockerStatsParser.WindowFunctionsService;

public static class WindowFunctionsParserGod
{
    public static StatsModel ExtractValues(string statsLogs)
    {
        StatsModel statsModel = new StatsModel();

        string separator =
            "CONTAINER ID   NAME                       CPU %     MEM USAGE / LIMIT     MEM %     NET I/O           BLOCK I/O   PIDS ";

        var logLines = statsLogs.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);

        string firstLogString = logLines[0];

        statsModel.ContainerId = firstLogString.Substring(0, 11);

        int startIndex = 15;
        int endIndex = firstLogString.IndexOf(' ', startIndex);
        statsModel.Name = firstLogString.Substring(startIndex, endIndex - startIndex).Trim();
        foreach (var logLine in logLines)
        {
            var parts = logLine.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 14) continue;

            string targetPart = parts[parts.Length - 12];
            // CPU %
            if (double.TryParse(targetPart.TrimEnd('%'), NumberStyles.Any, CultureInfo.InvariantCulture, out double cpu))
            {
                statsModel.CPUPercentage.Add(cpu);
            }

            targetPart = parts[5];
            // MEM Usage (GiB)
            if (double.TryParse(targetPart.Substring(0, targetPart.Length - 3), NumberStyles.Any, CultureInfo.InvariantCulture, out double memUsageGib))
            {
                statsModel.MemUsageGib.Add(memUsageGib);
            }

            targetPart = parts[6];
            // MEM %
            if (
                double.TryParse(targetPart.TrimEnd('%'), 
                NumberStyles.Any, 
                CultureInfo.InvariantCulture, 
                out double memPercentage))
            {
                statsModel.MemUsagePercentage.Add(memPercentage);
            }

            targetPart = parts[7];
            // NET I/O (GB)
            if (double.TryParse(
                targetPart.Substring(0, targetPart.Length - 2),
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out double netIoGb))
            {
                statsModel.NetIOGb.Add(netIoGb);
            }

            targetPart = parts[13];
            // PIDs
            if (int.TryParse(targetPart, out int pids))
            {
                statsModel.Pids.Add(pids);
            }
        }

        return statsModel;
    }
}
