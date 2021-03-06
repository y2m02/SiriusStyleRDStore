using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using SiriusStyleRd.Entities.Enums;
using SiriusStyleRd.Entities.Requests.Customer;
using SiriusStyleRd.Entities.Requests.Order;
using SiriusStyleRd.Entities.Requests.Product;
using SiriusStyleRd.Entities.ViewModels;
using SiriusStyleRd.Entities.ViewModels.Item;
using SiriusStyleRd.Entities.ViewModels.Order;
using SiriusStyleRd.Services.Services;

namespace SiriusStyleRdApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IItemService _itemService;
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService, IMapper mapper,
            IItemService itemService)
        {
            _orderService = orderService;
            _mapper = mapper;
            _itemService = itemService;
        }

        public async Task<IActionResult> Index()
        {
            var itemResponse = await _itemService
                .Get(new List<ItemType>
                {
                    ItemType.Category,
                    ItemType.Size,
                });

            if (itemResponse is Success<ItemViewModel> item)
            {
                ViewBag.Categories = item.Response.Categories;
                ViewBag.Sizes = item.Response.Sizes;
            }

            return View();
        }

        public async Task<IActionResult> Upsert(string orderNumber = null)
        {
            var itemResponse = await _itemService
                .Get(new List<ItemType>
                {
                    ItemType.Customer,
                    ItemType.Category,
                    ItemType.Size,
                    ItemType.Bale,
                });

            if (itemResponse is Success<ItemViewModel> item)
            {
                ViewBag.Customers = item.Response.Customers;
                ViewBag.Sizes = item.Response.Sizes;
                ViewBag.Categories = item.Response.Categories;
                ViewBag.Bales = item.Response.Bales;
            }

            var orderRequest = new OrderRequest
            {
                Status = OrderStatus.Pending
            };

            if (orderNumber.HasValue())
            {
                var response = await _orderService.GetByNumber(orderNumber).ConfigureAwait(false);

                if (response is Success<OrderViewModel> order)
                    orderRequest = _mapper.Map<OrderRequest>(order.Response);
            }

            return View(new OrderCustomerViewModel
            {
                Order = orderRequest,
                Customer = new CustomerRequest()
            });
        }

        public async Task<JsonResult> GetAll([DataSourceRequest] DataSourceRequest request)
        {
            var response = await _orderService.GetAll().ConfigureAwait(false);

            if (response is Success<IEnumerable<OrderViewModel>> orders)
            {
                return Json(await orders.Response.ToDataSourceResultAsync(request));
            }

            throw new Exception();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(OrderRequest order)
        {
            if (order.OrderNumber.HasValue())
            {
                var _ = await _orderService.Update(_mapper.Map<UpdateOrderRequest>(order));

                return RedirectToAction(nameof(Index));
            }

            _ = await _orderService.Create(_mapper.Map<CreateOrderRequest>(order));

            return RedirectToAction(nameof(Index));
        }

        public async Task<JsonResult> AjaxUpsert(OrderRequest order)
        {
            if (order.OrderNumber.HasValue())
            {
                var _ = await _orderService.Update(_mapper.Map<UpdateOrderRequest>(order));

                return Json(order);
            }

            _ = await _orderService.Create(_mapper.Map<CreateOrderRequest>(order));

            return Json(order);
        }

        [HttpPost]
        public async Task<JsonResult> Cancel(OrderRequest order)
        {
            var response = await _orderService.Cancel(_mapper.Map<CancelOrderRequest>(order));

            return Json(order);
        }

        //[HttpPost]
        //public async Task<IActionResult> Cancel([DataSourceRequest] DataSourceRequest request,
        //    OrderViewModel order)
        //{
        //        var _ = await _orderService.Cancel(
        //            _mapper.Map<CancelOrderRequest>(order));

        //    return Json(await new[] { order }.ToDataSourceResultAsync(request, ModelState));
        //}
    }
}