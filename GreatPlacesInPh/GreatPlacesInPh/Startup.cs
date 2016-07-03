using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GreatPlacesInPh.Startup))]
namespace GreatPlacesInPh
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
