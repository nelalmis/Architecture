using Architecture.Base;
using Architecture.Common.Types;
using System.Collections.Generic;

namespace Architecture.Operation.Root.BusinessHelper
{
    public partial class Resource
    {
        public GenericResponse<List<ResourceContract>> SelectByColumns(ObjectHelper objectHelper, ResourceRequest request)
        {
            GenericResponse<List<ResourceContract>> returnObject = objectHelper.InitializeGenericResponse<List<ResourceContract>>("SelectByColumns");
             Architecture.DataAccess.Root.BusinessHelper.Resource bo = new Architecture.DataAccess.Root.BusinessHelper.Resource();
            
            GenericResponse<List<ResourceContract>> response = bo.SelectByColumns(request.UserName,request.Password,request.ResourceId,request.ResourceCode);
            if (!response.Success)
            {
                returnObject.Results.AddRange(response.Results);
                return returnObject;
            }
            foreach (var item in response.Value)
            {
                GenericResponse<List<ResourceActionContract>> responseAction = bo.SelectActionByResourceId(item.ResourceId);
                if (!responseAction.Success)
                {
                    returnObject.Results.AddRange(responseAction.Results);
                    return returnObject;
                }
                item.ResourceActionList = responseAction.Value;
            }

            returnObject.Value = response.Value;
            return returnObject;
        }

        public GenericResponse<List<ActionContract>> SelectAction(ObjectHelper objectHelper, ResourceRequest request)
        {
            GenericResponse<List<ActionContract>> returnObject = objectHelper.InitializeGenericResponse<List<ActionContract>>("SelectByColumns");
            Architecture.DataAccess.Root.BusinessHelper.Resource bo = new Architecture.DataAccess.Root.BusinessHelper.Resource();

            GenericResponse<List<ActionContract>> response = bo.SelectAction();
            if (!response.Success)
            {
                returnObject.Results.AddRange(response.Results);
                return returnObject;
            }
            
            returnObject.Value = response.Value;
            return returnObject;
        }

    }
}
