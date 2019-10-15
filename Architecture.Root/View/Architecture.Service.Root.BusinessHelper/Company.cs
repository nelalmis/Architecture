using Architecture.Base;
using Architecture.Common.Types;

namespace Architecture.Service.Root.BusinessHelper
{
    public partial class Company
    {
        public GenericResponse<CompanyContract> Select(ObjectHelper objectHelper, CompanyRequest request)
        {
            GenericResponse<CompanyContract> returnObject = objectHelper.InitializeGenericResponse<CompanyContract>("SelectByColumns");
            Architecture.DataAccess.Root.BusinessHelper.Company da = new Architecture.DataAccess.Root.BusinessHelper.Company();

            GenericResponse<CompanyContract> response = da.SelectByColumns(request.Contract.Authentication.UserName,request.Contract.Authentication.Password);
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
