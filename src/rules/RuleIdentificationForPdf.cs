using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace PdfToCsv.src {
    class RuleIdentificationForPdf {

        public bool IdentifyPdfThatHasRule(string pdfFileName, List<string> listOfRuleNames) {
            //logic to map rule name to pdf so it handles only pdf that has rules
            for (int indexOfRules = 0; indexOfRules < listOfRuleNames.Count; indexOfRules++) {
                pdfFileName = Regex.Replace(pdfFileName, ".pdf", "");
                string ruleFileName = listOfRuleNames[indexOfRules];
                ruleFileName = Regex.Replace(ruleFileName, ".csv", "");
                if (pdfFileName == ruleFileName) {
                    return true;
                }
            }
            return false;
        }
}
}
