using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(E_Wallet.Startup))]
namespace E_Wallet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
