using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SiriusStyleRd.Entities.Enums;
using SiriusStyleRd.Entities.Models;
using SiriusStyleRd.Entities.Requests.Order;
using SiriusStyleRd.Entities.Requests.Product;
using SiriusStyleRd.Entities.ViewModels;
using SiriusStyleRd.Entities.ViewModels.Order;
using SiriusStyleRd.Repository.Repositories;
using SiriusStyleRd.Utility;

namespace SiriusStyleRd.Services.Services
{
    public interface IOrderService
    {
        Task<IViewModel> GetAll();
        Task<IViewModel> GetByNumber(string orderNumber);
        Task<IViewModel> Create(CreateOrderRequest order);
        Task<IViewModel> Update(UpdateOrderRequest order);
        Task<IViewModel> Cancel(CancelOrderRequest order);
    }

    public class OrderService : BaseService, IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrderService(IOrderRepository orderRepository, IMapper mapper, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<IViewModel> GetAll()
        {
            return await HandleErrors(Get);

            async Task<IViewModel> Get()
            {
                return Success(_mapper.Map<IEnumerable<OrderViewModel>>(await _orderRepository
                    .GetAll().ConfigureAwait(false)));
            }
        }

        public async Task<IViewModel> GetByNumber(string orderNumber)
        {
            return await HandleErrors(Get, orderNumber);

            async Task<IViewModel> Get(string id)
            {
                return Success(
                    _mapper.Map<OrderViewModel>(await _orderRepository
                        .GetByNumber(id)
                        .ConfigureAwait(false))
                );
            }
        }

        public async Task<IViewModel> Create(CreateOrderRequest order)
        {
            return await HandleErrors(Add, order);

            async Task<IViewModel> Add(CreateOrderRequest request)
            {
                bool exists;
                var mappedOrder = _mapper.Map<Order>(request);

                do
                {
                    mappedOrder.OrderNumber = CodeGenerator.Generate();
                    exists = await _orderRepository.CheckIfOrderNumberExists(mappedOrder.OrderNumber)
                        .ConfigureAwait(false);
                } while (exists);

                var orderResponse = await _orderRepository.Create(mappedOrder)
                    .ConfigureAwait(false);

                request.OrderNumber = orderResponse.OrderNumber;
                var productResponse = await _productRepository
                    .AssignToOrder(_mapper.Map<List<Product>>(GetProducts(request)))
                    .ConfigureAwait(false);

                return Success(_mapper.Map<OrderViewModel>(orderResponse));
            }
        }

        public async Task<IViewModel> Update(UpdateOrderRequest order)
        {
            return await HandleErrors(Modify, order);

            async Task<IViewModel> Modify(UpdateOrderRequest request)
            {
                var response = await _orderRepository.Update(_mapper.Map<Order>(request))
                    .ConfigureAwait(false);

                await UpdateOrderDetails(request);

                return Success(_mapper.Map<OrderViewModel>(response));
            }
        }

        public async Task<IViewModel> Cancel(CancelOrderRequest order)
        {
            return await HandleErrors(Modify, order);

            async Task<IViewModel> Modify(CancelOrderRequest request)
            {
                var products = await _productRepository.SetAsAvailable(request.OrderNumber)
                    .ConfigureAwait(false);

                var response = await _orderRepository.Cancel(_mapper.Map<Order>(request))
                    .ConfigureAwait(false);

                return Success(_mapper.Map<OrderViewModel>(response));
            }
        }

        private async Task UpdateOrderDetails(OrderRequest request)
        {
            var products = await _productRepository.GetByOrderNumber(request.OrderNumber)
                .ConfigureAwait(false);

            var productsToAssignOrder = GetProducts(request).ToList();

                foreach (var product in products)
                {
                    if (!request.ProductCodes.Contains(product.ProductCode))
                    {
                        productsToAssignOrder.Add(new AssignProductToOrderRequest
                        {
                            ProductCode = product.ProductCode,
                            OrderNumber = null,
                            Status = ProductStatus.Available
                        });
                    }
                }

            var productResponse = await _productRepository
                .AssignToOrder(_mapper.Map<List<Product>>(productsToAssignOrder))
                .ConfigureAwait(false);
        }

        private static ProductStatus GetProductStatus(OrderStatus status)
        {
            switch (status)
            {
                case OrderStatus.Pending:
                    return ProductStatus.Ordered;

                case OrderStatus.Paid:
                case OrderStatus.Shipped:
                    return ProductStatus.Sold;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static IEnumerable<AssignProductToOrderRequest> GetProducts(OrderRequest request)
        {
            return request.ProductCodes.Select(code => new AssignProductToOrderRequest
            {
                ProductCode = code,
                OrderNumber = request.OrderNumber,
                Status = GetProductStatus(request.Status)
            });
        }
    }
}