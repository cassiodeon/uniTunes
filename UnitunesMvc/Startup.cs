using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UnitunesMvc.Startup))]
namespace UnitunesMvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
          
        }
    }
}
