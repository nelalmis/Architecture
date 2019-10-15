using Architecture.Base;
using Architecture.Common.Types;
using Architecture.DataAccess;
using Architecture.Data;
using Architecture.Types.SystemManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Operation.SystemManagement
{
    public partial class Resource
    {
        public GenericResponse<Int32> Insert(ObjectHelper objectHelper, ResourceRequest request)
        {
            GenericResponse<Int32> returnObject = objectHelper.InitializeGenericResponse<Int32>("Insert");
            Architecture.DataAccess.SystemManagement.Resource da = new Architecture.DataAccess.SystemManagement.Resource();

            GenericResponse<Int32> response = da.Insert(request.Contract);
            if (!response.Success)
            {
                returnObject.Results.AddRange(response.Results);
                return returnObject;
            }

            foreach (var item in request.Contract.ResourceActionList)
            {
                item.ResourceId = response.Value;
                GenericResponse<Int32> responseAc = da.InsertResourceAction(item);
                if (!responseAc.Success)
                {
                    returnObject.Results.AddRange(responseAc.Results);
                    return returnObject;
                }
            }

            returnObject.Value = response.Value;
            return returnObject;
        }
        public GenericResponse<Int32> Update(ObjectHelper objectHelper, ResourceRequest request)
        {
            GenericResponse<Int32> returnObject = objectHelper.InitializeGenericResponse<Int32>("Insert");
            Architecture.DataAccess.SystemManagement.Resource da = new Architecture.DataAccess.SystemManagement.Resource();

            GenericResponse<Int32> response = da.Update(request.Contract);
            if (!response.Success)
            {
                returnObject.Results.AddRange(response.Results);
                return returnObject;
            }
            GenericResponse<Int32> responseD = da.DeleteResourceAction(request.Contract.ResourceId);
            if (!responseD.Success)
            {
                returnObject.Results.AddRange(responseD.Results);
                return returnObject;
            }

            foreach (var item in request.Contract.ResourceActionList)
            {
                GenericResponse<Int32> responseAc = da.InsertResourceAction(item);
                if (!responseAc.Success)
                {
                    returnObject.Results.AddRange(responseAc.Results);
                    return returnObject;
                }
            }

            returnObject.Value = response.Value;
            return returnObject;
        }

        public GenericResponse<Int32> Delete(ObjectHelper objectHelper, ResourceRequest request)
        {
            GenericResponse<Int32> returnObject = objectHelper.InitializeGenericResponse<Int32>("Insert");
            Architecture.DataAccess.SystemManagement.Resource da = new Architecture.DataAccess.SystemManagement.Resource();
            Architecture.Service.Root.BusinessHelper.Resource daService = new Architecture.Service.Root.BusinessHelper.Resource();
            GenericResponse<Int32> responseDelete = null;

            ResourceRequest b = new ResourceRequest();
            b.Contract.ResourceId = request.Contract.ResourceId;
            GenericResponse<List<ResourceContract>> responseResource = daService.SelectByColumns(objectHelper,b);
            if (!responseResource.Success)
            {
                returnObject.Results.AddRange(responseResource.Results);
                return returnObject;
            }

            var delList = GetDeleteReourceLeaf(responseResource.Value, request.Contract.ResourceId);
            delList.Add(request.Contract.ResourceId);
            foreach (var item in delList)
            {
                responseDelete = da.Delete(item);
                if (!responseDelete.Success)
                {
                    returnObject.Results.AddRange(responseDelete.Results);
                    return returnObject;
                }

                GenericResponse<Int32> responseDA = da.DeleteResourceAction(item);
                if (!responseDA.Success)
                {
                    returnObject.Results.AddRange(responseDA.Results);
                    return returnObject;
                }

            }
            returnObject.Value = responseDelete.Value;
            return returnObject;
        }
        public GenericResponse<List<ResourceContract>> SelectByColumns(ObjectHelper objectHelper, ResourceRequest request)
        {
            GenericResponse<List<ResourceContract>> returnObject = objectHelper.InitializeGenericResponse<List<ResourceContract>>("SelectByColumns");
            Architecture.Service.Root.BusinessHelper.Resource bo = new Architecture.Service.Root.BusinessHelper.Resource();

            GenericResponse<List<ResourceContract>> response = bo.SelectByColumns(objectHelper,request);
            if (!response.Success)
            {
                returnObject.Results.AddRange(response.Results);
                return returnObject;
            }
            returnObject.Value = response.Value;
            return returnObject;
        }

        public GenericResponse<List<ActionContract>> SelectAction(ObjectHelper objectHelper, ResourceRequest request)
        {
            GenericResponse<List<ActionContract>> returnObject = objectHelper.InitializeGenericResponse<List<ActionContract>>("SelectByColumns");
            Architecture.Service.Root.BusinessHelper.Resource bo = new Architecture.Service.Root.BusinessHelper.Resource();

            GenericResponse<List<ActionContract>> response = bo.SelectAction(objectHelper, request);
            if (!response.Success)
            {
                returnObject.Results.AddRange(response.Results);
                return returnObject;
            }
            
            returnObject.Value = response.Value;
            return returnObject;
        }

        public GenericResponse<ResourceDefinitonWindowContract> SelectWindowBindValues(ObjectHelper objectHelper, ResourceRequest request)
        {
            GenericResponse<ResourceDefinitonWindowContract> returnObject = objectHelper.InitializeGenericResponse<ResourceDefinitonWindowContract>("SelectWindowBindValues");
            Architecture.Service.Root.BusinessHelper.Resource serviceResource = new Architecture.Service.Root.BusinessHelper.Resource();
            Architecture.DataAccess.Root.BusinessHelper.Parameter dacParameter = new Architecture.DataAccess.Root.BusinessHelper.Parameter(objectHelper.ExecutionDataContext);
            returnObject.Value = new ResourceDefinitonWindowContract();
            GenericResponse<List<ResourceContract>> responseResource = serviceResource.SelectByColumns(objectHelper, request);
            if (!responseResource.Success)
            {
                returnObject.Results.AddRange(responseResource.Results);
                return returnObject;
            }
            returnObject.Value.ResourceList = responseResource.Value;

            GenericResponse<List<ActionContract>> responseAction = serviceResource.SelectAction(objectHelper, request);
            if (!responseAction.Success)
            {
                returnObject.Results.AddRange(responseAction.Results);
                return returnObject;
            }
            returnObject.Value.AllActionList = responseAction.Value;


            GenericResponse<List<ParameterContract>> responseMenuType = dacParameter.SelectByColumns("MenuType",null);
            if (!responseMenuType.Success)
            {
                returnObject.Results.AddRange(responseMenuType.Results);
                return returnObject;
            }
            ComboBoxItem citem;
            foreach (var item in responseMenuType.Value)
            {
                citem = new ComboBoxItem();
                citem.Value = (byte)item.ParamValue;
                citem.Text = item.ParamDescription;
                returnObject.Value.MenuTypeList.Add(citem);
            }


            GenericResponse<List<ParameterContract>> responseUIType = dacParameter.SelectByColumns("UIType", null);
            if (!responseUIType.Success)
            {
                returnObject.Results.AddRange(responseUIType.Results);
                return returnObject;
            }

            foreach (var item in responseUIType.Value)
            {
                citem = new ComboBoxItem();
                citem.Value = (byte)item.ParamValue;
                citem.Text = item.ParamDescription;
                returnObject.Value.UITypeList.Add(citem);
            }
            GenericResponse<List<ParameterContract>> responseActionType = dacParameter.SelectByColumns("ActionType", null);
            if (!responseActionType.Success)
            {
                returnObject.Results.AddRange(responseActionType.Results);
                return returnObject;
            }

            foreach (var item in responseActionType.Value)
            {
                citem = new ComboBoxItem();
                citem.Value = (byte)item.ParamValue;
                citem.Text = item.ParamDescription;
                returnObject.Value.ActionTypeList.Add(citem);
            }
            GenericResponse<List<ParameterContract>> responseModuleName = dacParameter.SelectByColumns("ModuleName", null);
            if (!responseModuleName.Success)
            {
                returnObject.Results.AddRange(responseModuleName.Results);
                return returnObject;
            }

            foreach (var item in responseModuleName.Value)
            {
                citem = new ComboBoxItem();
                citem.Value = (byte)item.ParamValue;
                citem.Text = item.ParamCode;
                returnObject.Value.ModuleNameList.Add(citem);
            }

            foreach (var item in responseResource.Value.Where(u => u.ParentId==-1).ToList())
            {
                citem = new ComboBoxItem();
                citem.Value = (byte)item.ResourceId;
                citem.Text = item.Text;
                returnObject.Value.ModuleList.Add(citem);
            }

            return returnObject;
        }

        private List<int> GetDeleteReourceLeaf(List<ResourceContract> resourceList, int resourceId)
        {
            List<int> deleteResourceList = new List<int>();
            
            foreach (var tab in resourceList.Where(u => u.ParentId == resourceId).ToList())
            {
                deleteResourceList.Add(tab.ResourceId);
                foreach (var group in resourceList.Where(u => u.ParentId == tab.ResourceId).ToList())
                {
                    deleteResourceList.Add(group.ResourceId);
                    foreach (var sub in resourceList.Where(u => u.ParentId == group.ResourceId).ToList())
                    {
                        deleteResourceList.Add(sub.ResourceId);
                        foreach (var leaf in resourceList.Where(u => u.ParentId == sub.ResourceId).ToList())
                        {
                            deleteResourceList.Add(leaf.ResourceId);
                        }
                    }
                }
            }
            return deleteResourceList;
        }
    }
}
