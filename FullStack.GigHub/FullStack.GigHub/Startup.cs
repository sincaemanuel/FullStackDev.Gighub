using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FullStack.GigHub.Startup))]
namespace FullStack.GigHub
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
