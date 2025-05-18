using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Asp.NetBook.Models;
using FirstApplicationMVC.Models;

namespace Asp.NetBook.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        int hour = DateTime.Now.Hour;
        ViewBag.Greeting = hour < 12 ? "Good Morning" : "Good Afternoon";
        return View("MyView");
    }

    [HttpGet]
    public IActionResult RsvpForm()
    {
        return View();
    }

    [HttpPost]
    public IActionResult RsvpForm(GuestResponse guestResponse)
    {
        if (ModelState.IsValid)
        {
            Repository.AddResponse(guestResponse);
            return View("Thanks", guestResponse);
        }
        else
        {
            return View();
        }
      
    }

    public ViewResult ListResponses()
    {
        return View(Repository.Responses.Where(r => r.WillAttend == true));
    }
}