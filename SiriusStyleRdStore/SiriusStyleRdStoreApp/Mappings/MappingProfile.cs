using System;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using SiriusStyleRdStore.Entities.Enums;
using SiriusStyleRdStore.Entities.Models;
using SiriusStyleRdStore.Entities.Requests.Bale;
using SiriusStyleRdStore.Entities.Requests.Category;
using SiriusStyleRdStore.Entities.Requests.Customer;
using SiriusStyleRdStore.Entities.Requests.Order;
using SiriusStyleRdStore.Entities.Requests.Product;
using SiriusStyleRdStore.Entities.Requests.Size;
using SiriusStyleRdStore.Entities.ViewModels.Bale;
using SiriusStyleRdStore.Entities.ViewModels.Category;
using SiriusStyleRdStore.Entities.ViewModels.Customer;
using SiriusStyleRdStore.Entities.ViewModels.Order;
using SiriusStyleRdStore.Entities.ViewModels.Product;
using SiriusStyleRdStore.Entities.ViewModels.Size;
using SiriusStyleRdStore.Utility.Extensions;

namespace SiriusStyleRdStoreApp.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile(IWebHostEnvironment webHostEnvironment)
        {
            CreateMap<Customer, CustomerViewModel>();


            CreateMap<CustomerRequest, CreateCustomerRequest>();

            CreateMap<CustomerViewModel, CreateCustomerRequest>();
            CreateMap<CreateCustomerRequest, Customer>()
                .ForMember(destination => destination.CreatedOn,
                    member => member.MapFrom(field => DateTime.Now));

            CreateMap<CustomerViewModel, UpdateCustomerRequest>();
            CreateMap<UpdateCustomerRequest, Customer>();

            CreateMap<CustomerViewModel, DeleteCustomerRequest>();
            CreateMap<DeleteCustomerRequest, Customer>()
                .ForMember(destination => destination.DeletedOn,
                    member => member.MapFrom(field => DateTime.Now));


            CreateMap<Category, CategoryViewModel>();

            CreateMap<CategoryViewModel, CreateCategoryRequest>();
            CreateMap<CreateCategoryRequest, Category>();

            CreateMap<CategoryViewModel, UpdateCategoryRequest>();
            CreateMap<UpdateCategoryRequest, Category>();

            CreateMap<CategoryViewModel, DeleteCategoryRequest>();
            CreateMap<DeleteCategoryRequest, Category>()
                .ForMember(destination => destination.DeletedOn,
                    member => member.MapFrom(field => DateTime.Now));


            CreateMap<Size, SizeViewModel>();

            CreateMap<SizeViewModel, CreateSizeRequest>();
            CreateMap<CreateSizeRequest, Size>();

            CreateMap<SizeViewModel, UpdateSizeRequest>();
            CreateMap<UpdateSizeRequest, Size>();

            CreateMap<SizeViewModel, DeleteSizeRequest>();
            CreateMap<DeleteSizeRequest, Size>()
                .ForMember(destination => destination.DeletedOn,
                    member => member.MapFrom(field => DateTime.Now));


            CreateMap<Product, ProductViewModel>()
                .ForMember(destination => destination.Status,
                    member => member.MapFrom(field => field.Status.GetDescription()))
                .ForMember(destination => destination.Category,
                    member => member.MapFrom(field => field.Category.Description))
                .ForMember(destination => destination.Size,
                    member => member.MapFrom(field => field.Size.Description))
                .ForMember(destination => destination.Bale,
                    member => member.MapFrom(field => $"{field.BaleId} - {field.Description}"));

            CreateMap<ProductRequest, CreateProductRequest>();
            CreateMap<CreateProductRequest, Product>()
                .ForMember(destination => destination.Status,
                    member => member.MapFrom(field => ProductStatus.Available))
                .ForMember(destination => destination.Image,
                    member => member.MapFrom(field => field.Image.UploadFile(webHostEnvironment)));

            CreateMap<ProductRequest, UpdateProductRequest>();
            CreateMap<UpdateProductRequest, Product>()
                .ForMember(destination => destination.Image,
                    member => member.MapFrom(field => field.Image.UploadFile(webHostEnvironment)));

            CreateMap<AssignProductToOrderRequest, Product>();

            CreateMap<CancelOrderRequest, Order>();


            CreateMap<Order, OrderViewModel>()
                .ForMember(destination => destination.Status,
                    member => member.MapFrom(field => field.Status.GetDescription()))
                .ForMember(destination => destination.ShippingCost,
                    member => member.MapFrom(field => field.ShippingCost.ToNumericFormat(2)))
                .ForMember(destination => destination.Discount,
                    member => member.MapFrom(field => field.Discount.ToNumericFormat(2)))
                .ForMember(destination => destination.ShippedOn,
                    member => member.MapFrom(field => field.ShippedOn.ToFormattedString(DateFormat.ddMMyyyy)))
                .ForMember(destination => destination.PaidOn,
                    member => member.MapFrom(field => field.PaidOn.ToFormattedString(DateFormat.ddMMyyyy)))
                .ForMember(destination => destination.Customer,
                    member => member.MapFrom(field => field.Customer.FullName))
                .ForMember(destination => destination.CreatedOn,
                    member => member.MapFrom(field => field.CreatedOn.Date));

            CreateMap<OrderViewModel, OrderRequest>()
                .ForMember(destination => destination.Status,
                    member => member.MapFrom(field => field.Status.GetEnumValueFromDescription<OrderStatus>()));

            CreateMap<OrderRequest, CreateOrderRequest>();
            CreateMap<CreateOrderRequest, Order>()
                .ForMember(destination => destination.CreatedOn,
                    member => member.MapFrom(field => DateTime.Now))
                .ForMember(destination => destination.ShippedOn,
                    member => member.MapFrom(field =>
                        field.Status == OrderStatus.Shipped ? (DateTime?)DateTime.Now : null))
                .ForMember(destination => destination.PaidOn,
                    member => member.MapFrom(field =>
                        field.Status == OrderStatus.Paid ? (DateTime?)DateTime.Now : null))
                .ForMember(destination => destination.Discount,
                    member => member.MapFrom(field => field.Discount.GetValueOrDefault()))
                .ForMember(destination => destination.ShippingCost,
                    member => member.MapFrom(field => field.ShippingCost.GetValueOrDefault()));

            CreateMap<OrderRequest, UpdateOrderRequest>();
            CreateMap<UpdateOrderRequest, Order>()
                .ForMember(destination => destination.ShippedOn,
                    member => member.MapFrom(field =>
                        field.Status == OrderStatus.Shipped ? (DateTime?)DateTime.Now : null))
                .ForMember(destination => destination.PaidOn,
                    member => member.MapFrom(field =>
                        field.Status == OrderStatus.Paid ? (DateTime?)DateTime.Now : null))
                .ForMember(destination => destination.Discount,
                    member => member.MapFrom(field => field.Discount.GetValueOrDefault()))
                .ForMember(destination => destination.ShippingCost,
                    member => member.MapFrom(field => field.ShippingCost.GetValueOrDefault()));

            CreateMap<OrderRequest, CancelOrderRequest>();
            CreateMap<CancelOrderRequest, Order>();

            CreateMap<Bale, BaleViewModel>()
                .ForMember(destination => destination.IdAndDescription,
                    member => member.MapFrom(field => $"{field.BaleId} - {field.Description}"));

            CreateMap<BaleViewModel, CreateBaleRequest>();
            CreateMap<CreateBaleRequest, Bale>()
                .ForMember(destination => destination.CompleteUploaded,
                    member => member.MapFrom(field => false));

            CreateMap<BaleViewModel, UpdateBaleRequest>();
            CreateMap<UpdateBaleRequest, Bale>()
                .ForMember(destination => destination.CompleteUploaded,
                    member => member.MapFrom(field => false));

            CreateMap<BaleViewModel, DeleteBaleRequest>();
            CreateMap<DeleteBaleRequest, Bale>()
                .ForMember(destination => destination.DeletedOn,
                    member => member.MapFrom(field => DateTime.Now));
        }
    }
}