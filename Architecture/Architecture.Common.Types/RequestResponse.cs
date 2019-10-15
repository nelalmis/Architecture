using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;

namespace Architecture.Common.Types
{
    [Serializable]
    public class MessageBase:ContractBase
    {        
        public string OperationKey { get; set; }  
    }

    [Serializable]
    public partial class RequestBase : MessageBase
    {
        private string _methodName;

        public string MethodName
        {
            get
            {
                if (string.IsNullOrEmpty(_methodName))
                {
                    return "Call";
                }
                else
                {
                    return _methodName;
                }
            }
            set
            {
                _methodName = value;
            }
        }
        public string TransactionCode { get; set; }
        public int? LanguageId { get; set; }
        public string Description { get; set; }        
        public bool IsActive { get; set; }        
        public DateTime TranDate { get; set; }
    }

    [Serializable]
    [DataContract]
    public partial class ResponseBase : MessageBase
    {
        public bool IsInitialized;
        public ResponseBase()
        {
        }
        public ResponseBase(bool isInitialized)
        {
            this.IsInitialized = isInitialized;
        }
        /// <summary>
        /// İşlemin başarılı olup olmadığı bilgisini verir.
        /// </summary>
        [DataMember]
        public virtual bool Success
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

        private List<Exception> _results;
        /// <summary>
        /// İşleme dair hata mesajlarını ve diğer sonuçları gösterir.
        /// </summary>   
        [DataMember]
        public virtual List<Exception> Results
        {
            get
            {
                if (_results == null)

                    _results = new List<Exception>();
                return _results;
            }
            set { _results = value; }
        }

        private string pointName;

        /// <summary>
        /// Mesajın döndüğü classın ismini verir.
        /// </summary>
        [DataMember]
        public string PointName
        {
            get { return pointName; }
            set { pointName = value; }
        }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Key { get; set; }
        public bool GottenFromCache { get; set; }

    }
    

    [Serializable]
    /// <summary>
    /// iptal Edilebilir Insert, Update, Delete işlemleri için kullanılır.
    /// </summary>
    public abstract class TransactionRequestBase : RequestBase
    {  
    }

    [Serializable]
    public abstract class TransactionResponseBase : ResponseBase
    {
        
    }

    [Serializable]
    public class MultipleRequest:RequestBase
    {   
        public List<RequestBase> RequestList { get; set; }
    }

    [Serializable]
    public class MultipleTransactionRequest : TransactionRequestBase
    {        
        public List<RequestBase> RequestList { get; set; }
    }

    [Serializable]
    public abstract class CacheRequest : RequestBase { }

    [Serializable]
    public class MultipleResponse : ResponseBase
    {
        public List<ResponseBase> ResponseList { get; set; }
        public override bool Success
        {
            get
            {
                if (ResponseList != null && ResponseList.Count > 0)
                {
                    return !ResponseList.Any(u => u.Success == false);
                }
                return base.Success;
            }
        }
    }

    [Serializable]
    public abstract class ContractBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
