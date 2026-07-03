using System.Text.RegularExpressions;

namespace Log.Services
{
    public class LogService
    {
        private static readonly Regex LogRegex = new Regex(@"(?<date>\d{2}.\d{2}.\d{4}|\d{4}-\d{2}-\d{2})\s+(?<time>\d{2}:\d{2}:\d{2}.\d+)\W+(?<level>[A-Z]+)\s*\|*\d*(?:\|(?<caller>.+)\|)?\s*(?<message>.+)",
        RegexOptions.Compiled);

        private static readonly string[] LogLevels = ["INFO", "INFORMATION", "WARN", "WARNING", "ERROR", "DEBUG"];

        public void Format(string srcFilePath, string dstFilePath, string errorFilePath)
        {
            validateFiles(srcFilePath, dstFilePath, errorFilePath);
            try
            {
                // Open the input file for reading and create/overwrite the output file for writing
                using (StreamReader reader = new StreamReader(srcFilePath))
                using (StreamWriter writer = new StreamWriter(dstFilePath))
                using (StreamWriter errorWriter = new StreamWriter(errorFilePath))
                {
                    string? line;
                    // Read line-by-line until the end of the file is reached
                    while ((line = reader.ReadLine()) != null)
                    {
                        // Apply your custom transformation logic
                        string? transformedLine = TransformLogLine(line);

                        if (transformedLine is null)
                        {
                            errorWriter.WriteLine(line);
                        }

                        // Write the transformed string to the target file
                        writer.WriteLine(transformedLine);
                    }
                }

                Console.WriteLine("Log file processing completed successfully.");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"I/O Error occurred: {ex.Message}");
            }
        }

        private void validateFiles(string srcFilePath, string dstFilePath, string errorFilePath)
        {
            if (!File.Exists(srcFilePath))
            {
                Console.Error.WriteLine(srcFilePath + " not exists");
                throw new ArgumentException(srcFilePath + " not exists");
            }
            if (!File.Exists(dstFilePath))
            {
                throw new ArgumentException(dstFilePath + " not exists");
            }
            if (!File.Exists(errorFilePath))
            {
                throw new ArgumentException(errorFilePath + " not exists");
            }
        }

        public string? TransformLogLine(string line)
        {
            var match = LogRegex.Match(line);
            if (match.Success)
            {

                DateOnly date = DateOnly.Parse(match.Groups["date"].Value);
                string time = match.Groups["time"].Value;
                string level = ParseLogLevel(match.Groups["level"].Value);
                string caller = ParseCaller(match.Groups["caller"]?.Value);
                string message = match.Groups["message"].Value;
                return $"{date:dd-MM-yyyy}\t{time}\t{level}\t{caller}\t{message}";
            }
            return null;
        }

        private string ParseLogLevel(string value)
        {
            if (LogLevels.Contains(value))
            {
                if (value.Equals("INFORMATION")) return "INFO";
                if (value.Equals("WARNING")) return "WARN";
                return value;
            }
            throw new ArgumentException(value + " is not a valid log level, valid values are: " + string.Join(",", LogLevels));
        }

        private string ParseCaller(string? value)
        {
            if (value is not null && value.Length > 0)
            {
                return value;
            }
            return "DEFAULT";
        }
    }
}
