using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SiriusStyleRdStore.Entities.Enums;
using SiriusStyleRdStore.Entities.ViewModels;
using SiriusStyleRdStore.Entities.ViewModels.Category;
using SiriusStyleRdStore.Entities.ViewModels.Customer;
using SiriusStyleRdStore.Entities.ViewModels.Item;
using SiriusStyleRdStore.Entities.ViewModels.Size;
using SiriusStyleRdStore.Repositories.Repositories;

namespace SiriusStyleRdStore.BL.Services
{
    public interface IItemService
    {
        Task<IViewModel> Get(List<ItemType> items);
    }
    public class ItemService : BaseService, IItemService
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;
        private readonly ISizeRepository _sizeRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ItemService(IMapper mapper, ICustomerRepository customerRepository,
            ISizeRepository sizeRepository, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
            _sizeRepository = sizeRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IViewModel> Get(List<ItemType> items)
        {
            var itemModel = new ItemViewModel();

            if (items.Contains(ItemType.Category))
            {
                itemModel.Categories = _mapper.Map<IEnumerable<CategoryViewModel>>(await _categoryRepository
                    .GetAll().ConfigureAwait(false));
            }

            if (items.Contains(ItemType.Customer))
            {
                itemModel.Customers = _mapper.Map<IEnumerable<CustomerViewModel>>(await _customerRepository
                    .GetAll().ConfigureAwait(false));
            }

            if (items.Contains(ItemType.Size))
            {
                itemModel.Sizes = _mapper.Map<IEnumerable<SizeViewModel>>(await _sizeRepository
                    .GetAll().ConfigureAwait(false));
            }

            return Success(itemModel);
        }
    }
}
