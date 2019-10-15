using Architecture;
using Architecture.Base;
using Architecture.Common.Types;
using System;
using System.IO;
using System.Text;

namespace Architecture.Helper
{
    public class TxtClass: ObjectHelperBase
    {
        /// <summary>
        /// ClassName
        /// </summary>
        private const string className = "Architecture.CommonMethods.TxtClass";

        #region Properties
        private string fileName="MyDocument.txt";
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        private string filePath= Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }
        public string FullPath { get; set; }
        #endregion

        /// <summary>
        /// Constacture
        /// Usage fileName="Mydocument.txt"
        /// </summary>
        public TxtClass(string filePath, string fileName)
        {
            if (!String.IsNullOrEmpty(filePath))
            {
                this.FilePath = filePath;
            }
            if (!String.IsNullOrEmpty(fileName))
            {
                this.FileName = fileName;
            }
            FullPath = Path.Combine(FilePath, FileName);
        }

        /// <summary>
        /// Verilen lokasyondaki text dosyasını okur.
        /// </summary>
        /// <param name="fileLocation"></param>
        /// <returns></returns>
        public GenericResponse<StreamReader> GetTextFileRead()
        {
            GenericResponse<StreamReader> returnObject;
            returnObject = InitializeGenericResponse<StreamReader>(className + ".GetTextFileRead");

            if (!System.IO.File.Exists(FullPath))
            {
                returnObject.Results.Add(new FileNotFoundException());  //GetMessageByColumns("RelShipNet", "NotFindFile");
                return returnObject;
            }

            try
            {
                FileStream fs = new FileStream(FullPath, FileMode.Open, FileAccess.Read);
                StreamReader sw = new StreamReader(fs);
                returnObject.Value = sw;
                return returnObject;
            }
            catch { throw; }
        }

        /// <summary>
        /// Text dosyasına yazma işlemi yapar. Defult olarak fileName ve path parametrerini alır. İstenirse değiştirilebilir.
        /// </summary>
        /// <param name="sb"></param>
        public void ExportToTxtFile(StringBuilder sb)
        {
            if (File.Exists(FullPath))
            {
                File.Delete(FullPath);
            }
            using (StreamWriter sw = File.CreateText(FullPath))
            {
                sw.WriteLine(sb);
            }
        }
    }
}
