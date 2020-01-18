using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PPPK.Startup))]
namespace PPPK
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
