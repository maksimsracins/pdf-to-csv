using Moq;
using PdfToCsv.src.logger;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PdfToCsv.src.tests {
    public class DataTransferServiceTest {
        private readonly DataTransferService victim;
        private readonly Mock<CreateCsvWithPdfData> createCsvWithPdfDataMock = new Mock<CreateCsvWithPdfData>();
        private readonly Mock<ReadPdf> readPdfMock = new Mock<ReadPdf>();
        private readonly Mock<GetRules> getRulesMock = new Mock<GetRules>();
        private readonly Mock<TextModificationService> textModificationServiceMock = new Mock<TextModificationService>();
        private readonly Mock<IFileService> fileServiceMock = new Mock<IFileService>();
        private readonly Mock<ILogger> loggerMock = new Mock<ILogger>();

        public DataTransferServiceTest() {
            victim = new DataTransferService(
                createCsvWithPdfDataMock.Object,
                readPdfMock.Object,
                getRulesMock.Object,
                textModificationServiceMock.Object,
                fileServiceMock.Object,
                loggerMock.Object);
        }
        public void DataTransfer() {

        }

    }
}
