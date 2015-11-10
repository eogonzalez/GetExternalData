using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GetExternalData.Startup))]
namespace GetExternalData
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
