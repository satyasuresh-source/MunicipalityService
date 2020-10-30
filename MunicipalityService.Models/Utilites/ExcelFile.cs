using MunicipalityService.Models.Utilites.Interfaces;
using System;
using ClosedXML.Excel;
using System.Data;

namespace MunicipalityService.Models.Utilites
{
    public class ExcelFile : IFileReader
    {
        private readonly string filePath;
        public ExcelFile(string filePath)
        {
            this.filePath = filePath;
        }

        /// <summary>
        /// Read the Excel file from the Location
        /// </summary>
        public DataTable ConvertToDataTable()
        {
            DataTable dt = new DataTable();

            using (XLWorkbook workBook = new XLWorkbook(filePath))
            {
                //Read the first Sheet from Excel file.
                IXLWorksheet workSheet = workBook.Worksheet(1);             

                bool firstRow = true;
                foreach (IXLRow row in workSheet.Rows())
                {
                    //Use the first row to add columns to DataTable.
                    if (firstRow)
                    {
                        foreach (IXLCell cell in row.Cells())
                        {
                            dt.Columns.Add(cell.Value.ToString());
                        }
                        firstRow = false;
                    }
                    else
                    {
                        //Add rows to DataTable.
                        dt.Rows.Add();
                        int i = 0;
                        foreach (IXLCell cell in row.Cells())
                        {
                            dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                            i++;
                        }
                    }                    
                }
                return dt;
            }            
        }

    }
}
