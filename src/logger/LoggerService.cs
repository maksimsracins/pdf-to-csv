using System;
using System.Collections.Generic;
using System.Text;

namespace PdfToCsv.src.logger {
    class LoggerService<T> : ILogger {

        private readonly IFileService fileService;
        private readonly string nameSpace;
        private readonly string currentTime = DateTime.Now.ToString();
        private short traceId = 1;

        public LoggerService(IFileService fileService) {
            this.fileService = fileService;
            this.nameSpace = typeof(T).FullName;
        }

        private string CastMessage(string message, LogLevel level, short traceId, Exception exception) {
            return exception != null
                ? $"{currentTime}|{traceId}|{nameSpace}|{level.ToString()}|{message}|{exception}"
                : $"{currentTime}|{traceId}|{nameSpace}|{level.ToString()}|{message}";
        } 

        public void Log(LogLevel level, string message, Exception exception = null) {
            var response = CastMessage(message, level, traceId, exception);
            traceId++;
            
            switch (level) {
                case LogLevel.Information:
                    LogInformation(response);
                    break;
                case LogLevel.Warning:
                    LogWarning(message);
                    break;
                case LogLevel.Error:
                    LogError(exception, response);
                    break;
                case LogLevel.Critical:
                    LogCritical(exception, response);
                    break;
            }
        }

        public void LogCritical(Exception ex, string message) {
            fileService.WriteLog(message, ex);
        }

        public void LogError(Exception ex, string message) {
            fileService.WriteLog(message, ex);
        }

        public void LogInformation(string message) {
            fileService.WriteLog(message);
        }

        public void LogWarning(string message) {
            fileService.WriteLog(message);
        }

        public void Trace(string message) {
            fileService.WriteLog(message);
        }
    }
}
