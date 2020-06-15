using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SiriusStyleRd.Entities.Enums;
using SiriusStyleRd.Entities.ViewModels;
using SiriusStyleRd.Entities.ViewModels.Bale;
using SiriusStyleRd.Entities.ViewModels.Category;
using SiriusStyleRd.Entities.ViewModels.Customer;
using SiriusStyleRd.Entities.ViewModels.Item;
using SiriusStyleRd.Entities.ViewModels.Size;
using SiriusStyleRd.Repository.Repositories;

namespace SiriusStyleRd.Services.Services
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
        private readonly IBaleRepository _baleRepository;

        public ItemService(IMapper mapper, ICustomerRepository customerRepository,
            ISizeRepository sizeRepository, ICategoryRepository categoryRepository, IBaleRepository baleRepository)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
            _sizeRepository = sizeRepository;
            _categoryRepository = categoryRepository;
            _baleRepository = baleRepository;
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

            if (items.Contains(ItemType.Bale))
            {
                itemModel.Bales = _mapper.Map<IEnumerable<BaleViewModel>>(await _baleRepository
                    .GetAllNotCompleteUploaded().ConfigureAwait(false));
            }

            return Success(itemModel);
        }
    }
}
