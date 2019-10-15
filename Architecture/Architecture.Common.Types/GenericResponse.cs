using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Common.Types
{
    [Serializable]
    public class GenericResponse<T>:ResponseBase
    {
        
        private T _value;
        /// <summary>
        /// İşlemin sonucunda dönen değeri gösterir.
        /// </summary>
        public T Value
        {
            get;set;
            //get {
            //     return _value == null ? (T)base.Value : _value; }
            //set {
            //    _value = value;
            //    base.Value = value;
            //}
        }
      
        /// <summary>
        /// contract
        /// </summary>
        public GenericResponse()
        {
        }
        public GenericResponse(bool isInitialized)
        {

        }
    }   

}
