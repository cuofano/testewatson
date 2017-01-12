using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TesteWatson.Startup))]
namespace TesteWatson
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
