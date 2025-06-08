using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Routing;
using Moq;
using SportsStore.Components;
using SportsStore.Models;

namespace SportStore.Tests
{
    public class NavigationMenuViewComponentTest
    {
        //[Fact]
        //public void Can_Select_Categories()
        //{
        //    //Организация
        //    Mock<IProductRepository> mock = new Mock<IProductRepository>();
        //    mock.Setup(repository => repository.Products)
        //        .Returns((new Product[]
        //        {
        //            new Product() { Name = "P1", Category = "Apples" },
        //            new Product() { Name = "P2", Category = "Apples" },
        //            new Product() { Name = "P3", Category = "Plums" },
        //            new Product() { Name = "P4", Category = "Oranges" }
        //        }).AsQueryable<Product>());

        //    NavigationMenuViewComponent target = new NavigationMenuViewComponent(mock.Object);

        //    //Действие - получение набора категорий
        //    string[] results = ((IEnumerable<string>)(target.Invoke() as ViewViewComponentResult).ViewData.Model)
        //        .ToArray();

        //    //Утверждение
        //    Assert.True(Enumerable.SequenceEqual(new string[] {"Apples", "Oranges", " Plums"}, results));

        //}

        [Fact]
        public void Indicates_Selected_Category()
        {
            //Организация
            string categoryToSelect = "Apples";
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(repository => repository.Products)
                .Returns((new Product[]
                {
                    new Product() { Name = "P1", Category = "Apples" },
                    new Product() { Name = "P4", Category = "Oranges" }
                }).AsQueryable<Product>());

            NavigationMenuViewComponent target = new NavigationMenuViewComponent(mock.Object);
            target.ViewComponentContext = new ViewComponentContext()
            {
                ViewContext = new ViewContext()
                {
                    RouteData = new RouteData()
                }
            };
            target.RouteData.Values["category"] = categoryToSelect;

            //Действие
            string result = (string)(target.Invoke() as ViewViewComponentResult).ViewData["SelectedCategory"];

            //Утверждение
            Assert.Equal(categoryToSelect, result);
        }
    }
}
