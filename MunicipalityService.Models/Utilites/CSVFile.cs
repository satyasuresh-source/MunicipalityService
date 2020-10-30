using MunicipalityService.Models.Utilites.Interfaces;
using System.Data;
using System.IO;

namespace MunicipalityService.Models.Utilites
{
    public class CSVFile : IFileReader
    {
        private readonly string filePath;
        public CSVFile(string filePath)
        {
            this.filePath = filePath;
        }
        public DataTable ConvertToDataTable()
        {
            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(filePath))
            {
                string[] headers = sr.ReadLine().Split('|');
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }
                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split('|');
                    if (rows.Length == headers.Length)
                    {
                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < headers.Length; i++)
                        {
                            dr[i] = rows[i];
                        }
                        dt.Rows.Add(dr);
                    }
                    else
                    { 
                      //Log
                    }
                }

            }

            return dt;
        }
    }
}
