using AutoMapper;
using SiriusStyleRdStore.Entities.Models;
using SiriusStyleRdStore.Entities.Responses;

namespace SiriusStyleRdStoreApp.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, GetCustomerResponse>();

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