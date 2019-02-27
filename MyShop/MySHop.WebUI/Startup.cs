using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MySHop.WebUI.Startup))]
namespace MySHop.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
