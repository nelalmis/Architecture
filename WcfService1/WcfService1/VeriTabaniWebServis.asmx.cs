using Business;
using Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WcfService1
{
    /// <summary>
    /// Summary description for VeriTabaniWebServis
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class VeriTabaniWebServis : System.Web.Services.WebService
    {
        [WebMethod(CacheDuration =120,MessageName ="ForStringFname")]
        [XmlInclude(typeof(employee))]
        public List<employee> GetEmployee(string fname)
        {
            Employee emp = new Employee();
            var result= emp.SelectByColumn(fname);
            if (!result.Success)
            {
                Console.WriteLine("İşlem başarısız oldu.", result.Results);
                return result.Value;
            }
            return result.Value;
        }

        [WebMethod(CacheDuration = 120, MessageName = "ForIntegerFname")]
        [XmlInclude(typeof(employee))]
        public List<employee> GetEmployee(int fname)
        {
            Employee emp = new Employee();
            var result = emp.SelectByColumn(fname.ToString());
            if (!result.Success)
            {
                Console.WriteLine("İşlem başarısız oldu.", result.Results);
                return result.Value;
            }
            return result.Value;
        }

        [WebMethod]
        public string InsertEmployee(string fname,string minit, string lname,string ssn,string address,string sex,Int32 salary,string superssn,Int16 dno)
        {
            Employee emp = new Employee();
            employee calisan = new employee() {
                fname=fname,
                minit=minit,
                lname=lname,
                ssn=ssn,
                bdate=DateTime.Now,
                address=address,
                sex=sex,
                salary=salary,
                superssn=superssn,
                dno=dno
            };
            var result = emp.Insert(calisan);
            if (!result.Success)
            {
                Console.WriteLine("İşlem başarısız oldu.", result.Results);
                return result.Results.FirstOrDefault().ToString();
            }
            return result.Value.ToString();
        }
       
    }
}
