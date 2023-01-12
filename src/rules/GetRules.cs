using PdfToCsv.src;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace PdfToCsv {
    class GetRules {

        public List<string> rules { set; get; }
        private ProcessRule processRule { set;get;}
        private List<string> listOfAllRuleNames = new List<string>();
        private List<string> listOfAllRuleLinks = new List<string>();


        static string targetDirectory = @"..\..\..\src\rules\data\rules";
        string[] arrOfRuleFiles = Directory.GetFiles(targetDirectory);
        


        public GetRules(List<string> rules, ProcessRule processRule) {
            this.processRule = processRule;
            this.rules = rules;
        }

        public List<string> GetListOfRulesFromOneFile(List<string> ruleNames, GetRules getRules, string pdfFileName) {
            pdfFileName = Regex.Replace(pdfFileName, ".pdf", "");
            for (int i = 0; i < ruleNames.Count; i++) {
                if (ruleNames[i].Contains(pdfFileName)) {
                    ruleNames[i] = Regex.Replace(pdfFileName, ".csv", "");
                    if (pdfFileName == ruleNames[i])
                        processRule.ProcessFile(rules, listOfAllRuleLinks[i]);
                    break;
                }
            }
            return rules;
        }

        public void CleanRules() {
            rules.Clear();
        }

        public List<string> GetAllFileRuleLinks() {
            foreach (var csvLink in arrOfRuleFiles)
                listOfAllRuleLinks.Add(csvLink);

            return rules;
        }

        public string GetFileRuleName(int indexOfFile) =>
            Path.GetFileNameWithoutExtension(arrOfRuleFiles[indexOfFile]);

        public string GetFileRulePath(int indexOfFile) =>
            Path.GetFileName(arrOfRuleFiles[indexOfFile]);

        public List<string> GetAllFileRuleNames() {
            GetAllFileRuleLinks();
            for (int indexOfCsvLink = 0; indexOfCsvLink < listOfAllRuleLinks.Count; indexOfCsvLink++) {
                listOfAllRuleNames.Add(Path.GetFileName(listOfAllRuleLinks[indexOfCsvLink]));
            }
            return listOfAllRuleNames;
        }
    }
}
