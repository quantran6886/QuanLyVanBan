using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AppNetShop.Startup))]
namespace AppNetShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
