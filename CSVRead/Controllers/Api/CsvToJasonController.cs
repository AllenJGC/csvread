using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;

namespace CSVRead.Controllers.Api
{
    public class FilesToJasonController : ApiController
    {  
        [HttpPost]
        public HttpResponseMessage CsvToJason()
        {
            const string CSVRead = ".csv";
            
            HttpPostedFile uploadedFile = null;
             
            uploadedFile = System.Web.HttpContext.Current.Request.Files["CSVFile"];              

            var fileExt = Path.GetExtension(uploadedFile.FileName);

            if (fileExt != CSVRead)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Please, select a valid CSV file");
            }

            var csvReader = new StreamReader(uploadedFile.InputStream);

            try
            {
                string results = CsvToJasonx(csvReader);
                return Request.CreateResponse(HttpStatusCode.OK, results);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }            
        } 

        public string CsvToJasonx(StreamReader csvReader)
        {
            string[] cols;
            string[] rows;
            DataTable table = new DataTable();
            Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
     
            string inputDataRead;
            var values = new List<string[]>();

            string line = csvReader.ReadLine();
            cols = Regex.Split(line, ",");

            for (int i = 0; i < cols.Length; i++)
            {
                table.Columns.Add(cols[i], typeof(string));
            }

            while ((inputDataRead = csvReader.ReadLine()) != null)
            {
                var row = string.Empty;
                rows = CSVParser.Split(inputDataRead);
                DataRow dr = table.NewRow();

                for (int i = 0; i < rows.Length; i++)
                {
                    rows[i] = rows[i].TrimStart(' ', '"');
                    rows[i] = rows[i].TrimEnd('"');
                }

                for (var i = 0; i < rows.Length; i++)
                {
                    dr[i] = rows[i];
                }
                table.Rows.Add(dr);
            }

            string Results = JsonConvert.SerializeObject(table, Formatting.Indented);
            return Results;
             
        }
    }
}
