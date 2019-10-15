using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.WCFModel
{
    [DataContract]
    public class ResponseModelBase<T>
    {
        private T _value;
        private List<Exception> _results;
        private string pointName;

        [DataMember]
        public T Value
        {
            get { return _value; }
            set { _value = value; }
        }

        /// <summary>
        /// İşlemin başarılı olup olmadığı bilgisini verir.
        /// </summary>
        [DataMember]
        public bool Success
        {
            get
            {
                if (_results.Count > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// İşleme dair hata mesajlarını ve diğer sonuçları gösterir.
        /// </summary>   
        [DataMember]
        public List<Exception> Results
        {
            get { return _results; }
            set { _results = value; }
        }

        /// <summary>
        /// Mesajın döndüğü classın ismini verir.
        /// </summary>
        [DataMember]
        public string PointName
        {
            get { return pointName; }
            set { pointName = value; }
        }
        public ResponseModelBase()
        {
            Results = new List<Exception>();
        }
    }
}
