using LanguageFeatures.Models;
using Microsoft.AspNetCore.Mvc;

namespace LanguageFeatures.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            List<string> results = new List<string>();
            foreach (var product in Product.GetProducts())
            {
                var name = product?.Name ?? "<No Name>";
                var price = product?.Price ?? 0;
                var relatedName = product?.Related?.Name ?? "<None>";
                results.Add(string.Format($"Name: {name}, Price: {price}, Related: {relatedName}"));
            }
            return View(results);
        }
    }
}
