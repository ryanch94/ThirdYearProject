using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FashionFix.Startup))]
namespace FashionFix
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
