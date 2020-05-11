using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnTwoWheels.Startup))]
namespace OnTwoWheels
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
