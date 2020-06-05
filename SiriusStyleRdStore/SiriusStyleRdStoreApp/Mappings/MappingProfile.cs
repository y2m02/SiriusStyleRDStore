using System;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using SiriusStyleRdStore.Entities.Enums;
using SiriusStyleRdStore.Entities.Models;
using SiriusStyleRdStore.Entities.Requests.Category;
using SiriusStyleRdStore.Entities.Requests.Customer;
using SiriusStyleRdStore.Entities.Requests.Order;
using SiriusStyleRdStore.Entities.Requests.Product;
using SiriusStyleRdStore.Entities.Requests.Size;
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
                    member => member.MapFrom(field => field.Size.Description));

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


            CreateMap<Order, OrderViewModel>()
                .ForMember(destination => destination.Status,
                    member => member.MapFrom(field => field.Status.GetDescription()))
                .ForMember(destination => destination.Customer,
                    member => member.MapFrom(field => field.Customer.FullName))
                .ForMember(destination => destination.ShippingCost,
                    member => member.MapFrom(field => field.ShippingCost.ToString("N")))
                .ForMember(destination => destination.Discount,
                    member => member.MapFrom(field => field.Discount.ToString("N")))
                .ForMember(destination => destination.SubTotal,
                    member => member.MapFrom(field => field.SubTotal.ToString("N")))
                .ForMember(destination => destination.Total,
                    member => member.MapFrom(field => field.Total.ToString("N")));

            CreateMap<OrderRequest, CreateOrderRequest>();
            CreateMap<CreateOrderRequest, Order>()
                .ForMember(destination => destination.CreatedOn,
                    member => member.MapFrom(field => DateTime.Now))
                .ForMember(destination => destination.ShippedOn,
                    member => member.MapFrom(field =>
                        field.Status == OrderStatus.Shipped ? (DateTime?) DateTime.Now : null))
                .ForMember(destination => destination.PaidOn,
                    member => member.MapFrom(field =>
                        field.Status == OrderStatus.Paid ? (DateTime?) DateTime.Now : null))
                .ForMember(destination => destination.CanceledOn,
                    member => member.MapFrom(field =>
                        field.Status == OrderStatus.Canceled ? (DateTime?) DateTime.Now : null))
                .ForMember(destination => destination.Discount,
                    member => member.MapFrom(field => field.Discount.GetValueOrDefault()))
                .ForMember(destination => destination.ShippingCost,
                    member => member.MapFrom(field => field.ShippingCost.GetValueOrDefault()));
            
            CreateMap<OrderRequest, UpdateOrderRequest>();
            CreateMap<UpdateOrderRequest, Order>()
                .ForMember(destination => destination.ShippedOn,
                    member => member.MapFrom(field =>
                        field.Status == OrderStatus.Shipped ? (DateTime?) DateTime.Now : null))
                .ForMember(destination => destination.PaidOn,
                    member => member.MapFrom(field =>
                        field.Status == OrderStatus.Paid ? (DateTime?) DateTime.Now : null))
                .ForMember(destination => destination.CanceledOn,
                    member => member.MapFrom(field =>
                        field.Status == OrderStatus.Canceled ? (DateTime?) DateTime.Now : null))
                .ForMember(destination => destination.Discount,
                    member => member.MapFrom(field => field.Discount.GetValueOrDefault()))
                .ForMember(destination => destination.ShippingCost,
                    member => member.MapFrom(field => field.ShippingCost.GetValueOrDefault()));


            //CreateMap<Category, GetCategoryResponse>();
            //CreateMap<CreateCategoryRequest, Category>();
            //CreateMap<UpdateCategoryRequest, Category>();
            //CreateMap<DeleteCategoryRequest, Category>()
            //    .ForMember(destination => destination.DeletedOn,
            //        member => member.MapFrom(field => DateTime.Now));


            //CreateMap<Person, GetPersonResponse>()
            //    .ForMember(destination => destination.Gender,
            //        member => member.MapFrom(field => field.Gender.GetDescription()))
            //    .ForMember(destination => destination.FullName,
            //        member => member.MapFrom(field => $"{field.FirstName} {field.LastName}"));


            //CreateMap<Person, GetCustomerResponse>()
            //    .ForMember(destination => destination.Gender,
            //        member => member.MapFrom(field => field.Gender.GetDescription()))
            //    .ForMember(destination => destination.FullName,
            //        member => member.MapFrom(field => $"{field.FirstName} {field.LastName}"))
            //    .ForMember(destination => destination.CustomerId,
            //        member => member.MapFrom(field => field.Customer.CustomerId));
            //CreateMap<CreateCustomerRequest, Person>()
            //    .ForMember(destination => destination.Customer,
            //        member => member.MapFrom(field => new Customer()))
            //    .ForMember(destination => destination.CreatedOn,
            //        member => member.MapFrom(field => DateTime.Now));
            //CreateMap<UpdateCustomerRequest, Person>();
            //CreateMap<DeletePersonRequest, Person>()
            //    .ForMember(destination => destination.DeletedOn,
            //        member => member.MapFrom(field => DateTime.Now));

            //CreateMap<User, GetUserResponse>()
            //    .ForMember(destination => destination.Password,
            //        member => member.MapFrom(field => Encrypt.DecryptString(field.Password, field.Username)));
            //CreateMap<CreateUserRequest, User>()
            //    .ForMember(destination => destination.IsActive,
            //        member => member.MapFrom(field => true))
            //    .ForMember(destination => destination.Password,
            //        member => member.MapFrom(field => Encrypt.EncryptString(field.Password, field.Username)));
            //CreateMap<UpdateUserRequest, User>();
            //CreateMap<ChangeUserStatusRequest, User>();
            //CreateMap<ValidateUserRequest, User>()
            //    .ForMember(destination => destination.Password,
            //        member => member.MapFrom(field => Encrypt.EncryptString(field.Password, field.Username)));
            //CreateMap<UpdatePasswordRequest, User>()
            //    .ForMember(destination => destination.Password,
            //        member => member.MapFrom(field => Encrypt.EncryptString(field.Password, field.Username)))
            //    .ForMember(destination => destination.Password,
            //        member => member.MapFrom(field => Encrypt.EncryptString(field.Password, field.Username)));


            //CreateMap<Person, GetEmployeeResponse>()
            //    .ForMember(destination => destination.Gender,
            //        member => member.MapFrom(field => field.Gender.GetDescription()))
            //    .ForMember(destination => destination.FullName,
            //        member => member.MapFrom(field => $"{field.FirstName} {field.LastName}"));
            //CreateMap<Employee, GetEmployee>();
            //CreateMap<CreateEmployee, Employee>()
            //    .ForMember(destination => destination.IsActive,
            //        member => member.MapFrom(field => true));
            //CreateMap<CreateEmployeeRequest, Person>()
            //    .ForMember(destination => destination.CreatedOn,
            //        member => member.MapFrom(field => DateTime.Now));
            //CreateMap<UpdateEmployee, Employee>();
            //CreateMap<UpdateEmployeeRequest, Person>();


            //CreateMap<Inventory, GetInventoryResponse>();
            //CreateMap<CreateInventoryRequest, Inventory>();
            //CreateMap<UpdateInventoryRequest, Inventory>();


            //CreateMap<ProductSpecification, GetProductSpecification>();
            //CreateMap<CreateProductSpecificationRequest, ProductSpecification>();
            //CreateMap<UpdateProductSpecificationRequest, ProductSpecification>();


            //CreateMap<Product, GetProductResponse>()
            //    .ForMember(destination => destination.CategoryDescription,
            //        member => member.MapFrom(field => field.Category.Description));
            //CreateMap<CreateProductRequest, Product>()
            //    .ForMember(destination => destination.IsActive,
            //        member => member.MapFrom(field => true));
            //CreateMap<UpdateProductRequest, Product>();
        }
    }
}