using SqlScriptGenerator.WindowFunctionsService;
using System.Globalization;
using System.Text.RegularExpressions;

namespace DockerStatsParser.WindowFunctionsService;

public static class WindowFunctionsParserGod
{
    public static StatsModel ExtractLogsValues(string statsLogs)
    {
        StatsModel statsModel = new StatsModel();

        string separatorPattern = @"CONTAINER\s+ID\s+NAME\s+CPU\s+%\s+MEM\s+USAGE\s+/\s+LIMIT\s+MEM\s+%\s+NET\s+I/O\s+BLOCK\s+I/O\s+PIDS";
        Regex separatorRegex = new Regex(separatorPattern, RegexOptions.Compiled);

        var logLines = separatorRegex.Split(statsLogs).Where(line => !string.IsNullOrWhiteSpace(line)).ToArray();        
        
        foreach (var logLine in logLines)
        {
            string currentLogLine = logLine.Trim();
            var parts = currentLogLine.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 14) continue;

            string targetPart = parts[parts.Length - 12];
            // CPU %
            if (double.TryParse(targetPart.TrimEnd('%'), NumberStyles.Any, CultureInfo.InvariantCulture, out double cpu))
            {
                statsModel.CPUPercentage.Add(cpu);
            }

            targetPart = parts[3];
            // MEM Usage (MiB)
            if (double.TryParse(targetPart.Substring(0, targetPart.Length - 3), NumberStyles.Any, CultureInfo.InvariantCulture, out double memUsageGib))
            {
                if (parts[3].EndsWith("GiB"))
                {
                    memUsageGib *= 1024;
                }
                statsModel.MemUsageMib.Add(memUsageGib);
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

            // NET I/O (GB)
            targetPart = parts[7];
            if (double.TryParse(
                targetPart.Substring(0, targetPart.Length - 2),
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out double netIGb))
            {
                if (parts[7].EndsWith("GB"))
                {
                    netIGb *= 1024;
                }

                statsModel.NetIGb.Add(netIGb);
            }

            targetPart = parts[9];
            if (double.TryParse(
                targetPart.Substring(0, targetPart.Length - 2),
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out double netOGb))
            {
                if (parts[9].EndsWith("GB"))
                {
                    netOGb *= 1024;
                }

                statsModel.NetOGb.Add(netOGb);
            }

            // BLOCK I/O
            targetPart = parts[10];
            if (int.TryParse(
                targetPart.Substring(0, targetPart.Length - 1),
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out int i))
            {
                statsModel.BlockI.Add(i);
            }

            targetPart = parts[12];
            if (int.TryParse(
                targetPart.Substring(0, targetPart.Length - 1),
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out int o))
            {
                statsModel.BlockO.Add(o);
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
    public static List<int> ExtractTimeValues(string input)
    {
        List<int> timeValues = new List<int>();

        MatchCollection matches = Regex.Matches(input, @"\b\d+\s*ms\b");

        foreach (Match match in matches)
        {
            string value = match.Value.Replace("ms", "").Trim();
            if (int.TryParse(value, out int time))
            {
                timeValues.Add(time);
            }
        }

        return timeValues;
    }

    public static (int NumericPart, string StringPart) ExtractParts(string input)
    {
        Match match = Regex.Match(input, @"^\d+");

        if (match.Success)
        {
            string numericPartStr = match.Groups[1].Value;
            string stringPart = match.Groups[2].Value;

            if (int.TryParse(numericPartStr.TrimEnd('}').TrimStart('{'), out int numericPart))
            {
                return (numericPart, stringPart);
            }
        }

        return (0, input);
    }
}
