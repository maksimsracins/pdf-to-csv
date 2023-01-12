using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace PdfToCsv {
    class TextModificationService {

        string fileWithValuesToDelete = @"..\..\..\src\rules\data\delete_rules\to_delete.txt";

        public string FormateLine(string lineToModify) {

            //replace more than 2 tabs to 1 tab
            lineToModify = Regex.Replace(lineToModify, @"\s+", " ");
            if (lineToModify.StartsWith(" ")) {
                lineToModify = lineToModify.TrimStart();
            }

            //remove everything based on the file
            using (StreamReader stream = new StreamReader(fileWithValuesToDelete)) {
                while (!stream.EndOfStream) {
                    var line = stream.ReadLine().ToLower();
                    var values = line.Split(',');
                    foreach (var item in values) {
                        if (lineToModify.Contains(item.ToLower())) {
                            lineToModify = Regex.Replace(lineToModify, item.ToLower(), "");
                             
                            return lineToModify;
                        }
                    }
                }
            }
            return lineToModify;
        }

        public string FormateLineToGetHeader(string input, string header) => header;

        public string FormateLineToGetValue(string input, string header) => 
            input.Substring(input.IndexOf(header) + header.Length);

        public string FormateLineToRemoveEndOfFileLine(string input) =>
             input = (input.EndsWith(" ")) ? Regex.Replace(input, @"\s$", "") : input;

        public string FormateLineToReplaceCommaToDot(string input) =>
            input = (input.Contains(",")) ? Regex.Replace(input, ",", ".") : input;
    }
}
