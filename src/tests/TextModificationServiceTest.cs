using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PdfToCsv.src.tests {
    public class TextModificationServiceTest {
        private readonly TextModificationService victim;

        public TextModificationServiceTest() {
            victim = new TextModificationService();
        }

        [Fact]
        public void FormateLineToReplaceCommaToDotForString() {
            string inputParameter = "I'am input! Please, replace,, all, commas to dots: 123,321.00.";

            string actualResult = victim.FormateLineToReplaceCommaToDot(inputParameter);
            string expectedResult = "I'am input! Please. replace.. all. commas to dots: 123.321.00.";

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void FormateLineToRemoveEndOfFileLineForString() {
            string inputParameter = "I'am input! Please, pass ";

            string actualResult = victim.FormateLineToRemoveEndOfFileLine(inputParameter);
            string expectedResult = "I'am input! Please, pass";

            Assert.Equal(expectedResult, actualResult);

        }

        [Fact]
        public void FormateLineToGetValueFromKeyValuePairList() {

            string[] input = new string[] {
                "Value to check",
                "One two three four five",
                "input headerHere check"
           };

            string[] headers = new string[] {
                "Value",
                "three",
                "headerHere"
            };
            string[] expectedResult = new string[] {
                " to check",
                " four five",
                " check"
            };

            string actualResult;
            for (int i = 0; i < input.Length; i++) {
                actualResult = victim.FormateLineToGetValue(input[i], headers[i]);
                Assert.Equal(expectedResult[i], actualResult);
            }

        }
    }
}
