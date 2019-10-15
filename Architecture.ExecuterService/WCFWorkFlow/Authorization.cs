using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCFWorkFlow
{
    public class Authorization : CodeActivity
    {
        public InArgument<string> UserName { get; set; }
        public InArgument<string> Password { get; set; }
        public OutArgument<bool> IsLogin { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var userName = UserName.Get<string>(context);
            var password = Password.Get<string>(context);
            if (userName == "admin" && password == "admin")
            {
                IsLogin.Set(context, true);
            }else
            {
                IsLogin.Set(context, false);
            }
        }
    }
}