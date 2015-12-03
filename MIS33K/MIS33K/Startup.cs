using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MIS33K.Startup))]
namespace MIS33K
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
