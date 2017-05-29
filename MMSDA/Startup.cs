using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MMSDA.Startup))]
namespace MMSDA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
