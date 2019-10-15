using Architecture.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Architecture.Proxy;
using System.Reflection;
using System.Web.Routing;
using Architecture.View.Root.BusinessHelper;
using Architecture.Common.Types;

namespace Architecture.View.Web
{
    public class UserControllerBase : Controller,INotifyPropertyChanged
    {
        #region ResourceCollection

        private List<ResourceContract> resourceCollection;
        public List<ResourceContract> ResourceCollection
        {
            get { return resourceCollection; }
            set
            {
                resourceCollection = value;
                if (value != resourceCollection)
                {
                    OnPropertyChanged("ResourceCollection");
                }
            }
        }

        #endregion ResourceCollection
        private int companyId { get; set; }
        private string companyEmail { get; set; }
        private string companyPassword { get; set; }
        public UserControllerBase(string email,string password)
        {
            this.companyEmail = email;
            this.companyPassword = password;
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            string controllerName = requestContext.RouteData.Values["controller"].ToString();
            string actionName = requestContext.RouteData.Values["action"].ToString();

            return base.BeginExecute(requestContext, callback, state);
        }
        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            GetResource();
            string controllerName = filterContext.RouteData.Values["controller"].ToString();
            string actionName = filterContext.RouteData.Values["action"].ToString();

            base.OnResultExecuting(filterContext);
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string controllerName = filterContext.RouteData.Values["controller"].ToString();
            string actionName = filterContext.RouteData.Values["action"].ToString();

            base.OnActionExecuting(filterContext);
        }
        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            filterContext.Result = new RedirectResult("~/Shared/Error");
        }

        public void GetResource()
        {
            var response = BusinessHelper.GetResource(companyEmail,companyPassword, null, null);
            if (!response.Success)
            {

            }

            ResourceCollection = response.Value;
            Dictionary<string, List<ResourceContract>> dictionaryGroupResource = new Dictionary<string, List<ResourceContract>>();
            dictionaryGroupResource.Add("Uygulamalar", ResourceCollection.Where(u => u.ViewType == (short)Enums.ViewType.WinForm).ToList());
            dictionaryGroupResource.Add("Raporlar", ResourceCollection.Where(u => u.ViewType == (short)Enums.ViewType.Report).ToList());
            dictionaryGroupResource.Add("Favoriler", new List<ResourceContract>());

            var DictionaryNavBarMapList = new Dictionary<string, List<ResourceTreeNode>>();
            DictionaryNavBarMapList = BusinessHelper.GetAllGroupTreeNodeList(dictionaryGroupResource);

            var html = CreateMenu(DictionaryNavBarMapList);
            
            ViewBag.CompanyContract = response.Value.Count()>0 ? response.Value.FirstOrDefault().Company:new CompanyContract();
            
            ViewBag.Html = html;
        }
        private StringBuilder CreateMenu(Dictionary<string, List<ResourceTreeNode>> dictionaryMenu)
        {
            StringBuilder html = new StringBuilder();

            html.AppendLine("<div id='sidebar-menu' class='main_menu_side hidden-print main_menu'>");
            foreach (var group in dictionaryMenu)
            {
                html.AppendLine("<div class='menu_section'>");//new Group Begin
                html.AppendLine("<h3>" + group.Key + "</h3>");
                html.AppendLine("<ul class='nav side-menu'>");

                foreach (var item in group.Value)
                {
                    if (!item.IsLeaf)
                    {
                        html.AppendLine("<li>");
                        //html.AppendLine("<a><i class='fa fa-home'></i>" + item.Text +"<span class='fa fa-chevron-down'></span></a>");
                        html.AppendLine("<a>" + item.Text + "<span class='fa fa-chevron-down'></span></a>");
                        if (item.ChildList.Count > 0)
                        {
                            html.AppendLine("<ul class='nav child_menu'>");
                            CreateChildMenuItem(html, item.ChildList);
                        }
                        //child.
                        html.AppendLine("</ul>");
                        html.AppendLine("</li>");
                    }
                    else
                    {
                        var resourceRecord = ResourceCollection.Find(u => u.ResourceId == item.Id);
                        if (!string.IsNullOrEmpty(resourceRecord.ControllerName))
                            html.AppendLine("<li><a href='/" + resourceRecord.ControllerName + "/" + resourceRecord.ViewName + "'>" + item.Text + "</a></li>");
                        else
                            html.AppendLine("<li><a href='#'>" + item.Text + "</a></li>");
                    }
                }

                html.Append("</ul>");
                html.Append("</div>");//Group End
            }
            html.Append("</div>");
            return html;
        }
        private void CreateChildMenuItem(StringBuilder html, List<ResourceTreeNode> childList)
        {
            foreach (var item in childList)
            {
                if (!item.IsLeaf)
                {
                    html.AppendLine("<li>");
                    html.AppendLine("<a>" + item.Text + "<span class='fa fa-chevron-down'></span></a>");
                    if (item.ChildList.Count > 0)
                    {
                        html.AppendLine("<ul class='nav child_menu'>");
                        CreateChildMenuItem(html, item.ChildList);
                    }
                    html.AppendLine("</ul>");
                    html.AppendLine("</li>");
                }
                else
                {
                    var resourceRecord = ResourceCollection.Find(u => u.ResourceId == item.Id);
                    if (!string.IsNullOrEmpty(resourceRecord.ControllerName))
                        html.AppendLine("<li><a href='/" + resourceRecord.ControllerName + "/" + resourceRecord.ViewName + "'>" + item.Text + "</a></li>");
                    else
                        html.AppendLine("<li><a href='#'>" + item.Text + "</a></li>");
                }
            }
        }

        public TResponse Execute<TRequest, TResponse>(TRequest request)
            where TRequest : RequestBase
            where TResponse : ResponseBase
        {
            return Proxy.Executer<TRequest, TResponse>.Execute(request, Assembly.GetCallingAssembly(), Common.Types.Enums.ExecuteType.Local);
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
            var response = Proxy.Executer<RequestBase, ResponseBase>.MultipleExecute(mRequest.RequestList, Assembly.GetCallingAssembly());
            return response;
        }
    }
}
