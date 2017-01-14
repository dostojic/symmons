using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(symmons.com.Startup))]
namespace symmons.com
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
