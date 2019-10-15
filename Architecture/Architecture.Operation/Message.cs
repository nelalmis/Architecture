using Architecture.Base;
using Architecture.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Operation
{
   public partial class Message
    {
        public GenericResponse<MessageContract> SelectByColumns(ObjectHelper objectHelper, MessageRequest request)
        {
            GenericResponse<MessageContract> returnObject = objectHelper.InitializeGenericResponse<MessageContract>("SelectByColumns");
            Architecture.DataAccess.Message bo = new DataAccess.Message(objectHelper.ExecutionDataContext);

            GenericResponse<MessageContract> response = bo.SelectByColumns(request.Contract);
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
