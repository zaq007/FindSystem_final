using FindSystem.Filters;
using Microsoft.Owin;
using Owin;
using WebMatrix.WebData;

namespace FindSystem
{
    public class Startup
    {
        [InitializeSimpleMembership]
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}