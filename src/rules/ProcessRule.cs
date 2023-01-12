using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PdfToCsv.src {
    class ProcessRule {

        public void ProcessFile(List<string>rules, string path) {
            int i = 0;

            using (var reader = new StreamReader(path, true)) {
                while (!reader.EndOfStream) {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    foreach (var item in values) {
                        rules.Add(values[i]);
                        i++;
                    }
                }
            }
        }
    }
}
