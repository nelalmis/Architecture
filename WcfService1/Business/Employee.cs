using Architecture;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Types;
using static Architecture.SQLDBHelper;

namespace Business
{
    public class Employee : BusinessBaseClass
    {
        public static string className = "WcfService1.Business.Employee";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fname"></param>
        /// <returns></returns>
        public GenericResponse<List<employee>> SelectByColumn(string fname)
        {
            SqlCommand command;
            GenericResponse<List<employee>> returnObject;
            GenericResponse<SqlDataReader> sp;

            returnObject = this.InitializeGenericResponse<List<employee>>(className + ".SelectByColumn");

            command = this.DBLayer.GetDBCommand(Databases.Company, "sel_EmployeeByFname");

            this.DBLayer.AddInParameter(command, "@fname", SqlDbType.NVarChar, fname);

            sp = this.DBLayer.ExecuteReader(command);
            if (!sp.Success)
            {
                returnObject.Results.AddRange(sp.Results);
                return returnObject;
            }

            #region Fill from SqlDataReader to List

            List<employee> listOfDataContract = new List<employee>();
            employee dataContract = null;
            SqlDataReader reader = sp.Value;

            while (reader.Read())
            {
                dataContract = new employee();
                dataContract.address = SQLDBHelper.GetStringValue(reader["address"]);
                dataContract.bdate = SQLDBHelper.GetDateTimeValue(reader["bdate"]);
                dataContract.dno = SQLDBHelper.GetInt16Value(reader["dno"]);
                dataContract.fname = SQLDBHelper.GetStringValue(reader["fname"]);
                dataContract.lname = SQLDBHelper.GetStringValue(reader["lname"]);
                dataContract.minit = SQLDBHelper.GetStringValue(reader["minit"]);
                dataContract.salary = SQLDBHelper.GetInt32NullableValue(reader["salary"]);
                dataContract.sex = SQLDBHelper.GetStringValue(reader["sex"]);
                dataContract.ssn = SQLDBHelper.GetStringValue(reader["ssn"]);
                dataContract.superssn = SQLDBHelper.GetStringValue(reader["superssn"]);

                listOfDataContract.Add(dataContract);

            }

            reader.Close();
            //Return 

            returnObject.Value = listOfDataContract;

            #endregion

            return returnObject;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contract"></param>
        /// <returns></returns>
        public GenericResponse<Int32> Insert(employee contract)
        {
            SqlCommand command;
            GenericResponse<Int32> returnObject;
            
            returnObject = this.InitializeGenericResponse<Int32>(className+".Insert");

            command = this.DBLayer.GetDBCommand(Databases.Company, "ins_Employee");

            this.DBLayer.AddInParameter(command, "@fname", SqlDbType.NVarChar, contract.fname);
            this.DBLayer.AddInParameter(command, "@address", SqlDbType.NVarChar, contract.address);
            this.DBLayer.AddInParameter(command, "@bdate", SqlDbType.SmallDateTime, contract.bdate);
            this.DBLayer.AddInParameter(command, "@dno", SqlDbType.SmallInt, contract.dno);
            this.DBLayer.AddInParameter(command, "@lname", SqlDbType.NVarChar, contract.lname);
            this.DBLayer.AddInParameter(command, "@minit", SqlDbType.NVarChar, contract.minit);
            this.DBLayer.AddInParameter(command, "@salary", SqlDbType.Decimal, contract.salary);
            this.DBLayer.AddInParameter(command, "@sex", SqlDbType.NVarChar, contract.sex);
            this.DBLayer.AddInParameter(command, "@ssn", SqlDbType.NVarChar, contract.ssn);
            this.DBLayer.AddInParameter(command, "@superssn", SqlDbType.NVarChar, contract.superssn);

            var result = this.DBLayer.ExecuteNonQuery(command);
            if (!result.Success)
            {
                returnObject.Results.AddRange(result.Results);
                return returnObject;
            }

            returnObject.Value = result.Value;
            return returnObject;
        }

    }
}
