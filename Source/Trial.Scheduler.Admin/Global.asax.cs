using System;
using System.Web.Mvc;
using Trial.Scheduler.Admin.Config;

namespace Trial.Scheduler.Admin
{
    public class Global : System.Web.HttpApplication
    {
        private readonly Bootstrapper _bootstrapper = new Bootstrapper();

        protected void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            _bootstrapper.Run();
        }

        protected void Application_End(object sender, EventArgs e)
        {
            _bootstrapper.Dispose();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }
    }
}