using Architecture.Common.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Helper
{
    public static class DllCollect
    {
        private static List<FileInfo> fileList;
        public static bool DllCopy()
        {

            try
            {
                fileList = new List<FileInfo>();
                if (!Directory.Exists(Constants.DllParentPath))
                {
                    Directory.CreateDirectory(Constants.DllParentPath);
                    Directory.CreateDirectory(Constants.ClientDllPath);
                    Directory.CreateDirectory(Constants.ServerDllPath);
                }

                var serverFolder = Constants.ServerDllPath + @"\";
                var clientFolder = Constants.ClientDllPath + @"\";
                var aranacakFolder = Constants.DllSearchFolderPath+@"\";
                var AllDllList = DllSearch(aranacakFolder);

                var serverDll = AllDllList.Where(u => !u.FullName.Contains("."+ Constants.View) && !u.FullName.Contains("Test")).ToList();
                var clientDll = AllDllList.Where(u => u.FullName.Contains("."+ Constants.View) || u.FullName.Contains(".Types")).ToList();
                serverDll.AddRange(AllDllList.Where(u => u.FullName.Contains("." + Constants.View + ".Root")).ToList());
                serverDll.AddRange(AllDllList.Where(u => u.FullName.Contains("." + Constants.View + ".Resource")).ToList());


                foreach (var dll in serverDll)
                {
                    var newDll = serverFolder + dll.Name;
                    dll.CopyTo(newDll, true);
                }
                foreach (var dll in clientDll)
                {
                    var newDll = clientFolder + dll.Name;
                    dll.CopyTo(newDll, true);
                }
                return true;
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        private static List<FileInfo> DllSearch(string sDir)
        {
            try
            {
                foreach (var d in Directory.GetDirectories(sDir))
                {
                    foreach (var f in Directory.GetFiles(d, "*.dll"))
                    {
                        FileInfo i = new FileInfo(f);
                        var name = i.Name.Replace(".dll", "");
                        if (f.Contains("Architecture") && f.Contains("bin") && f.Contains("Debug") && (i.Directory.FullName.Contains(name) || name.Contains(i.Directory.FullName)))
                            fileList.Add(i);
                    }
                    DllSearch(d);
                }
            }catch (Exception e)
            {
                throw e;
            }
            return fileList;
        }
    }
}
