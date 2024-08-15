using DockerStatsParser.WindowFunctionsService;

int funcCount = 30000;
int tagsCount = 300;
int levelsCount = 8;
int componentsCount = 1500;

string parsedFileName = 
    $"{funcCount}f-" +
    $"{tagsCount}t-" +
    $"{levelsCount}l-" +
    $"{componentsCount}comp" +
    $"_logs.txt";
string generatedFilePath = Path.Combine(
    "..", "..", "..", "WindowFunctionsService", "Logs", "Gen1", parsedFileName);
string logs = File.ReadAllText(generatedFilePath);

var model = WindowFunctionsParserGod.ExtractLogsValues(logs);

string calculationulateFileName =
    $"{funcCount}f-" +
    $"{tagsCount}t-" +
    $"{levelsCount}l-" +
    $"{componentsCount}comp" +
    $"_calculation.txt";
string calculationFilePath = Path.Combine(
    "..", "..", "..", "WindowFunctionsService", "Logs", "Gen1", calculationulateFileName);
string calculationLogs = File.ReadAllText(calculationFilePath);

List<int> calculationTimes = WindowFunctionsParserGod.ExtractTimeValues(calculationLogs);

ReportCreator reportCreator = new(
    model,
    funcCount,
    tagsCount,
    levelsCount,
    componentsCount,
    calculationTimes);

string report = reportCreator.Create();

string reportFileName =
    $"{funcCount}f-" +
    $"{tagsCount}t-" +
    $"{levelsCount}l-" +
    $"{componentsCount}comp" +
    $"_report.txt";
string reportFilePath = Path.Combine(
    "..", "..", "..", "WindowFunctionsService", "Reports", "Gen1", reportFileName);
File.WriteAllText(reportFilePath, report);

Console.WriteLine(report);
