using Architecture.Common.Types;
using System.Data;
using System.Data.SqlClient;

namespace Architecture.Base
{
    /// <summary>
    /// Sql Connection Cümleleri için 
    /// </summary>
    public interface IDBLayer
    {
        SqlCommand GetDBCommand(SQLDBHelper.Databases databaseName, string storedProcedureName);
        SqlCommand GetDBCommand(SQLDBHelper.Databases databaseName, string sqlString, CommandType type = CommandType.Text);
        SqlCommand GetDBCommand(string storedProcedureName);
        void AddInParameter(SqlCommand cmd, string parameterName, SqlDbType dbType, object value);
        void AddInParameter(SqlCommand cmd, string parameterName, object value);
        GenericResponse<SqlDataReader> ExecuteReader(SqlCommand command);
        GenericResponse<SqlDataAdapter> ExecuteDataAdapter(SqlCommand command);
        GenericResponse<int> ExecuteNonQuery(SqlCommand command);
        GenericResponse<int> ExecuteScalar(SqlCommand command);
        
    }
}
