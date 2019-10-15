using Architecture.Base;
using Architecture.Common.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Helper
{

    public class ExcelClass<T>: ObjectHelperBase
         where T : class
    {

        private static  List<T> dataToPrint;
        // Excel object references.
        private static Microsoft.Office.Interop.Excel.Application _excelApp = null;
        private static Microsoft.Office.Interop.Excel.Workbooks _books = null;
        private static Microsoft.Office.Interop.Excel._Workbook _book = null;
        private static Microsoft.Office.Interop.Excel.Sheets _sheets = null;
        private static Microsoft.Office.Interop.Excel._Worksheet _sheet = null;
        private static Microsoft.Office.Interop.Excel.Range _range = null;
        private static Microsoft.Office.Interop.Excel.Font _font = null;
        private static  object  _optionalValue = Missing.Value;
        
        public ExcelClass(List<T> dataList)
        {
            dataToPrint = dataList;
        }

        /// <summary>
        /// Excele aktrma işlemini yapar.
        /// </summary>
        public void GenerateReport()
        {
            try
            {
                if (dataToPrint != null)
                {
                    if (dataToPrint.Count != 0)
                    {
                        CreateExcelRef();
                        FillSheet();
                        OpenReport();
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                ReleaseObject(_sheet);
                ReleaseObject(_sheets);
                ReleaseObject(_book);
                ReleaseObject(_books);
                ReleaseObject(_excelApp);
            }
        }
        private static void OpenReport()
        {
            _excelApp.Visible = true;
        }
        private static void FillSheet()
        {
            object[] header = CreateHeader();
            WriteData(header);
        }
        private static void WriteData(object[] header)
        {
            object[,] objData = new object[dataToPrint.Count, header.Length];
            for (int j = 0; j < dataToPrint.Count; j++)
            {
                var item = dataToPrint[j];
                for (int i = 0; i < header.Length; i++)
                {
                    var y = typeof(T).InvokeMember
            (header[i].ToString(), BindingFlags.GetProperty, null, item, null);
                    objData[j, i] = (y == null) ? "" : y.ToString();
                }
            }
            AddExcelRows("A2", dataToPrint.Count, header.Length, objData);
            AutoFitColumns("A1", dataToPrint.Count + 1, header.Length);
        }
        private static void AutoFitColumns(string startRange, int rowCount, int colCount)
        {
            _range = _sheet.get_Range(startRange, _optionalValue);
            _range = _range.get_Resize(rowCount, colCount);
            _range.Columns.AutoFit();
        }
        private static object[] CreateHeader()
        {
            PropertyInfo[] headerInfo = typeof(T).GetProperties();
            List<object> objHeaders = new List<object>();
            for (int n = 0; n < headerInfo.Length; n++)
            {
                objHeaders.Add(headerInfo[n].Name);
            }
            var headerToAdd = objHeaders.ToArray();
            AddExcelRows("A1", 1, headerToAdd.Length, headerToAdd);
            SetHeaderStyle();

            return headerToAdd;
        }
        private static void SetHeaderStyle()
        {
            _font = _range.Font;
            _font.Bold = true;
        }
        private static void AddExcelRows(string startRange, int rowCount, int colCount, object values)
        {
            _range = _sheet.get_Range(startRange, _optionalValue);
            _range = _range.get_Resize(rowCount, colCount);
            _range.set_Value(_optionalValue, values);
        }
        private static void CreateExcelRef()
        {
            _excelApp = new Microsoft.Office.Interop.Excel.Application();
            _books = (Microsoft.Office.Interop.Excel.Workbooks)_excelApp.Workbooks;
            _book = (Microsoft.Office.Interop.Excel._Workbook)(_books.Add(_optionalValue));
            _sheets = (Microsoft.Office.Interop.Excel.Sheets)_book.Worksheets;
            _sheet = (Microsoft.Office.Interop.Excel._Worksheet)(_sheets.get_Item(1));
        }
        private static void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                throw;
            }
            finally
            {
                GC.Collect();
            }
        }

        /// <summary>
        /// Verilen lokasyondaki excel sheetlerini okur.
        /// </summary>
        /// <param name="fileLocation"></param>
        /// <returns></returns>
        public GenericResponse<DataSet> GetExcelFileRead(string fileLocation)
        {
            GenericResponse<DataSet> returnObject=null;
            returnObject = InitializeGenericResponse<DataSet>("Architecture.CommonMethods.ExcelClass" + ".GetExcelFileRead");

            try
            {
                if (!System.IO.File.Exists(fileLocation))
                {
                    returnObject.Results.Add(new FileNotFoundException());  //GetMessageByColumns("RelShipNet", "NotFindFile");
                    return returnObject;
                }
                string FileName = Path.GetFileName(fileLocation);
                string fileExtension = System.IO.Path.GetExtension(fileLocation);
                string excelConnectionString = string.Empty;
                if (fileExtension == ".xls")
                {
                    excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                    fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                }
                //connection String for xlsx file format.
                else if (fileExtension == ".xlsx")
                {
                    excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                    fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                }
                //Create Connection to Excel work book and add oledb namespace
                OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);

                DataTable dt = new DataTable();
                excelConnection.Open();
                dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                if (dt == null)
                {
                    returnObject.Results.Add(new ArgumentNullException(dt.ToString()));
                    return returnObject;
                }
                excelConnection.Close();
                String[] excelSheets = new String[dt.Rows.Count];
                int t = 0;
                //excel data saves in temp file here.
                foreach (DataRow row in dt.Rows)
                {
                    excelSheets[t] = row["TABLE_NAME"].ToString();
                    t++;
                }
                OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);

                string query = "";
                DataSet ds = new DataSet();

                for (int i = 0; i < t; i++)
                {
                    query = string.Format("Select * from [{" + i + "}]", excelSheets[i]);
                    using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                    {
                        dataAdapter.Fill(ds);
                    }
                } 
                returnObject.Value = ds;
                return returnObject;
            }
            catch(Exception ex)
            {
                returnObject.Results.Add(ex);
                return returnObject;
            }
        }
    }
}

