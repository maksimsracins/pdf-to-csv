using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PdfToCsv {
    class ReadPdf {

        static string targetDirectory = @"..\..\..\src\domain\data\pdf\";
        static string[] arrayOfFiles = Directory.GetFiles(targetDirectory);
        private List<string> listOfPdfLinks = GetAllPdfFromFolder();

        static public List<string> GetAllPdfFromFolder() {
            List<string> listOfPdfs = new List<string>();
            foreach (var item in arrayOfFiles)
                listOfPdfs.Add(item);

            return listOfPdfs;
        }

        public PdfReader GetPdfReader(int indexOfPdf, List<string> listOfPdfLinks) =>
            new PdfReader(listOfPdfLinks[indexOfPdf]);

        public string GetPdfFileName(int indexOfFile) =>
            Path.GetFileNameWithoutExtension(arrayOfFiles[indexOfFile]);

        public string GetPdfFilePath(int indexOfFile) =>
            Path.GetFileName(arrayOfFiles[indexOfFile]);

        public List<string> GetAllPdfFileNames() {
            List<string> listOfAllPdfFileNames = new List<string>();

            for (int indexOfPdfLink = 0; indexOfPdfLink < listOfPdfLinks.Count; indexOfPdfLink++) {
                listOfAllPdfFileNames.Add(Path.GetFileName(listOfPdfLinks[indexOfPdfLink]));
            }
            return listOfAllPdfFileNames;
        }
    }
}
