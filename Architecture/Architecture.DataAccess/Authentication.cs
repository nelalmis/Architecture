using Architecture.Base;
using Architecture.Common.Types;
using System.Data;
using System.Data.SqlClient;

namespace Architecture.DataAccess
{
    public partial class Authentication : ObjectHelper
    {
        public Authentication()
        {
        }
        public GenericResponse<AuthenticationContract> SelectByColumns(string userName,string password)
        {
            SqlCommand command;
            GenericResponse<AuthenticationContract> returnObject;
            GenericResponse<SqlDataReader> sp;
            returnObject = InitializeGenericResponse<AuthenticationContract>("");

            command = this.DBLayer.GetDBCommand(SQLDBHelper.Databases.ARCHITECTURE, "COR.sel_AuthenticationByColumns");

            this.DBLayer.AddInParameter(command, "@UserName", SqlDbType.NVarChar, userName);
            this.DBLayer.AddInParameter(command, "@Password", SqlDbType.NVarChar, password);

            sp = this.DBLayer.ExecuteReader(command);
            if (!sp.Success)
            {
                returnObject.Results.AddRange(sp.Results);
                return returnObject;
            }

            #region Fill from SqlDataReader to List
            
            AuthenticationContract dataContract = null;

            SqlDataReader reader = sp.Value;

            while (reader.Read())
            {
                dataContract = new AuthenticationContract();
                //dataContract.AuthenticationId = SQLDBHelper.GetInt64Value(reader["AuthenticationId"]);
                dataContract.Password = SQLDBHelper.GetStringValue(reader["Password"]);
                //dataContract.PotentialId = SQLDBHelper.GetInt64Value(reader["PotentialId"]);
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
