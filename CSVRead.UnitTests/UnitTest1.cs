using System;
using CSVRead.Controllers.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using System.Text;
using System.Collections.Generic;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CSVRead.UnitTests
{ 
    [TestClass]
    public class FilesToJasonControllerTest
    {
         private static readonly HttpClient client = new HttpClient();

        [TestMethod]
        public void CsvToJason_CSVFile_shouldReturnValidJson()
        {
            var testCSV = TestCsv();
            var controller = new FilesToJasonController(); 
            byte[] byteArray = Encoding.UTF8.GetBytes(testCSV); 
            MemoryStream stream = new MemoryStream(byteArray);
            StreamReader reader = new StreamReader(stream);
            var result = controller.CsvToJasonx(reader);
            dynamic resultArray = JArray.Parse(result);
            string name = resultArray[0].Name; 
           
            Assert.AreEqual(name, "Tom");
        } 

        private string TestCsv()
        {
            string csv = @"Id,Name,City
            1,Tom,Toronto
            2,Mark,New York
            3,Lou,Montreal
            4,Smith,Vancouver
            5,Allen,Toronto";

            return csv;
        }  
    }
}
