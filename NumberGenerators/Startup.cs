using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NumberGenerators.Startup))]
namespace NumberGenerators
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
