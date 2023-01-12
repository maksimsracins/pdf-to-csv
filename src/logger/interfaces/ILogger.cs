using System;
using System.Collections.Generic;
using System.Text;

namespace PdfToCsv.src.logger {
    interface ILogger {

        void Log(LogLevel level, string message, Exception exception = null);
        void Trace(string message);
        void LogInformation(string message);
        void LogWarning(string message);
        void LogError(Exception ex, string message);
        void LogCritical(Exception ex, string message); 
    }
}
