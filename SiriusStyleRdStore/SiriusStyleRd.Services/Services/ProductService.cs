using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SiriusStyleRd.Entities.Models;
using SiriusStyleRd.Entities.Requests.Product;
using SiriusStyleRd.Entities.ViewModels;
using SiriusStyleRd.Entities.ViewModels.Product;
using SiriusStyleRd.Repository.Repositories;
using SiriusStyleRd.Utility;

namespace SiriusStyleRd.Services.Services
{
    public interface IProductService
    {
        Task<IViewModel> GetAll();
        Task<IViewModel> GetByCode(string productCode);
        Task<IViewModel> Create(CreateProductRequest product);
        Task<IViewModel> BatchCreate(List<CreateProductRequest> products);
        Task<IViewModel> Update(UpdateProductRequest product);
        Task<IViewModel> BatchUpdate(List<UpdateProductRequest> products);
        Task<IViewModel> GetByOrderNumber(string orderNumber);
        Task<IViewModel> GetAllForOrderDetails(string orderNumber);
    }

    public class ProductService : BaseService, IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IViewModel> GetAll()
        {
            return await HandleErrors(Get);

            async Task<IViewModel> Get()
            {
                return Success(_mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository
                    .GetAll().ConfigureAwait(false)));
            }
        }

        public async Task<IViewModel> GetByCode(string productCode)
        {
            return await HandleErrors(Get, productCode);

            async Task<IViewModel> Get(string id)
            {
                return Success(
                    _mapper.Map<ProductViewModel>(await _productRepository
                        .GetByCode(id)
                        .ConfigureAwait(false))
                );
            }
        }

        public async Task<IViewModel> Create(CreateProductRequest product)
        {
            return await HandleErrors(Add, product);

            async Task<IViewModel> Add(CreateProductRequest request)
            {
                bool exists;
                var mappedProduct = _mapper.Map<Product>(request);

                do
                {
                    mappedProduct.ProductCode = CodeGenerator.Generate();
                    exists = await _productRepository.CheckIfProductCodeExists(mappedProduct.ProductCode).ConfigureAwait(false); ;
                } while (exists);

                var response = await _productRepository.Create(mappedProduct)
                    .ConfigureAwait(false);

                return Success(_mapper.Map<ProductViewModel>(response));
            }
        }

        public async Task<IViewModel> BatchCreate(List<CreateProductRequest> products)
        {
            return await HandleErrors(Add, products);

            async Task<IViewModel> Add(List<CreateProductRequest> request)
            {
                var mappedProducts = _mapper.Map<List<Product>>(request);

                foreach (var product in mappedProducts)
                {
                    bool exists;
                    do
                    {
                        product.ProductCode = CodeGenerator.Generate();
                        exists = await _productRepository.CheckIfProductCodeExists(product.ProductCode).ConfigureAwait(false); ;
                    } while (exists);
                }

                var response = await _productRepository.BatchCreate(mappedProducts)
                    .ConfigureAwait(false);

                return Success(_mapper.Map<List<ProductViewModel>>(response));
            }
        }

        public async Task<IViewModel> Update(UpdateProductRequest product)
        {
            return await HandleErrors(Modify, product);

            async Task<IViewModel> Modify(UpdateProductRequest request)
            {
                var response = await _productRepository.Update(_mapper.Map<Product>(request), request.UpdateImage)
                    .ConfigureAwait(false);

                return Success(_mapper.Map<ProductViewModel>(response));
            }
        }

        public async Task<IViewModel> BatchUpdate(List<UpdateProductRequest> products)
        {
            return await HandleErrors(Modify, products);

            async Task<IViewModel> Modify(List<UpdateProductRequest> request)
            {
                var response = await _productRepository.BatchUpdate(_mapper.Map<List<Product>>(request))
                    .ConfigureAwait(false);

                return Success(_mapper.Map<List<ProductViewModel>>(response));
            }
        }

        public async Task<IViewModel> GetByOrderNumber(string orderNumber)
        {
            return await HandleErrors(Get);

            async Task<IViewModel> Get()
            {
                return Success(_mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository
                    .GetByOrderNumber(orderNumber).ConfigureAwait(false)));
            }
        }

        public async Task<IViewModel> GetAllForOrderDetails(string orderNumber)
        {
            return await HandleErrors(Get);

            async Task<IViewModel> Get()
            {
                return Success(_mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository
                    .GetAllForOrderDetails(orderNumber).ConfigureAwait(false)));
            }
        }
    }
}