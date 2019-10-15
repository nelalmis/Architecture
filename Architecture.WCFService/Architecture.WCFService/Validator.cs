using System.IdentityModel.Selectors;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Architecture.WCFService
{
    public class Validator: UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            Architecture.DataAccess.Authentication aut = new DataAccess.Authentication();
            var response = aut.SelectByColumns(userName, password);
            if (!response.Success)
            {
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
            if (response.Value != null)
            {
                return;
            }else
            {
                WebOperationContext.Current.OutgoingResponse.Headers.Add("WWW-Authenticate: Basic realm=\"ExecuteService\"");
                //Throw an exception with the associated HTTP status code equivalent to HTTP status 401  
                throw new WebFaultException(HttpStatusCode.Unauthorized);
            }
        }
    }
}
