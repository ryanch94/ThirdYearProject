using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SignMeIn.Startup))]
namespace SignMeIn
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
