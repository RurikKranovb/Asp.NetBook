using Microsoft.AspNetCore.Mvc;
using WorkingWithVS.Models;

namespace WorkingWithVS.Controllers
{
    public class HomeController : Controller
    {
        SimpleRepository Repository = SimpleRepository.SharedRepository;

        public IActionResult Index() => View(SimpleRepository.SharedRepository.Products
            .Where(p => p?.Price <50));

        [HttpGet]
        public IActionResult AddProduct() => View(new Product());

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            Repository.AddProduct(product);
            return RedirectToAction("Index");
        }
    }
}
