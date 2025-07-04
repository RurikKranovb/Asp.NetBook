﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;

namespace SportStore.Tests
{
    public class OrderControllerTests
    {
        [Fact]
        public void Cannot_Checkout_Empty_Cart()
        {
            //Организация - создание имитированного хранилища заказов
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();

            //Организация создание пустой корзины
            Cart cart = new Cart();

            //Организация создание заказа
            Order order = new ();

            //Организация - создание эклемпляра контроллера
            OrderController target = new OrderController(mock.Object, cart);

            //Действие
            ViewResult result = target.Checkout(order) as ViewResult;
            
            //Утверждение - проверка, что заказ не был сохранен
            mock.Verify(m=> m.SaveOrder(It.IsAny<Order>()),Times.Never);

            //Утверждение - проверка, что метод возвращает стандартное представление
            Assert.True(string.IsNullOrEmpty(result.ViewName));

            //Утверждение - проверка, что представлению передана недопустимая модель
            Assert.False(result.ViewData.ModelState.IsValid);

        }

        [Fact]
        public void Cannot_Checkout_Invalid_ShippingDetails()
        {
            //Организация - создание имитированного хранилища заказов
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();

            //Организация создание корзины c 1 элементом
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);

            //Организация - создание эклемпляра контроллера
            OrderController target = new OrderController(mock.Object, cart);

            //Организация - добавление ошибки в модель
            target.ModelState.AddModelError("error", "error");

            //Действие, попытка перехода к оплате
            ViewResult result = target.Checkout(new Order()) as ViewResult;

            //Утверждение - проверка, что заказ не был сохранен
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);

            //Утверждение - проверка, что метод возвращает стандартное представление
            Assert.True(string.IsNullOrEmpty(result.ViewName));

            //Утверждение - проверка, что представлению передается недопустимая модель
            Assert.False(result.ViewData.ModelState.IsValid);

        }

        //[Fact]
        //public void Can_Checkout_And_Submit_Order()
        //{
        //    //Организация - создание имитированного хранилища заказов
        //    Mock<IOrderRepository> mock = new Mock<IOrderRepository>();

        //    //Организация создание корзины c 1 элементом
        //    Cart cart = new Cart();
        //    cart.AddItem(new Product(),1);

        //    //Организация - создание эклемпляра контроллера
        //    OrderController target = new OrderController(mock.Object, cart);

        //    //Действие, попытка перехода к оплате
        //    RedirectToActionResult result = target.Checkout(new Order()) as RedirectToActionResult;

        //    //Утверждение - проверка, что заказ был сохранен
        //    mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Once);

        //    //Утверждение- проверка, что метод перенаправляется на действие Completed
        //    Assert.Equal("Completed", result.ActionName);

        //}
    }
}
