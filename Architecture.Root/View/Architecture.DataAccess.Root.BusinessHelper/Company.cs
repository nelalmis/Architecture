using Architecture.Base;
using Architecture.Common.Types;
using System.Data;
using System.Data.SqlClient;

namespace Architecture.DataAccess.Root.BusinessHelper
{
    public partial class Company:ObjectHelper
    {
        public GenericResponse<CompanyContract> SelectByColumns(string email,string password)
        {
            SqlCommand command;
            GenericResponse<CompanyContract> returnObject;
            GenericResponse<SqlDataReader> sp;
            returnObject = this.InitializeGenericResponse<CompanyContract>("");

            command = this.DBLayer.GetDBCommand(SQLDBHelper.Databases.ARCHITECTURE, "COR.sel_CompanyByColumns");

            this.DBLayer.AddInParameter(command, "@UserName", SqlDbType.NVarChar, email);
            this.DBLayer.AddInParameter(command, "@Password", SqlDbType.NVarChar, password);

            sp = this.DBLayer.ExecuteReader(command);
            if (!sp.Success)
            {
                returnObject.Results.AddRange(sp.Results);
                return returnObject;
            }

            #region Fill from SqlDataReader to List
            
            CompanyContract dataContract = null;

            SqlDataReader reader = sp.Value;

            if (reader.Read())
            {
                dataContract = new CompanyContract();
                dataContract.Contact = new CompanyContactContract();
                //dataContract.Authentication = new CompanyAuthenticationContract();
              
                dataContract.Contact.Address.Address = SQLDBHelper.GetStringValue(reader["Address"]);
                dataContract.Contact.Address.City = SQLDBHelper.GetStringValue(reader["City"]);
                dataContract.CompanyFullName = SQLDBHelper.GetStringValue(reader["CompanyFullName"]);
                dataContract.CompanyIcon = SQLDBHelper.GetStringValue(reader["CompanyIcon"]);
                dataContract.CompanyId = SQLDBHelper.GetInt32Value(reader["CompanyId"]);
                dataContract.CompanyName = SQLDBHelper.GetStringValue(reader["CompanyName"]);
                dataContract.Contact.Address.Country = SQLDBHelper.GetStringValue(reader["Country"]);
                dataContract.Contact.Email.Email = SQLDBHelper.GetStringValue(reader["Email"]);
                dataContract.Contact.Fax.Fax = SQLDBHelper.GetStringValue(reader["Fax"]);
                dataContract.HostIp = SQLDBHelper.GetStringValue(reader["HostIp"]);
                dataContract.HostName = SQLDBHelper.GetStringValue(reader["HostName"]);
                //dataContract.Authentication.Password = SQLDBHelper.GetStringValue(reader["Password"]);
                dataContract.Contact.Phone.Phone = SQLDBHelper.GetStringValue(reader["Phone"]);
                dataContract.Contact.Address.PostalCode = SQLDBHelper.GetStringValue(reader["PostalCode"]);
                dataContract.State = SQLDBHelper.GetByteValue(reader["State"]);
                dataContract.SystemDate = SQLDBHelper.GetDateTimeNullableValue(reader["SystemDate"]);
                dataContract.UpdateSystemDate = SQLDBHelper.GetDateTimeNullableValue(reader["UpdateSystemDate"]);
                dataContract.UpdateUserName = SQLDBHelper.GetStringValue(reader["UpdateUserName"]);
                dataContract.UserName = SQLDBHelper.GetStringValue(reader["UserName"]);
            }

            reader.Close();
            //Return 

            returnObject.Value = dataContract;

            #endregion

            return returnObject;
        }
        
    }
}
