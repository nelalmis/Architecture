using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Architecture.WebApplication.Startup))]
namespace Architecture.WebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
