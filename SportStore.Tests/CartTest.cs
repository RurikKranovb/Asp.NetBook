using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Models;

namespace SportStore.Tests
{
    public class CartTest
    {
        [Fact]
        public void Can_Add_New_Lines()
        {
            //Организация- тестовый товар
            Product p1 = new Product { ProductId = 1, Name = "P1" };
            Product p2 = new Product { ProductId = 2, Name = "P2" };

            //Организация- создание новой корзины
            Cart target = new Cart();

            //Действие
            target.AddItem(p1, 1);
            target.AddItem(p2, 2);

            CartLine[] result = target.Lines.ToArray();

            //Утверждение
            Assert.Equal(2, result.Length);
            Assert.Equal(p1, result[0].Product);
            Assert.Equal(p2, result[1].Product);
        }

        [Fact]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            //Организация- тестовый товар
            Product p1 = new Product { ProductId = 1, Name = "P1" };
            Product p2 = new Product { ProductId = 2, Name = "P2" };

            //Организация- создание новой корзины
            Cart target = new Cart();

            //Действие
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 10);


            CartLine[] result = target.Lines.OrderBy(c => c.Product.ProductId).ToArray();

            //Утверждение
            Assert.Equal(2, result.Length);
            Assert.Equal(11, result[0].Quantity);
            Assert.Equal(1, result[1].Quantity);
        }

        [Fact]
        public void Calculate_Cart_Total()
        {
            //Организация- тестовый товар
            Product p1 = new Product { ProductId = 1, Name = "P1", Price = 100M };
            Product p2 = new Product { ProductId = 2, Name = "P2", Price = 50M };

            //Организация- создание новой корзины
            Cart target = new Cart();

            //Действие
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 3);

            decimal result = target.ComputeTotalValue();

            //Утверждение
            Assert.Equal(450M, result);
            
        }

        [Fact]
        public void Can_Clear_Contents()
        {
            //Организация- тестовый товар
            Product p1 = new Product { ProductId = 1, Name = "P1", Price = 100M };
            Product p2 = new Product { ProductId = 2, Name = "P2", Price = 50M };

            //Организация- создание новой корзины
            Cart target = new Cart();

            //Действие
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 3);

           target.Clear();

            //Утверждение
            Assert.Equal(0, target.Lines.Count());

        }


    }
}
