using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Trial.Scheduler.Admin.Navigation;

namespace Trial.Scheduler.Admin.Controllers
{
    public class LayoutController : Controller
    {
        public ActionResult Index()
        {
            dynamic model = new ExpandoObject();
            model.Menu = BuildMenu();
            return View(model);
        }

        public void Html(string folder, string subfolder, string template)
        {
            var templatePath = string.IsNullOrWhiteSpace(subfolder)
                ? Server.MapPath(string.Format("~/Views/{0}/{1}.html", folder, template))
                : Server.MapPath(string.Format("~/Views/{0}/{1}/{2}.html", folder, subfolder, template));

            if (!System.IO.File.Exists(templatePath))
            {
                Response.StatusCode = 404;
            }
            else
            {
                string html = System.IO.File.ReadAllText(templatePath);
                var bytes = Encoding.UTF8.GetBytes(html);
                Response.OutputStream.Write(bytes, 0, bytes.Length);
                Response.OutputStream.Flush();
            }
        }

        private IEnumerable<NavigationGroup> BuildMenu()
        {
            var builder = new NavigationBuilder();
            builder
                .UsingGroup("Scheduler")
                .WithDisplayName("Scheduler")
                .WithLink("List clients", cfg => cfg.Url("clients").Template("list-clients.html").LinkToGroup())
                .WithLink("List commands", cfg => cfg.Url("commands").Template("list-commands.html"))
                .WithLink("List logs", cfg => cfg.Url("logs").Template("list-logs.html"))
                ;
            
            return builder.Groups.Select(x => x.Build());
        }
    }
}