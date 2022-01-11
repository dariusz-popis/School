using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(School.MvcUi.Startup))]
namespace School.MvcUi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
