using System.Collections.Generic;
using PdfToCsv.src;
using PdfToCsv.src.logger;

namespace PdfToCsv {
    class Program {
        static void Main(string[] args) {
            Program.Run();
        }

        static void Run() {

            IFileService fileService = new FileService();
            ILogger logger = new LoggerService<DataTransferService>(fileService);

            //files
            CreateCsvWithPdfData createCSV = new CreateCsvWithPdfData();
            ReadPdf readPdf = new ReadPdf();

            //rules
            ProcessRule processRule = new ProcessRule();
            List<string> headerRules = new List<string>();
            GetRules rules = new GetRules(headerRules, processRule);


            TextModificationService textModification = new TextModificationService();
            DataTransferService dt = new DataTransferService(createCSV, readPdf, rules, textModification, fileService, logger);
            dt.transferData();
        }
    }
}
