using Architecture.Common.Types;
using DevExpress.XtraEditors;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Threading.Tasks;

namespace Architecture.View.Win
{
    public partial class UserControlBase : XtraUserControl, INotifyPropertyChanged
    {
        public UserControlBase()
        {
            InitializeComponent();
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public TResponse Execute<TRequest, TResponse>(TRequest request)
            where TRequest : RequestBase
            where TResponse : ResponseBase
        {
            
            return Proxy.Executer<TRequest, TResponse>.Execute(request,Assembly.GetCallingAssembly(),Common.Types.Enums.ExecuteType.Server);

        }
        public virtual async Task<TResponse> ExecuteAsync<TRequest, TResponse>(TRequest request)
            where TRequest : RequestBase
            where TResponse : ResponseBase
        {
            return await Proxy.Executer<TRequest, TResponse>.ExecuteAsync(request);
        }
        public MultipleResponse MultipleExecute<TRequest, TResponse>(List<RequestBase> requestList)
            where TRequest : RequestBase
            where TResponse : ResponseBase
        {
            return Proxy.Executer<TRequest, TResponse>.MultipleExecute(requestList, Assembly.GetCallingAssembly());
        }
        public MultipleResponse Execute(MultipleRequest mRequest)
        {
            var response= Proxy.Executer<RequestBase, ResponseBase>.MultipleExecute(mRequest.RequestList, Assembly.GetCallingAssembly());
            return response;
        }
    }
    
}
