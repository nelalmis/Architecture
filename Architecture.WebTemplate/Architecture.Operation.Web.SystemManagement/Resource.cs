using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Architecture.Service.Root.BusinessHelper;
using Architecture.Base;
using Architecture.Common.Types;
using Architecture.Data;

namespace Architecture.Operation.Web.SystemManagement
{
    public partial class Resource
    {
        public GenericResponse<List<ResourceContract>> Select(ObjectHelper objectHelper, ResourceRequest request)
        {
            GenericResponse<List<ResourceContract>> returnObject = objectHelper.InitializeGenericResponse<List<ResourceContract>>("SelectByColumns");
            Service.Root.BusinessHelper.Resource service = new Service.Root.BusinessHelper.Resource();

            GenericResponse<List<ResourceContract>> response = service.SelectByColumns(objectHelper,request);
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
