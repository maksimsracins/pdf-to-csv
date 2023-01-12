using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PdfToCsv.src.logger {
    class FileService : IFileService {
        private const string FILE_PATH = @"..\..\..\src\logger\data\logs\logs.txt";

        public async void WriteLog(string message, Exception ex) {
            using (StreamWriter sw = new StreamWriter(FILE_PATH, true, System.Text.Encoding.Default)) {
                await sw.WriteLineAsync(message);
            }
        }

        public async void ReadLogs() {
            using(StreamReader sr = new StreamReader(FILE_PATH)) {
                Console.WriteLine(await sr.ReadToEndAsync());
            }
        }
    }
}
