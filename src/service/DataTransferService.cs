using iTextSharp.text.pdf.parser;
using PdfToCsv.src;
using PdfToCsv.src.logger;
using System;
using System.Collections.Generic;
using System.IO;

namespace PdfToCsv {

    delegate void WriteLog();

    class DataTransferService {
        private readonly CreateCsvWithPdfData createCsvFileWithPdfData;
        private readonly ReadPdf pdfReader;
        private readonly GetRules rules;
        private readonly TextModificationService textModification;
        private readonly ILogger logger;
        private readonly IFileService fileService;

        public DataTransferService(
            CreateCsvWithPdfData createCsvFileWithPdfData, ReadPdf pdfReader, GetRules rules,
            TextModificationService textModification, IFileService fileService, ILogger logger) {
            this.createCsvFileWithPdfData = createCsvFileWithPdfData;
            this.pdfReader = pdfReader;
            this.rules = rules;
            this.textModification = textModification;
            this.fileService = fileService;
            this.logger = logger;
        }

        public void transferData() {

            RuleIdentificationForPdf ruleIdentification = new RuleIdentificationForPdf();

            //pdf
            List<string> listOfPdfLinks = ReadPdf.GetAllPdfFromFolder();
            List<string> listOfPdfNames = pdfReader.GetAllPdfFileNames();


            //csv
            List<string> listOfRuleNames = rules.GetAllFileRuleNames();
            List<KeyValuePair<string, string>> keyValuePairListOfNonUniqueCsvData = new List<KeyValuePair<string, string>>();

            //loop through all pdf files and handle it 1by1
            for (int indexOfPdf = 0; indexOfPdf < listOfPdfNames.Count; indexOfPdf++) {
                //clean dictionary for another values to be unique
                keyValuePairListOfNonUniqueCsvData.Clear();
                //get rules for particular pdf file
                List<string> listOfRules = rules.GetListOfRulesFromOneFile(listOfRuleNames, rules, listOfPdfNames[indexOfPdf]);
                //to make sure that program handles pdf's that have rules
                if (ruleIdentification.IdentifyPdfThatHasRule(listOfPdfNames[indexOfPdf], listOfRuleNames) == true) {
                    //create stream writer of csv file that is created based on pdf name
                    try {
                        using (StreamWriter sw = new StreamWriter(createCsvFileWithPdfData.CreateCsvFile(pdfReader.GetPdfFileName(indexOfPdf)), true)) {
                            logger.Log(LogLevel.Information, "CSV file has been created");
                            for (int j = 1; j <= pdfReader.GetPdfReader(indexOfPdf, listOfPdfLinks).NumberOfPages; j++) {
                                string pageText = PdfTextExtractor.GetTextFromPage(pdfReader.GetPdfReader(indexOfPdf, listOfPdfLinks), j);

                                string[] lines = pageText.Split('\n');

                                foreach (string line in lines) {
                                    string myLine = line;
                                    string header, value;
                                    myLine = textModification.FormateLine(myLine).ToLower();
                                    if (!myLine.Equals("")) {
                                        foreach (var rule in listOfRules) {
                                            if (myLine.StartsWith(rule.ToLower())) {
                                                if (myLine.Equals(rule.ToLower())) {
                                                    header = textModification.FormateLineToGetHeader(myLine, rule.ToLower());
                                                    keyValuePairListOfNonUniqueCsvData.Add(new KeyValuePair<string, string>(header, ""));
                                                    //sw.Write(header);
                                                } else if (myLine.Contains(rule.ToLower())) {
                                                    header = textModification.FormateLineToGetHeader(myLine, rule.ToLower());
                                                    value = textModification.FormateLineToGetValue(myLine, rule.ToLower());
                                                    keyValuePairListOfNonUniqueCsvData.Add(new KeyValuePair<string, string>(header, value));
                                                    //sw.Write(header);
                                                    //sw.WriteLine(value);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            foreach (var item in keyValuePairListOfNonUniqueCsvData) {
                                Console.WriteLine(item);
                            }
                            if (keyValuePairListOfNonUniqueCsvData.Count != 0) {
                                //loop through list of converted data
                                for (int i = 0; i < keyValuePairListOfNonUniqueCsvData.Count; i++) {
                                    string finalKey;
                                    //modify keys to remove gaps etc. mentioned in to_delete file
                                    if (keyValuePairListOfNonUniqueCsvData.Count - 1 == i) {
                                        //modify keys to remove gaps etc. mentioned in to_delete file
                                        finalKey = textModification.FormateLine(keyValuePairListOfNonUniqueCsvData[i].Key);
                                        finalKey = textModification.FormateLineToRemoveEndOfFileLine(finalKey);
                                        finalKey = textModification.FormateLineToReplaceCommaToDot(finalKey);
                                        //write without comma when end of list of headers
                                        sw.WriteLine(finalKey);
                                        //write with comma as separator for csv
                                    } else {
                                        finalKey = textModification.FormateLine(keyValuePairListOfNonUniqueCsvData[i].Key);
                                        finalKey = textModification.FormateLineToRemoveEndOfFileLine(finalKey);
                                        finalKey = textModification.FormateLineToReplaceCommaToDot(finalKey);
                                        sw.Write($"{finalKey},");
                                    }
                                }
                                //loop through list of converted data
                                for (int i = 0; i < keyValuePairListOfNonUniqueCsvData.Count; i++) {
                                    string finalValue;
                                    //write all values as headers in csv file
                                    if (keyValuePairListOfNonUniqueCsvData.Count - 1 == i) {
                                        //modify values to remove gaps etc. mentioned in to_delete file
                                        finalValue = textModification.FormateLine(keyValuePairListOfNonUniqueCsvData[i].Value);
                                        finalValue = textModification.FormateLineToRemoveEndOfFileLine(finalValue);
                                        finalValue = textModification.FormateLineToReplaceCommaToDot(finalValue);
                                        //write without comma when end of list of headers
                                        sw.WriteLine(finalValue);
                                        //write with comma as separator for csv
                                    } else {
                                        finalValue = textModification.FormateLine(keyValuePairListOfNonUniqueCsvData[i].Value);
                                        finalValue = textModification.FormateLineToRemoveEndOfFileLine(finalValue);
                                        finalValue = textModification.FormateLineToReplaceCommaToDot(finalValue);
                                        sw.Write($"{finalValue},");
                                    }
                                }
                            }

                            //clean list with previous rules to add new ones as per new file
                            rules.CleanRules();
                        }
                    } catch (NullReferenceException nullRefException) {
                        logger.LogCritical(nullRefException, nullRefException.Message);
                        nullRefException.StackTrace.ToLower();
                    }
                }
            }
        }
    }
}