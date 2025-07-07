using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace boadaDP3.Services
{
    public class LogService
    {
        private readonly string _logFilePath;

        public LogService()
        {
            _logFilePath = Path.Combine(FileSystem.AppDataDirectory, "Logs_Boada.txt");
        }

        public async Task WriteLogAsync(string dispositivo)
        {
            var logEntry = $"Se incluyó el registro [{dispositivo}] el {DateTime.Now:dd/MM/yyyy HH:mm}";

            try
            {
                await File.AppendAllTextAsync(_logFilePath, logEntry + Environment.NewLine);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error escribiendo log: {ex.Message}");
            }
        }

        public async Task<List<string>> GetLogsAsync()
        {
            try
            {
                if (!File.Exists(_logFilePath))
                    return new List<string>();

                var content = await File.ReadAllTextAsync(_logFilePath);
                return content.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).ToList();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error leyendo logs: {ex.Message}");
                return new List<string>();
            }
        }
    }
}