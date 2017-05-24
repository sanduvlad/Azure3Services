using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Azure3Services.Startup))]
namespace Azure3Services
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
