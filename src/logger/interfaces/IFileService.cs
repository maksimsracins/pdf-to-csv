using System;
using System.Collections.Generic;
using System.Text;

namespace PdfToCsv.src.logger {
    interface IFileService {
        void WriteLog(string message, Exception ex = null);
        void ReadLogs();
    }
}