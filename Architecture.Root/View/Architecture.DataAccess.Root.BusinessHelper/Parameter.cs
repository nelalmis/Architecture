using Architecture.Base;
using Architecture.Common.Types;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Architecture.DataAccess.Root.BusinessHelper
{
    public partial class Parameter:ObjectHelper
    {
        public Parameter(ExecutionDataContext ex) : base(ex) { }
        public Parameter() { }
        public GenericResponse<List<ParameterContract>> SelectByColumns(string paramType,string paramCode)
        {
            SqlCommand command;
            GenericResponse<List<ParameterContract>> returnObject;
            GenericResponse<SqlDataReader> sp;
            returnObject = this.InitializeGenericResponse<List<ParameterContract>>("SelectByColumns");
            
            command = this.DBLayer.GetDBCommand(SQLDBHelper.Databases.ARCHITECTURE, "COR.sel_ParameterByColumns");

            this.DBLayer.AddInParameter(command, "@ParamType", SqlDbType.NVarChar, paramType);
            this.DBLayer.AddInParameter(command, "@ParamCode", SqlDbType.NVarChar, paramCode);

            sp = this.DBLayer.ExecuteReader(command);
            if (!sp.Success)
            {
                returnObject.Results.AddRange(sp.Results);
                return returnObject;
            }

            #region Fill from SqlDataReader to List

            List<ParameterContract> listOfDataContract = new List<ParameterContract>();
            ParameterContract dataContract = null;

            SqlDataReader reader = sp.Value;

            while (reader.Read())
            {
                dataContract = new ParameterContract();
                
                dataContract.CompanyId = SQLDBHelper.GetInt64Value(reader["CompanyId"]);
                dataContract.ParamId = SQLDBHelper.GetInt32Value(reader["ParamId"]);
                dataContract.ParamCode = SQLDBHelper.GetStringValue(reader["ParamCode"]);
                dataContract.ParamType = SQLDBHelper.GetStringValue(reader["ParamType"]);
                dataContract.ParamDescription = SQLDBHelper.GetStringValue(reader["ParamDescription"]);
                dataContract.ParamValue = SQLDBHelper.GetInt32NullableValue(reader["ParamValue"]);
                dataContract.ParamValue2 = SQLDBHelper.GetStringValue(reader["ParamValue2"]);
                dataContract.ParamValue3 = SQLDBHelper.GetStringValue(reader["ParamValue3"]);
                dataContract.ParamValue4 = SQLDBHelper.GetStringValue(reader["ParamValue4"]);
                dataContract.ParamValue5 = SQLDBHelper.GetStringValue(reader["ParamValue5"]);
                dataContract.ParamValue6 = SQLDBHelper.GetStringValue(reader["ParamValue6"]);
                dataContract.ParamValue7 = SQLDBHelper.GetStringValue(reader["ParamValue7"]);
                dataContract.SystemDate = SQLDBHelper.GetDateTimeNullableValue(reader["SystemDate"]);
                dataContract.UserName = SQLDBHelper.GetStringValue(reader["UserName"]);
                dataContract.HostIp = SQLDBHelper.GetStringValue(reader["HostIp"]);
                dataContract.HostName = SQLDBHelper.GetStringValue(reader["HostName"]);
                dataContract.UpdateUserName = SQLDBHelper.GetStringValue(reader["UpdateUserName"]);
                dataContract.UpdateSystemDate = SQLDBHelper.GetDateTimeNullableValue(reader["UpdateSystemDate"]);

                listOfDataContract.Add(dataContract);

            }

            reader.Close();
            //Return 

            returnObject.Value = listOfDataContract;

            #endregion

            return returnObject;
        }

    }
}
