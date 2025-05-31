using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using SportsStore.ViewModels;

namespace SportStore.Tests
{
    public class ProductControllerTests
    {
        [Fact]
        public void Can_Paginate()
        {
            //Организация
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            var products = new Product[]
            {
                new Product() { ProductId = 1, Name = "P1" },
                new Product() { ProductId = 2, Name = "P2" },
                new Product() { ProductId = 3, Name = "P3" },
                new Product() { ProductId = 4, Name = "P4" },
                new Product() { ProductId = 5, Name = "P5" }

            }.AsQueryable<Product>();

            mock.Setup(m => m.Products).Returns(products);

            ProductController controller = new ProductController(mock.Object);

            controller.PageSize = 3;

            //Действие
            ProductsListViewModel result =
                controller.List(null, 2).ViewData.Model as ProductsListViewModel;


            //Утверждение
            Product[] prodArray = result.Products.ToArray();
            Assert.True(prodArray.Length == 2);
            Assert.Equal("P4", prodArray[0].Name);
            Assert.Equal("P5", prodArray[1].Name);
        }

        [Fact]
        public void Can_Send_Pagination_View_Model()
        {
            //Организация
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product() {ProductId = 1, Name = "P1"},
                new Product() {ProductId = 2, Name = "P2"},
                new Product() {ProductId = 3, Name = "P3"},
                new Product() {ProductId = 4, Name = "P4"},
                new Product() {ProductId = 5, Name = "P5"}
            }).AsQueryable<Product>());

            //Организация
            ProductController controller = new ProductController(mock.Object) { PageSize = 3 };

            //Действие
            ProductsListViewModel result = controller.List(null, 2).ViewData.Model as ProductsListViewModel;

            //Утверждение
            PagingInfo pagingInfo = result.PagingInfo;
            Assert.Equal(2, pagingInfo.CurrentPage);
            Assert.Equal(3, pagingInfo.ItemsPerPage);
            Assert.Equal(5, pagingInfo.TotalItems);
            Assert.Equal(2, pagingInfo.TotalPages);
        }

        [Fact]
        public void Can_Filter_Products()
        {
            //Организация
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product() { ProductId = 1, Name = "P1" },
                new Product() { ProductId = 2, Name = "P2" },
                new Product() { ProductId = 3, Name = "P3" },
                new Product() { ProductId = 4, Name = "P4" },
                new Product() { ProductId = 5, Name = "P5" }
            }).AsQueryable<Product>());

            //Организация — создание контроллера и установка размера страницы в три элемента
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            //Действие
            Product[] result =
                (controller.List("Cat2", 1).ViewData.Model as ProductsListViewModel).Products.ToArray();

            //Утверждение
            Assert.Equal(2, result.Length);
            Assert.True(result[0].Name == "P2" && result[0].Category == "Cat2");
            Assert.True(result[1].Name == "P4" && result[1].Category == "Cat2");
        }

        [Fact]
        public void Generate_Category_Specific_Product_Count()
        {
            //Организация
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(repository => repository.Products)
                .Returns((new Product[]
                {
                    new Product() { ProductId = 1, Name = "P1", Category = "Cat1" },
                    new Product() { ProductId = 2, Name = "P2", Category = "Cat2" },
                    new Product() { ProductId = 3, Name = "P3", Category = "Cat1" },
                    new Product() { ProductId = 4, Name = "P4", Category = "Cat2" },
                    new Product() { ProductId = 5, Name = "P5", Category = "Cat3" }
                }).AsQueryable<Product>());

            ProductController target = new ProductController(mock.Object);

            target.PageSize = 3;

            Func<ViewResult, ProductsListViewModel> GetModel = result =>
                result?.ViewData?.Model as ProductsListViewModel;
            
            //Действие
            int? res1 = GetModel(target.List("Cat1"))?.PagingInfo.TotalItems;
            int? res2 = GetModel(target.List("Cat2"))?.PagingInfo.TotalItems;
            int? res3 = GetModel(target.List("Cat3"))?.PagingInfo.TotalItems;
            int? resAll = GetModel(target.List(null))?.PagingInfo.TotalItems;

            //Утверждение
            Assert.Equal(2,res1);
            Assert.Equal(2,res2);
            Assert.Equal(1,res3);
            Assert.Equal(5,resAll);
        }



    }
}
