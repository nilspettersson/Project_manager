using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TaskManager.Startup))]
namespace TaskManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
