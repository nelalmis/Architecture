using Architecture.Base;
using Architecture.Common.Types;
using System;

namespace Architecture.Operation
{
    public partial class Journal
    {
        public GenericResponse<Int32> Insert(ObjectHelper objectHelper, JournalRequest request)
        {
            GenericResponse<Int32> returnObject = objectHelper.InitializeGenericResponse<Int32>("SelectByColumns");
            DataAccess.Journal bo = new DataAccess.Journal(objectHelper.ExecutionDataContext);

            GenericResponse<Int32> response = bo.Insert(request.Contract);
            if (!response.Success)
            {
                returnObject.Results.AddRange(response.Results);
                return returnObject;
            }

            returnObject.Value = response.Value;
            return returnObject;
        }
        public GenericResponse<Int32> SelectAndUpdate(ObjectHelper objectHelper, JournalRequest request)
        {
            GenericResponse<Int32> returnObject = objectHelper.InitializeGenericResponse<Int32>("SelectByColumns");
            DataAccess.Journal bo = new DataAccess.Journal(objectHelper.ExecutionDataContext);

            var responseJournal = bo.SelectByOperationKey(request.Contract.OperationKey);
            if (!responseJournal.Success)
            {
                returnObject.Results.AddRange(responseJournal.Results);
                return returnObject;
            }
            var responseUpdate = bo.Update(request.Contract);
            if (!responseUpdate.Success)
            {
                returnObject.Results.AddRange(responseUpdate.Results);
                return returnObject;
            }

            returnObject.Value = responseUpdate.Value;
            return returnObject;
        }

    }
}
