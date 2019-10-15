using Architecture.Base;
using Architecture.Common.Types;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Architecture.DataAccess.Root.BusinessHelper
{
    public partial class Country:ObjectHelper
    {
        public Country(ExecutionDataContext context) : base(context) { }

        public GenericResponse<List<CountryContract>> Select()
        {
            SqlCommand command;
            GenericResponse<List<CountryContract>> returnObject;
            GenericResponse<SqlDataReader> sp;
            returnObject = this.InitializeGenericResponse<List<CountryContract>>("");

            command = this.DBLayer.GetDBCommand(SQLDBHelper.Databases.DB_RNA, "dbo.sel_CountryByColumns");

            sp = this.DBLayer.ExecuteReader(command);
            if (!sp.Success)
            {
                returnObject.Results.AddRange(sp.Results);
                return returnObject;
            }

            #region Fill from SqlDataReader to List

            List<CountryContract> listOfDataContract = new List<CountryContract>();
            CountryContract dataContract = null;

            SqlDataReader reader = sp.Value;

            while (reader.Read())
            {
                dataContract = new CountryContract();
                
                dataContract.CountryId = SQLDBHelper.GetInt32Value(reader["CountryId"]);
                dataContract.CountryCode = SQLDBHelper.GetStringValue(reader["CountryCode"]);
                dataContract.CountryName = SQLDBHelper.GetStringValue(reader["CountryName"]);
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
