﻿using Microsoft.AspNetCore.Mvc;
using SportsStore.Infrastructure;
using SportsStore.Models;
using SportsStore.ViewModels;

namespace SportsStore.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository _repository;
        private Cart _cart;

        public CartController(IProductRepository repository, Cart cartService)
        {
            _repository = repository;
            _cart = cartService;
        }

        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            Product product = _repository.Products
                .FirstOrDefault(p => p.ProductId == productId);

            if (product != null)
            {
                _cart.AddItem(product,1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
        {
            Product product = _repository.Products
                .FirstOrDefault(p => p.ProductId == productId);

            if (product != null)
            {
                _cart.RemoveLine(product);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel()
            {
                Cart = _cart,
                ReturnUrl = returnUrl
            });
        }

    }
}
