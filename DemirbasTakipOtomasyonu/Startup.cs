using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DemirbasTakipOtomasyonu.Startup))]
namespace DemirbasTakipOtomasyonu
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
