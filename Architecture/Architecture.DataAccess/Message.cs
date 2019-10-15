using Architecture.Base;
using Architecture.Common.Types;
using System.Data;
using System.Data.SqlClient;

namespace Architecture.DataAccess
{
    public partial class Message:ObjectHelper
    {
        public Message(ExecutionDataContext ct) : base(ct) { }
        public GenericResponse<MessageContract> SelectByColumns(MessageContract contract)
        {
            SqlCommand command;
            GenericResponse<MessageContract> returnObject;
            GenericResponse<SqlDataReader> sp;
            returnObject = InitializeGenericResponse<MessageContract>("");

            command = this.DBLayer.GetDBCommand(SQLDBHelper.Databases.ARCHITECTURE, "COR.sel_MessageByColumns");

            this.DBLayer.AddInParameter(command, "@MessageCode", SqlDbType.NVarChar, contract.MessageCode);
            this.DBLayer.AddInParameter(command, "@GroupName", SqlDbType.NVarChar, contract.GroupName);
            this.DBLayer.AddInParameter(command, "@ClassName", SqlDbType.NVarChar, contract.ClassName);
            this.DBLayer.AddInParameter(command, "@PropertyName", SqlDbType.NVarChar, contract.PropertyName);


            sp = this.DBLayer.ExecuteReader(command);
            if (!sp.Success)
            {
                returnObject.Results.AddRange(sp.Results);
                return returnObject;
            }

            #region Fill from SqlDataReader to List
            
             MessageContract dataContract = null;

            SqlDataReader reader = sp.Value;

            while (reader.Read())
            {
                dataContract = new MessageContract();
                dataContract.ClassName = SQLDBHelper.GetStringValue(reader["ClassName"]);
                dataContract.GroupName = SQLDBHelper.GetStringValue(reader["GroupName"]);
                dataContract.MessageCode = SQLDBHelper.GetStringValue(reader["MessageCode"]);
                dataContract.MessageId = SQLDBHelper.GetInt32Value(reader["MessageId"]);
                dataContract.MessageText = SQLDBHelper.GetStringValue(reader["MessageText"]);
                dataContract.PropertyName = SQLDBHelper.GetStringValue(reader["PropertyName"]);
                break;
            }

            reader.Close();
            //Return 

            returnObject.Value = dataContract;

            #endregion

            return returnObject;
        }

    }
}
