﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SA_Project.Web.Models;
using SA_Project.Web.Service.IService;
using SA_Project.Web.Utility;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;

namespace SA_Project.Web.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderRestService _orderRest;

        public OrderController(IOrderRestService orderRest)
        {
            _orderRest = orderRest;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            List<Order> orders = await _orderRest.GetAsync(url: "/api/orders");

            return View(orders);
        }
        [Authorize]
        [HttpGet]
        public IActionResult CreateOrder()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateOrder(Order order)
        {
            await _orderRest.PostAsync(url: "/api/order/create", data: order);

            return RedirectToAction("Index");
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            await _orderRest.Delete(url: $"/api/order/delete/{orderId}");
            return RedirectToAction(nameof(Index));
        }
    }
}
