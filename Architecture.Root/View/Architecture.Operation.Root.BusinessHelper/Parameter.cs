using Architecture.Base;
using Architecture.Common.Types;
using Architecture.Types.Root.BusinessHelper;
using System.Collections.Generic;

namespace Architecture.Operation.Root.BusinessHelper
{
    public partial class Parameter
    {
        public GenericResponse<List<ParameterContract>> SelectByColumns(ObjectHelper objectHelper, ParameterRequest request)
        {
            GenericResponse<List<ParameterContract>> returnObject = objectHelper.InitializeGenericResponse<List<ParameterContract>>("SelectByColumns");
            Architecture.DataAccess.Root.BusinessHelper.Parameter bo = new Architecture.DataAccess.Root.BusinessHelper.Parameter(objectHelper.ExecutionDataContext);

            GenericResponse<List<ParameterContract>> response = bo.SelectByColumns(request.ParamType,request.ParamCode);
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
