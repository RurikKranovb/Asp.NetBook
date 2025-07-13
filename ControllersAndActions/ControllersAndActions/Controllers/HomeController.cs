using System.Text;
using ControllersAndActions.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace ControllersAndActions.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index() => View("SimpleForm");

        //public void ReceiveForm(string name, string city)
        //{
        //    Response.StatusCode = 200;
        //    Response.ContentType = "text/html";

        //    byte[] content = Encoding.ASCII.GetBytes($"<html><body>{name} lives in {city}</body>");

        //    Response.Body.WriteAsync(content, 0, content.Length);
        //    //return View("Result", $"{name} lives in {city}");
        //}

        //public IActionResult ReceiveForm(string name, string city) =>
        //    new CustomHtmlResult()
        //    {
        //        Content = $"{name} lives in {city}"
        //    };

        public ViewResult ReceiveForm(string name, string city) => View("Result", $"{name} lives in {city}");
    }
}
