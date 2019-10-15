using Architecture.Common.Types;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Architecture.Base
{
    public sealed class DBLayer : ObjectHelperBase, IDBLayer
    {
        private const string dataSource = "VAIO\\DOMINATION";
        private const string UserID = "sa";
        private const string Password = "admin";
        public static SqlTransaction myTransaction;
        public static SqlConnection con;
        
        /// <summary>
        /// Sql procedure çalıştıracak SqlCommand oluşturur.
        /// </summary>
        /// <param name="databaseName"></param>
        /// <param name="storedProcedureName"></param>
        /// <returns></returns>
        public SqlCommand GetDBCommand(SQLDBHelper.Databases databaseName, string storedProcedureName)
        {
            try
            {
                using (con)
                {
                    SetDBConnection(dataSource, databaseName.ToString(), UserID, Password);
                    myTransaction = con.BeginTransaction();
                    using (SqlCommand cmd = new SqlCommand(storedProcedureName, con,myTransaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        return cmd;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// TODO: NORTHWND veritabanına ayarlıdır.
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <returns></returns>
        public SqlCommand GetDBCommand(string storedProcedureName)
        {
            return GetDBCommand(SQLDBHelper.Databases.NORTHWND, storedProcedureName);
        }

        /// <summary>
        /// Sql metini çalıştırarak SqlCommand oluşturur.
        /// </summary>
        /// <param name="databaseName"></param>
        /// <param name="sqlString"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public SqlCommand GetDBCommand(SQLDBHelper.Databases databaseName, string sqlString,CommandType type)
        {
            try
            {
                SetDBConnection(dataSource, databaseName.ToString(), UserID, Password);
                myTransaction = con.BeginTransaction();
                using (SqlCommand cmd = new SqlCommand(sqlString, con,myTransaction))
                {
                    cmd.CommandType = CommandType.Text;
                    return cmd;
                }
            }
            catch
            {
                throw;
            }
        }
        
        /// <summary>
        /// Stored procedure  ve command cümleleri için SqlDbType ile  parametre eklemek için kullanılır. 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="parameterName"></param>
        /// <param name="dbType"></param>
        /// <param name="value"></param>
        public void AddInParameter(SqlCommand cmd, string parameterName, SqlDbType dbType, object value)
        {
            try
            {
                SqlParameter parameter = new SqlParameter(parameterName, dbType);
                parameter.Value = new object();
                parameter.Value = value;
                cmd.Parameters.Add(parameter);
            }
            catch { throw; }
        }

        /// <summary>
        /// Stored procedure  ve command cümleleri için parametre eklemek için kullanılır. 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="parameterName"></param>
        /// <param name="dbType"></param>
        /// <param name="value"></param>
        public void AddInParameter(SqlCommand cmd, string parameterName, object value)
        {
            try
            {
                cmd.Parameters.AddWithValue(parameterName,value);
            }
            catch { throw; }
        }

        public void AddOutParameter(SqlCommand cmd, string parameterName, SqlDbType dbType)
        {
            try
            {
                SqlParameter parameter = new SqlParameter(parameterName, dbType);
                parameter.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(parameter);
            }
            catch { throw; }
        }

        /// <summary>
        /// Reader
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public GenericResponse<SqlDataReader> ExecuteReader(SqlCommand command)
        {
            GenericResponse<SqlDataReader> returnObject;
            returnObject = InitializeGenericResponse<SqlDataReader>("ExecuteReader");
            try
            {
                using (command)
                {
                    returnObject.Value = command.ExecuteReader();
                }
            }
            catch (Exception e)
            {
                returnObject.Results.Add(e);
            }
            return returnObject;
        }

        /// <summary>
        /// Dataset ve datagridler için SqlDataAdapter nesnesi döner.
        /// Örnek kullanımı da.Fill(ds)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public GenericResponse<SqlDataAdapter> ExecuteDataAdapter(SqlCommand command)
        {
            GenericResponse<SqlDataAdapter> returnObject;
            returnObject = InitializeGenericResponse<SqlDataAdapter>("ExecuteDataAdapter");
            try
            {
                returnObject.Value = new SqlDataAdapter(command);
            }
            catch (Exception e)
            {
                returnObject.Results.Add(e);
            }
            finally
            {
                con.Dispose();
            }
            return returnObject;
        }

        /// <summary>
        /// Insert, Update,Delete işlemi 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public GenericResponse<Int32> ExecuteNonQuery(SqlCommand command)
        {
            GenericResponse<Int32> returnObject;
            returnObject = InitializeGenericResponse<Int32>("ExecuteNonQuery");
            try
            {
                using (command)
                {
                    returnObject.Value = command.ExecuteNonQuery();
                    myTransaction.Commit();
                }
            }
            catch (Exception e)
            {
                myTransaction.Rollback();
                returnObject.Results.Add(e);
            }
            finally
            {
                con.Dispose();
            }
            return returnObject;
        }

        /// <summary>
        /// Integer değer dönderir.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public GenericResponse<int> ExecuteScalar(SqlCommand command)
        {
            int sayi = 0;
            GenericResponse<int> result = InitializeGenericResponse<Int32>("ExecuteScalar");
            try
            {
                using (command)
                {
                    result.Value = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            catch (Exception e)
            {
                result.Results.Add(e);
            }
            finally
            {
                con.Dispose();
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="initialCatalog"></param>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private void SetDBConnection(string dataSource,string initialCatalog,string userID,string password)
        {
            con = new SqlConnection();
            try
            {
                SqlConnectionStringBuilder conString = new SqlConnectionStringBuilder()
                {
                    DataSource = dataSource,
                    UserID = userID,
                    Password = password,
                    InitialCatalog = initialCatalog,
                    IntegratedSecurity = false,// true olursa Windows Authentication kullanır
                    MultipleActiveResultSets = false,
                    PersistSecurityInfo = false,
                    Pooling = true,
                    MaxPoolSize = 15
                };
                con = new SqlConnection(conString.ConnectionString);
                con.Open();
            }
            catch (SqlException err)
            {
                if (err.Number == 233)
                {
                    SqlConnection.ClearAllPools();
                }
            }
        }
        
    }
}
