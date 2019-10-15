using Architecture.Base;
using Architecture.Common.Types;
using Architecture.Types.Root.BusinessHelper;
using System.Collections.Generic;

namespace Architecture.Operation.Root.BusinessHelper
{
    public partial class Country
    {
        public GenericResponse<List<CountryContract>> SelectByColumns(ObjectHelper objectHelper, CountryRequest request)
        {
            GenericResponse<List<CountryContract>> returnObject = objectHelper.InitializeGenericResponse<List<CountryContract>>("SelectByColumns");
            Architecture.DataAccess.Root.BusinessHelper.Country bo = new DataAccess.Root.BusinessHelper.Country(objectHelper.ExecutionDataContext);

            GenericResponse<List<CountryContract>> response = bo.Select();
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
