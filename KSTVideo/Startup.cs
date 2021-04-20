using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KSTVideo.Startup))]
namespace KSTVideo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
