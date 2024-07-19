using DockerStatsParser.WindowFunctionsService;

string parsedFileName = "logs.txt";
string generatedFilePath = Path.Combine("..", "..", "..", "WindowFunctionsService", "Logs", "Gen1", parsedFileName);

string logs = File.ReadAllText(generatedFilePath);

WindowFunctionsParserGod.Start(logs);