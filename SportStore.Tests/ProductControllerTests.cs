using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;

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
            IEnumerable<Product>? result =
                controller.List(2).ViewData.Model as IEnumerable<Product>;

            //Утверждение
            Product[] prodArray = result.ToArray();
            Assert.True(prodArray.Length == 2);
            Assert.Equal("P4", prodArray[0].Name);
            Assert.Equal("P5", prodArray[1].Name);
        }



    }
}
