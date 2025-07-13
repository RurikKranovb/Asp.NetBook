using Microsoft.AspNetCore.Mvc;

namespace ControllersAndActions.Controllers
{
    public class ExampleController : Controller
    {
        public ViewResult Index() => View(DateTime.Now);
        public RedirectResult RedirectResult() => Redirect("/Example/Index");
    }
}
