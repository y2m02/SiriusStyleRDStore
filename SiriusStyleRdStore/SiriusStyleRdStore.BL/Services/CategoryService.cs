using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SiriusStyleRdStore.Entities.Models;
using SiriusStyleRdStore.Entities.Requests.Category;
using SiriusStyleRdStore.Entities.ViewModels;
using SiriusStyleRdStore.Entities.ViewModels.Category;
using SiriusStyleRdStore.Repositories.Repositories;

namespace SiriusStyleRdStore.BL.Services
{
    public interface ICategoryService
    {
        Task<IViewModel> GetAll();
        Task<IViewModel> GetById(int categoryId);
        Task<IViewModel> Create(CreateCategoryRequest category);
        Task<IViewModel> BatchCreate(List<CreateCategoryRequest> categories);
        Task<IViewModel> Update(UpdateCategoryRequest category);
        Task<IViewModel> BatchUpdate(List<UpdateCategoryRequest> categories);
        Task<IViewModel> Delete(DeleteCategoryRequest category);
        Task<IViewModel> BatchDelete(List<DeleteCategoryRequest> categories);
        Task<IViewModel> GetAllForDropDownList(int categoryId);
    }

    public class CategoryService : BaseService, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<IViewModel> GetAll()
        {
            return await HandleErrors(Get);

            async Task<IViewModel> Get()
            {
                return Success(_mapper.Map<IEnumerable<CategoryViewModel>>(await _categoryRepository
                    .GetAll().ConfigureAwait(false)));
            }
        }

        public async Task<IViewModel> GetById(int categoryId)
        {
            return await HandleErrors(Get, categoryId);

            async Task<IViewModel> Get(int id)
            {
                return Success(
                    _mapper.Map<CategoryViewModel>(await _categoryRepository
                        .GetById(id)
                        .ConfigureAwait(false))
                );
            }
        }

        public async Task<IViewModel> Create(CreateCategoryRequest category)
        {
            return await HandleErrors(Add, category);

            async Task<IViewModel> Add(CreateCategoryRequest request)
            {
                var response = await _categoryRepository.Create(_mapper.Map<Category>(request))
                    .ConfigureAwait(false);

                return Success(_mapper.Map<CategoryViewModel>(response));
            }
        }

        public async Task<IViewModel> BatchCreate(List<CreateCategoryRequest> categories)
        {
            return await HandleErrors(Add, categories);

            async Task<IViewModel> Add(List<CreateCategoryRequest> request)
            {
                var response = await _categoryRepository.BatchCreate(_mapper.Map<List<Category>>(request))
                    .ConfigureAwait(false);

                return Success(_mapper.Map<List<CategoryViewModel>>(response));
            }
        }

        public async Task<IViewModel> Update(UpdateCategoryRequest category)
        {
            return await HandleErrors(Modify, category);

            async Task<IViewModel> Modify(UpdateCategoryRequest request)
            {
                var response = await _categoryRepository.Update(_mapper.Map<Category>(request))
                    .ConfigureAwait(false);

                return Success(_mapper.Map<CategoryViewModel>(response));
            }
        }

        public async Task<IViewModel> BatchUpdate(List<UpdateCategoryRequest> categories)
        {
            return await HandleErrors(Modify, categories);

            async Task<IViewModel> Modify(List<UpdateCategoryRequest> request)
            {
                var response = await _categoryRepository.BatchUpdate(_mapper.Map<List<Category>>(request))
                    .ConfigureAwait(false);

                return Success(_mapper.Map<List<CategoryViewModel>>(response));
            }
        }

        public async Task<IViewModel> Delete(DeleteCategoryRequest category)
        {
            return await HandleErrors(Remove, category);

            async Task<IViewModel> Remove(DeleteCategoryRequest request)
            {
                var response = await _categoryRepository.Delete(_mapper.Map<Category>(request))
                    .ConfigureAwait(false);

                return Success(_mapper.Map<CategoryViewModel>(response));
            }
        }

        public async Task<IViewModel> BatchDelete(List<DeleteCategoryRequest> categories)
        {
            return await HandleErrors(Remove, categories);

            async Task<IViewModel> Remove(List<DeleteCategoryRequest> request)
            {
                var response = await _categoryRepository.BatchDelete(_mapper.Map<List<Category>>(request))
                    .ConfigureAwait(false);

                return Success(_mapper.Map<List<CategoryViewModel>>(response));
            }
        }

        public async Task<IViewModel> GetAllForDropDownList(int categoryId)
        {
            return await HandleErrors(Get);

            async Task<IViewModel> Get()
            {
                return Success(_mapper.Map<IEnumerable<CategoryViewModel>>(await _categoryRepository
                    .GetAllForDropDownList(categoryId).ConfigureAwait(false)));
            }
        }
    }
}