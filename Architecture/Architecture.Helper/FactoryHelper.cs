using Architecture.Common.Types;
using System;

namespace Architecture.Helper
{
    public class FactoryHelper
    {
        public static GenericResponse<T> InitializeGenericResponse<T>(string pointName)
        {
            GenericResponse<T> response = new GenericResponse<T>(true);
            response.StartTime = DateTime.Now;
            return response;
        }
        public static ResponseBase InitializeResponseBase(object value)
        {
            if (value != null)
            {
                var type = typeof(GenericResponse<>);
                var responseType = type.MakeGenericType(value.GetType());
                var instance = Activator.CreateInstance(responseType, new object[] { true });
                var propertyInfo = responseType.GetProperty("Value");
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(instance, value);
                }
                var genericResponse = (ResponseBase)instance;
                genericResponse.IsInitialized = true;
                return genericResponse;
            }
            else
            {

                ResponseBase response = new ResponseBase(true);
                response.StartTime = DateTime.Now;
                return response;
            }
        }
    }
}
