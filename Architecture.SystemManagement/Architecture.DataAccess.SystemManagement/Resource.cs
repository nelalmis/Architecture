using Architecture.Base;
using Architecture.Common.Types;
using Architecture.Data;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Architecture.DataAccess.SystemManagement
{
    public partial class Resource : ObjectHelper
    {
        public GenericResponse<Int32> Insert(ResourceContract contract)
        {
            SqlCommand command;
            GenericResponse<Int32> returnObject;
            GenericResponse<Int32> spInsert;

            returnObject = this.InitializeGenericResponse<Int32>("");
            command = this.DBLayer.GetDBCommand(SQLDBHelper.Databases.ARCHITECTURE, "COR.ins_Resource");

            if (contract == null)
            {
                returnObject.Results.Add(new ArgumentNullException("contract"));
                return returnObject;
            }
            this.DBLayer.AddOutParameter(command, "@ResourceId", SqlDbType.Int);
            this.DBLayer.AddInParameter(command, "@AssemblyName", SqlDbType.NVarChar, contract.AssemblyName);
           // this.DBLayer.AddInParameter(command, "@ClassName", SqlDbType.NVarChar, contract.Class);
            this.DBLayer.AddInParameter(command, "@Code", SqlDbType.NVarChar, contract.Code);
            this.DBLayer.AddInParameter(command, "@Text", SqlDbType.NVarChar, contract.Text);
            this.DBLayer.AddInParameter(command, "@Icon", SqlDbType.NVarChar, contract.Icon);
            this.DBLayer.AddInParameter(command, "@ModuleId", SqlDbType.Int, contract.ModuleId);
            this.DBLayer.AddInParameter(command, "@ParentId", SqlDbType.Int, contract.ParentId);
            this.DBLayer.AddInParameter(command, "@MenuType", SqlDbType.TinyInt, contract.MenuType);
            this.DBLayer.AddInParameter(command, "@SortId", SqlDbType.TinyInt, contract.SortId);
            this.DBLayer.AddInParameter(command, "@ViewType", SqlDbType.TinyInt, contract.ViewType);
            this.DBLayer.AddInParameter(command, "@Description", SqlDbType.NVarChar, contract.Description);
            this.DBLayer.AddInParameter(command, "@UserName", SqlDbType.NVarChar, contract.UserName);
            this.DBLayer.AddInParameter(command, "@HostName", SqlDbType.NVarChar, contract.HostName);
            this.DBLayer.AddInParameter(command, "@SystemDate", SqlDbType.DateTime, contract.SystemDate);
            this.DBLayer.AddInParameter(command, "@HostIp", SqlDbType.NVarChar, contract.HostIp);
            

            spInsert = this.DBLayer.ExecuteNonQuery(command);
            if (!spInsert.Success)
            {
                returnObject.Results.AddRange(spInsert.Results);
                return returnObject;
            }
            returnObject.Value = this.SQLDBHelper.GetInt32Value(command.Parameters["@ResourceId"].Value);
            return returnObject;
        }

        public GenericResponse<Int32> Update(ResourceContract contract)
        {
            SqlCommand command;
            GenericResponse<Int32> returnObject;
            GenericResponse<Int32> spUpdate;

            returnObject = this.InitializeGenericResponse<Int32>("");
            command = this.DBLayer.GetDBCommand(SQLDBHelper.Databases.ARCHITECTURE, "COR.upd_Resource");

            if (contract == null)
            {
                returnObject.Results.Add(new ArgumentNullException("contract"));
                return returnObject;
            }
            this.DBLayer.AddInParameter(command, "@ResourceId", SqlDbType.Int, contract.ResourceId);
            this.DBLayer.AddInParameter(command, "@AssemblyName", SqlDbType.NVarChar, contract.AssemblyName);
            //this.DBLayer.AddInParameter(command, "@ClassName", SqlDbType.NVarChar, contract.ClassName);
            this.DBLayer.AddInParameter(command, "@Code", SqlDbType.NVarChar, contract.Code);
            this.DBLayer.AddInParameter(command, "@Text", SqlDbType.NVarChar, contract.Text);
            this.DBLayer.AddInParameter(command, "@Icon", SqlDbType.NVarChar, contract.Icon);
            this.DBLayer.AddInParameter(command, "@ModuleId", SqlDbType.Int, contract.ModuleId);
            this.DBLayer.AddInParameter(command, "@ParentId", SqlDbType.Int, contract.ParentId);
            this.DBLayer.AddInParameter(command, "@MenuType", SqlDbType.TinyInt, contract.MenuType);
            this.DBLayer.AddInParameter(command, "@SortId", SqlDbType.TinyInt, contract.SortId);
            this.DBLayer.AddInParameter(command, "@ViewType", SqlDbType.TinyInt, contract.ViewType);
            this.DBLayer.AddInParameter(command, "@Description", SqlDbType.NVarChar, contract.Description);
            this.DBLayer.AddInParameter(command, "@UpdateUserName", SqlDbType.NVarChar, contract.UpdateUserName);
            this.DBLayer.AddInParameter(command, "@UpdateSystemDate", SqlDbType.DateTime, contract.UpdateSystemDate);
            this.DBLayer.AddInParameter(command, "@HostIp", SqlDbType.NVarChar, contract.HostIp);

            spUpdate = this.DBLayer.ExecuteNonQuery(command);

            if (!spUpdate.Success)
            {
                returnObject.Results.AddRange(spUpdate.Results);
                return returnObject;
            }
            returnObject.Value = spUpdate.Value;
            return returnObject;
        }
        
        public GenericResponse<Int32> InsertResourceAction(ResourceActionContract contract)
        {
            SqlCommand command;
            GenericResponse<Int32> returnObject;
            GenericResponse<Int32> spInsert;

            returnObject = this.InitializeGenericResponse<Int32>("");
            command = this.DBLayer.GetDBCommand(SQLDBHelper.Databases.ARCHITECTURE, "COR.ins_ResourceAction");

            if (contract == null)
            {
                returnObject.Results.Add(new ArgumentNullException("contract"));
                return returnObject;
            }
            this.DBLayer.AddInParameter(command, "@ResourceId", SqlDbType.Int, contract.ResourceId);
            this.DBLayer.AddInParameter(command, "@ActionId", SqlDbType.Int, contract.ActionId);
            this.DBLayer.AddInParameter(command, "@ActionType", SqlDbType.TinyInt, contract.ActionType);
            this.DBLayer.AddInParameter(command, "@CommandName", SqlDbType.NVarChar, contract.CommandName);
            this.DBLayer.AddInParameter(command, "@Description", SqlDbType.NVarChar, contract.Description);
            this.DBLayer.AddInParameter(command, "@DisplayName", SqlDbType.NVarChar, contract.DisplayName);
            this.DBLayer.AddInParameter(command, "@Icon", SqlDbType.NVarChar, contract.Icon);
            this.DBLayer.AddInParameter(command, "@SortId", SqlDbType.TinyInt, contract.SortId);

            spInsert = this.DBLayer.ExecuteNonQuery(command);

            if (!spInsert.Success)
            {
                returnObject.Results.AddRange(spInsert.Results);
                return returnObject;
            }
            returnObject.Value = spInsert.Value;
            return returnObject;
        }

        public GenericResponse<Int32> Delete(int resourceId)
        {
            SqlCommand command;
            GenericResponse<Int32> returnObject;
            GenericResponse<Int32> spInsert;

            returnObject = this.InitializeGenericResponse<Int32>("");
            command = this.DBLayer.GetDBCommand(SQLDBHelper.Databases.ARCHITECTURE, "COR.del_Resource");

            this.DBLayer.AddInParameter(command, "@ResourceId", SqlDbType.Int, resourceId);

            spInsert = this.DBLayer.ExecuteNonQuery(command);

            if (!spInsert.Success)
            {
                returnObject.Results.AddRange(spInsert.Results);
                return returnObject;
            }
            returnObject.Value = spInsert.Value;
            return returnObject;
        }

        public GenericResponse<Int32> DeleteResourceAction(int resourceId)
        {
            SqlCommand command;
            GenericResponse<Int32> returnObject;
            GenericResponse<Int32> spInsert;

            returnObject = this.InitializeGenericResponse<Int32>("");
            command = this.DBLayer.GetDBCommand(SQLDBHelper.Databases.ARCHITECTURE, "COR.del_ResourceAction");
            
            this.DBLayer.AddInParameter(command, "@ResourceId", SqlDbType.Int, resourceId);

            spInsert = this.DBLayer.ExecuteNonQuery(command);

            if (!spInsert.Success)
            {
                returnObject.Results.AddRange(spInsert.Results);
                return returnObject;
            }
            returnObject.Value = spInsert.Value;
            return returnObject;
        }
    }
}
