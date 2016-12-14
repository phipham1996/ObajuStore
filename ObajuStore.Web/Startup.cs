using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ObajuStore.Web.Startup))]
namespace ObajuStore.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
