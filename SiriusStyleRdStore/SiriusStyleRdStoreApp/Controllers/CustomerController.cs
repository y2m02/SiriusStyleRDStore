using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using SiriusStyleRdStore.BL.Services;
using SiriusStyleRdStore.Entities.Requests.Customer;
using SiriusStyleRdStore.Entities.Responses;
using SiriusStyleRdStore.Entities.ViewModels;
using SiriusStyleRdStore.Entities.ViewModels.Customer;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SiriusStyleRdStoreApp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetAll([DataSourceRequest] DataSourceRequest request)
        {
            var response = await _customerService.GetAll().ConfigureAwait(false);

            if (response is Success<IEnumerable<CustomerViewModel>> customers)
                return Json(await customers.Response.ToDataSourceResultAsync(request));

            throw new Exception();
        }

        [HttpPost]
        public async Task<ActionResult> BatchCreate([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<CustomerViewModel> customers)
        {
            var results = new List<CustomerViewModel>();

            if (customers != null && ModelState.IsValid)
            {
                var response = await _customerService.BatchCreate(_mapper.Map<List<CreateCustomerRequest>>(customers.ToList()));

                if (response is Success<CustomerViewModel> result)
                {
                    results.Add(result.Response);
                }
            }

            return Json(await results.ToDataSourceResultAsync(request, ModelState));
        }
        
        [HttpPost]
        public async Task<ActionResult> BatchUpdate([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<CustomerViewModel> customers)
        {
            var customerList = customers.ToList();
            if (ModelState.IsValid)
            {
                var _ = await _customerService.BatchUpdate(_mapper.Map<List<UpdateCustomerRequest>>(customerList.ToList()));
            }

            return Json(await customerList.ToDataSourceResultAsync(request, ModelState));
        }
        
        [HttpPost]
        public async Task<ActionResult> BatchDelete([DataSourceRequest] DataSourceRequest request,
            [Bind(Prefix = "models")]IEnumerable<CustomerViewModel> customers)
        {
            var customerList = customers.ToList();
            if (ModelState.IsValid)
            {
                var _ = await _customerService.BatchDelete(_mapper.Map<List<DeleteCustomerRequest>>(customerList.ToList()));
            }

            return Json(await customerList.ToDataSourceResultAsync(request, ModelState));
        }

        public async Task<JsonResult> AjaxCreate(CustomerRequest customer)
        {
            _ = await _customerService.Create(_mapper.Map<CreateCustomerRequest>(customer));

            return Json(customer);
        }

        public async Task<JsonResult> GetAllForDropDownList(int? id)
        {
            var response = await _customerService.GetAllForDropDownList(id.GetValueOrDefault()).ConfigureAwait(false);

            if (response is Success<IEnumerable<CustomerViewModel>> customers)
                return Json(customers.Response
                    .Select(c => new
                    {
                        id = c.CustomerId,
                        description = c.FullName
                    }));

            throw new Exception();
        }
    }
}