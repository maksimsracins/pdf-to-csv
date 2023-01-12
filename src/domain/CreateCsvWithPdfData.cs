using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PdfToCsv {
    class CreateCsvWithPdfData {

        private static string csvTargetDirectory = @"..\..\..\src\ui\data\csv\";
        private static string csvExtension = ".csv";

        public string CreateCsvFile(string nameOfPdf) {
            FileStream fileStream = File.Create($"{csvTargetDirectory}{nameOfPdf}{csvExtension}");
            fileStream.Dispose();

            return fileStream.Name;
        }
    }
}
