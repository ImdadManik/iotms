using System;
using System.IO;

public static class cLogs
{
    private static readonly string logFilePath = "log.txt";

    public static void Log(string message)
    {
        // Format log message with timestamp
        string formattedMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}";

        // Write log message to file
        using (StreamWriter sw = File.AppendText(logFilePath))
        {
            sw.WriteLine(formattedMessage);
        }
    }
}
