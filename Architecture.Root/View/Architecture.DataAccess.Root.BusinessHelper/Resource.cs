using Architecture.Base;
using Architecture.Common.Types;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Architecture.DataAccess.Root.BusinessHelper
{
    public partial class Resource: ObjectHelper
    {
        public GenericResponse<List<ResourceContract>> SelectByColumns(string username,string password, int? resourceId,string resourceCode)
        {   
            SqlCommand command;
            GenericResponse<List<ResourceContract>> returnObject;
            GenericResponse<SqlDataReader> sp;
            returnObject = this.InitializeGenericResponse<List<ResourceContract>>("");

            command = this.DBLayer.GetDBCommand(SQLDBHelper.Databases.ARCHITECTURE, "COR.sel_ResourceByColumns");
            this.DBLayer.AddInParameter(command, "@UserName", SqlDbType.NVarChar, username);
            this.DBLayer.AddInParameter(command, "@Password", SqlDbType.NVarChar, password);
            this.DBLayer.AddInParameter(command, "@ResourceId", SqlDbType.Int, resourceId);
            this.DBLayer.AddInParameter(command, "@Code", SqlDbType.NVarChar, resourceCode);


            sp = this.DBLayer.ExecuteReader(command);
            if (!sp.Success)
            {
                returnObject.Results.AddRange(sp.Results);
                return returnObject;
            }

            #region Fill from SqlDataReader to List

            List<ResourceContract> listOfDataContract = new List<ResourceContract>();
            ResourceContract dataContract = null;
            SqlDataReader reader = sp.Value;

            while (reader.Read())
            {
                dataContract = new ResourceContract();
                dataContract.Company = new CompanyContract();
                dataContract.Parent = new ResourceContract();
                
                dataContract.CompanyId = SQLDBHelper.GetInt32Value(reader["CompanyId"]);
                dataContract.AssemblyName = SQLDBHelper.GetStringValue(reader["AssemblyName"]);
                dataContract.ControllerName = SQLDBHelper.GetStringValue(reader["ControllerName"]);
                dataContract.ViewName = SQLDBHelper.GetStringValue(reader["ViewName"]);
                dataContract.Code = SQLDBHelper.GetStringValue(reader["Code"]);
                dataContract.Description = SQLDBHelper.GetStringValue(reader["ResourceDescription"]);
                dataContract.HostIp = SQLDBHelper.GetStringValue(reader["HostIp"]);
                dataContract.HostName = SQLDBHelper.GetStringValue(reader["HostName"]);
                dataContract.Icon = SQLDBHelper.GetStringValue(reader["Icon"]);
                dataContract.MenuType = SQLDBHelper.GetByteNullableValue(reader["MenuType"]);
                dataContract.MenuTypeName = SQLDBHelper.GetStringValue(reader["MenuTypeName"]);
                dataContract.ParentId = SQLDBHelper.GetInt32NullableValue(reader["ParentId"]);
                dataContract.ParentName = SQLDBHelper.GetStringValue(reader["ParentName"]);
                dataContract.ModuleId = SQLDBHelper.GetInt32NullableValue(reader["ModuleId"]);
                dataContract.ModuleName = SQLDBHelper.GetStringValue(reader["ModuleName"]);
                dataContract.ResourceId = SQLDBHelper.GetInt32Value(reader["ResourceId"]);
                dataContract.SortId = SQLDBHelper.GetByteNullableValue(reader["ResourceSortId"]);
                dataContract.SystemDate = SQLDBHelper.GetDateTimeNullableValue(reader["SystemDate"]);
                dataContract.Text = SQLDBHelper.GetStringValue(reader["Text"]);
                dataContract.ViewType = SQLDBHelper.GetByteNullableValue(reader["ViewType"]);
                dataContract.ViewTypeName = SQLDBHelper.GetStringValue(reader["ViewTypeName"]);
                dataContract.UpdateSystemDate = SQLDBHelper.GetDateTimeNullableValue(reader["UpdateSystemDate"]);
                dataContract.UpdateUserName = SQLDBHelper.GetStringValue(reader["UpdateUserName"]);
                dataContract.UserName = SQLDBHelper.GetStringValue(reader["UserName"]);
                dataContract.Company.CompanyFullName = SQLDBHelper.GetStringValue(reader["CompanyFullName"]);
                dataContract.Company.CompanyIcon = SQLDBHelper.GetStringValue(reader["CompanyIcon"]);
                dataContract.Company.CompanyId = SQLDBHelper.GetInt32Value(reader["CompanyId"]);
                dataContract.Company.CompanyName = SQLDBHelper.GetStringValue(reader["CompanyName"]);

                listOfDataContract.Add(dataContract);

            }

            reader.Close();
            //Return 

            returnObject.Value = listOfDataContract;

            #endregion

            return returnObject;
        }
        public GenericResponse<List<ResourceActionContract>> SelectActionByResourceId(int resourceId)
        {
            SqlCommand command;
            GenericResponse<List<ResourceActionContract>> returnObject;
            GenericResponse<SqlDataReader> sp;
            returnObject = this.InitializeGenericResponse<List<ResourceActionContract>>("");

            command = this.DBLayer.GetDBCommand(SQLDBHelper.Databases.ARCHITECTURE, "COR.sel_ResourceActionByColumns");
            this.DBLayer.AddInParameter(command, "@ResourceId", SqlDbType.Int, resourceId);

            sp = this.DBLayer.ExecuteReader(command);
            if (!sp.Success)
            {
                returnObject.Results.AddRange(sp.Results);
                return returnObject;
            }

            #region Fill from SqlDataReader to List

            List<ResourceActionContract> listOfDataContract = new List<ResourceActionContract>();
            ResourceActionContract dataContract = null;
            SqlDataReader reader = sp.Value;

            while (reader.Read())
            {
                dataContract = new ResourceActionContract();
                dataContract.ActionId = SQLDBHelper.GetInt32Value(reader["ActionId"]);
                dataContract.ActionType = SQLDBHelper.GetByteNullableValue(reader["ActionType"]);
                dataContract.ActionTypeName = SQLDBHelper.GetStringValue(reader["ActionTypeName"]);
                dataContract.CommandName = SQLDBHelper.GetStringValue(reader["CommandName"]);
                dataContract.Description = SQLDBHelper.GetStringValue(reader["ActionDescription"]);
                dataContract.DisplayName = SQLDBHelper.GetStringValue(reader["DisplayName"]);
                dataContract.Icon = SQLDBHelper.GetStringValue(reader["Icon"]);
                dataContract.SortId = SQLDBHelper.GetByteNullableValue(reader["SortId"]);
                dataContract.ResourceActionId= SQLDBHelper.GetInt32Value(reader["ResourceActionId"]);
                dataContract.ResourceId= SQLDBHelper.GetInt32Value(reader["ResourceId"]);

                listOfDataContract.Add(dataContract);

            }

            reader.Close();
            //Return 

            returnObject.Value = listOfDataContract;

            #endregion

            return returnObject;
        }
        public GenericResponse<List<ActionContract>> SelectAction()
        {
            SqlCommand command;
            GenericResponse<List<ActionContract>> returnObject;
            GenericResponse<SqlDataReader> sp;
            returnObject = this.InitializeGenericResponse<List<ActionContract>>("");

            command = this.DBLayer.GetDBCommand(SQLDBHelper.Databases.ARCHITECTURE, "COR.sel_Action");

            sp = this.DBLayer.ExecuteReader(command);
            if (!sp.Success)
            {
                returnObject.Results.AddRange(sp.Results);
                return returnObject;
            }

            #region Fill from SqlDataReader to List

            List<ActionContract> listOfDataContract = new List<ActionContract>();
            ActionContract dataContract = null;
            SqlDataReader reader = sp.Value;

            while (reader.Read())
            {
                dataContract = new ActionContract();
                dataContract.ActionId = SQLDBHelper.GetInt32Value(reader["ActionId"]);
                dataContract.ActionType = SQLDBHelper.GetByteValue(reader["ActionType"]);
                dataContract.ActionTypeName = SQLDBHelper.GetStringValue(reader["ActionTypeName"]);
                dataContract.CommandName = SQLDBHelper.GetStringValue(reader["CommandName"]);
                dataContract.Description = SQLDBHelper.GetStringValue(reader["ActionDescription"]);
                dataContract.DisplayName = SQLDBHelper.GetStringValue(reader["DisplayName"]);
                dataContract.Icon = SQLDBHelper.GetStringValue(reader["Icon"]);
                dataContract.SortId = SQLDBHelper.GetByteNullableValue(reader["SortId"]);

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
