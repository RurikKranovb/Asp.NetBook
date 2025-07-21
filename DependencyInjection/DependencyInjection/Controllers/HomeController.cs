using DependencyInjection.Infrastructure;
using DependencyInjection.Models;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjection.Controllers
{
    public class HomeController : Controller
    {
        private IRepository _repository;
        private ProductTotalizer _productTotalizer;


        public HomeController(IRepository repo, ProductTotalizer productTotalizer)
        {
            _repository = repo;
            _productTotalizer = productTotalizer;
        }


        public ViewResult Index([FromServices] ProductTotalizer totalizer)
        {
            //IRepository repository = HttpContext.RequestServices.GetService<IRepository>(); //LOCATOR SERVICE
            ViewBag.HomeController = _repository.ToString();
            ViewBag.Totalizer = totalizer.Repository.ToString();
            return View(_repository.Products);
        }
        //public ViewResult Index()
        //{
        //    ViewBag.HomeController = _repository.ToString();
        //    ViewBag.Totalizer = _productTotalizer.Repository.ToString();
        //     return View(_repository.Products);
        //}
    }
}
