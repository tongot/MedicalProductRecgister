using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Pharmacy.Startup))]
namespace Pharmacy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
