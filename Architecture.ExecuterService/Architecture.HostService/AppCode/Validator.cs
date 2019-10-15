using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.HostService
{
    public class Validator: UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            if (userName == "admin" && password == "admin")
                return;
            else
            {
                throw new FaultException("Unknown Username or Incorrect Password");
            }
        }
    }
}
