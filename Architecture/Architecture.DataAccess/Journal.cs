using Architecture.Base;
using Architecture.Common.Types;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Architecture.DataAccess
{
    public partial class Journal:ObjectHelper
    {
        public Journal(ExecutionDataContext context) : base(context) { }

        public GenericResponse<Int32> Insert(JournalContract contract)
        {
            SqlCommand command;
            GenericResponse<Int32> returnObject;
            GenericResponse<Int32> spInsert;

            returnObject = InitializeGenericResponse<Int32>("");
            command = this.DBLayer.GetDBCommand(SQLDBHelper.Databases.ARCHITECTURELOG, "COR.ins_Journal");

            if (contract == null)
            {
                returnObject.Results.Add(new ArgumentNullException("contract"));
                return returnObject;
            }

            this.DBLayer.AddInParameter(command, "@OperationKey", SqlDbType.NVarChar, contract.OperationKey);
            this.DBLayer.AddInParameter(command, "@ExecutionTree", SqlDbType.VarBinary, contract.ExecutionTree);
            this.DBLayer.AddInParameter(command, "@HasException", SqlDbType.TinyInt, contract.HasException);
            this.DBLayer.AddInParameter(command, "@HostIp", SqlDbType.NVarChar, contract.HostIp);
            this.DBLayer.AddInParameter(command, "@HostName", SqlDbType.NVarChar, contract.HostName);
            this.DBLayer.AddInParameter(command, "@LanguageId", SqlDbType.TinyInt, contract.LanguageId);
            this.DBLayer.AddInParameter(command, "@Request", SqlDbType.VarBinary, SQLDBHelper.BinarySerialize(contract.Request));
            this.DBLayer.AddInParameter(command, "@Response", SqlDbType.VarBinary, SQLDBHelper.BinarySerialize(contract.Response));
            this.DBLayer.AddInParameter(command, "@SystemDate", SqlDbType.DateTime, contract.SystemDate);
            this.DBLayer.AddInParameter(command, "@TranDate", SqlDbType.DateTime, contract.TranDate);
            this.DBLayer.AddInParameter(command, "@TransactionName", SqlDbType.NVarChar, contract.TransactionName);
            this.DBLayer.AddInParameter(command, "@UpdateSystemDate", SqlDbType.DateTime, contract.UpdateSystemDate);
            this.DBLayer.AddInParameter(command, "@UpdateUserName", SqlDbType.NVarChar, contract.UpdateUserName);
            this.DBLayer.AddInParameter(command, "@UserName", SqlDbType.NVarChar, contract.UserName);
            
            spInsert = this.DBLayer.ExecuteNonQuery(command);

            if (!spInsert.Success)
            {
                returnObject.Results.AddRange(spInsert.Results);
                return returnObject;
            }
            returnObject.Value = spInsert.Value;
            return returnObject;
        }

        public GenericResponse<JournalContract> SelectByOperationKey(string operationKey)
        {
            SqlCommand command;
            GenericResponse<JournalContract> returnObject;
            GenericResponse<SqlDataReader> sp;
            returnObject = InitializeGenericResponse<JournalContract>("");

            command = this.DBLayer.GetDBCommand(SQLDBHelper.Databases.ARCHITECTURELOG, "COR.sel_JournalByOperationKey");

            this.DBLayer.AddInParameter(command, "@OperationKey", SqlDbType.NVarChar, operationKey);

            sp = this.DBLayer.ExecuteReader(command);
            if (!sp.Success)
            {
                returnObject.Results.AddRange(sp.Results);
                return returnObject;
            }

            #region Fill from SqlDataReader to List

            JournalContract dataContract = null;

            SqlDataReader reader = sp.Value;

            while (reader.Read())
            {
                dataContract = new JournalContract();
                dataContract.OperationKey = SQLDBHelper.GetStringValue(reader["OperationKey"]);
                dataContract.ExecutionTree = SQLDBHelper.GetBinaryValue(reader["ExecutionTree"]);
                dataContract.HasException = SQLDBHelper.GetBooleanNullableValue(reader["HasException"]);
                dataContract.HostIp = SQLDBHelper.GetStringValue(reader["HostIp"]);
                dataContract.HostName = SQLDBHelper.GetStringValue(reader["HostName"]);
                dataContract.LanguageId = SQLDBHelper.GetByteNullableValue(reader["LanguageId"]);
                dataContract.RequestBinary = SQLDBHelper.GetBinaryValue(reader["Request"]);
                dataContract.ResponseBinary = SQLDBHelper.GetBinaryValue(reader["Response"]);
                dataContract.SystemDate = SQLDBHelper.GetDateTimeNullableValue(reader["SystemDate"]);
                dataContract.TranDate = SQLDBHelper.GetDateTimeNullableValue(reader["TranDate"]);
                dataContract.TransactionName = SQLDBHelper.GetStringValue(reader["TransactionName"]);
                dataContract.UpdateSystemDate = SQLDBHelper.GetDateTimeNullableValue(reader["UpdateSystemDate"]);
                dataContract.UpdateUserName = SQLDBHelper.GetStringValue(reader["UpdateUserName"]);
                dataContract.UserName = SQLDBHelper.GetStringValue(reader["UserName"]);

                break;
            }
            reader.Close();
            //Return 

            returnObject.Value = dataContract;

            #endregion

            return returnObject;
        }

        public GenericResponse<Int32> Update(JournalContract contract)
        {
            SqlCommand command;
            GenericResponse<Int32> returnObject;
            GenericResponse<Int32> spUpdate;

            returnObject = InitializeGenericResponse<Int32>("");
            command = this.DBLayer.GetDBCommand(SQLDBHelper.Databases.ARCHITECTURELOG, "COR.upd_Journal");

            if (contract == null)
            {
                returnObject.Results.Add(new ArgumentNullException("contract"));
                return returnObject;
            }

            this.DBLayer.AddInParameter(command, "@OperationKey", SqlDbType.NVarChar, contract.OperationKey);
            this.DBLayer.AddInParameter(command, "@ExecutionTree", SqlDbType.Binary, contract.ExecutionTree);
            this.DBLayer.AddInParameter(command, "@HasException", SqlDbType.TinyInt, contract.HasException);
            this.DBLayer.AddInParameter(command, "@Response", SqlDbType.Binary, contract.ResponseBinary);
            this.DBLayer.AddInParameter(command, "@UpdateSystemDate", SqlDbType.DateTime, contract.UpdateSystemDate);
            this.DBLayer.AddInParameter(command, "@UpdateUserName", SqlDbType.NVarChar, contract.UpdateUserName);

            spUpdate = this.DBLayer.ExecuteNonQuery(command);

            if (!spUpdate.Success)
            {
                returnObject.Results.AddRange(spUpdate.Results);
                return returnObject;
            }
            returnObject.Value = spUpdate.Value;
            return returnObject;
        }

    }
}
