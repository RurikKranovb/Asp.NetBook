using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WorkingWithVS.Controllers;
using WorkingWithVS.Models;

namespace WorkingWithVS.Tests
{
    public class HomeControllerTests
    {

        class ModelCompleteFakeRepository : IRepository
        {
            public IEnumerable<Product> Products { get; set; }
            public void AddProduct(Product product) { }
        }

        [Theory]
        [ClassData(typeof(ProductTestData))]
        public void IndexActionModelIsComplete(Product[] products)
        {
            //Организация

            var moq = new Mock<IRepository>();
            moq.SetupGet(m => m.Products)
                .Returns(new[] { new Product() { Name = "P1", Price = 100 } });

            var controller = new HomeController() {Repository = moq.Object};
            //controller.Repository = new ModelCompleteFakeRepository()
            //{
            //   Products = products
            //};

            //Действие
            var model = controller.Index();//(controller.Index() as ViewResult)?.ViewData.Model as IEnumerable<Product>;

            //Утверждение
            //Assert.Equal(controller.Repository.Products, model,
            //    Comparer.Get<Product>((p1, p2) => p1.Name == p2.Name
            //                                      && p1.Price == p2.Price));
            moq.VerifyGet(m => m.Products, Times.Once);
        }

        #region TheoryInlineData

        //[Theory]
        //[InlineData(275, 48.95, 19.50, 24.95)]
        //[InlineData(5, 48.95, 19.50, 24.95)]
        //public void IndexActionModelIsComplete(decimal price1, decimal price2, decimal price3, decimal price4)
        //{
        //    //Организация
        //    var controller = new HomeController();
        //    controller.Repository = new ModelCompleteFakeRepository()
        //    {
        //        Products = new Product[]
        //        {
        //            new Product() { Name = "P1", Price = price1 },
        //            new Product() { Name = "P2", Price = price2 },
        //            new Product() { Name = "P3", Price = price3 },
        //            new Product() { Name = "P4", Price = price4 },
        //        }
        //    };

        //    //Действие
        //    var model = (controller.Index() as ViewResult)?.ViewData.Model as IEnumerable<Product>;

        //    //Утверждение
        //    Assert.Equal(controller.Repository.Products, model,
        //        Comparer.Get<Product>((p1, p2) => p1.Name == p2.Name
        //                                          && p1.Price == p2.Price));
        //}

        #endregion

        class PropertyOnceFakeRepository : IRepository
        {
            public int PropertyCounter { get; set; } = 0;

            public IEnumerable<Product> Products
            {
                get
                {
                    PropertyCounter++;
                    return new[] { new Product() { Name = "P1", Price = 100 } };
                }
            }

            public void AddProduct(Product product) { }
        }

        [Fact]
        public void RepositoryPropertyCalledOnce()
        {
            //Организация
            var repo = new PropertyOnceFakeRepository();
            var controller = new HomeController() { Repository = repo };

            //Действие
            var result = controller.Index();

            //Утверждение
            Assert.Equal(1,repo.PropertyCounter);
        }


    }
}
