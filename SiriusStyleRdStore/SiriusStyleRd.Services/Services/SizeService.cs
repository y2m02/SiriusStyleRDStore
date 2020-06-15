using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SiriusStyleRd.Entities.Models;
using SiriusStyleRd.Entities.Requests.Size;
using SiriusStyleRd.Entities.ViewModels;
using SiriusStyleRd.Entities.ViewModels.Size;
using SiriusStyleRd.Repository.Repositories;

namespace SiriusStyleRd.Services.Services
{
    public interface ISizeService
    {
        Task<IViewModel> GetAll();
        Task<IViewModel> GetById(int sizeId);
        Task<IViewModel> Create(CreateSizeRequest size);
        Task<IViewModel> BatchCreate(List<CreateSizeRequest> sizes);
        Task<IViewModel> Update(UpdateSizeRequest size);
        Task<IViewModel> BatchUpdate(List<UpdateSizeRequest> sizes);
        Task<IViewModel> Delete(DeleteSizeRequest size);
        Task<IViewModel> BatchDelete(List<DeleteSizeRequest> sizes);
        Task<IViewModel> GetAllForDropDownList(int sizeId);
    }

    public class SizeService : BaseService, ISizeService
    {
        private readonly IMapper _mapper;
        private readonly ISizeRepository _sizeRepository;

        public SizeService(ISizeRepository sizeRepository, IMapper mapper)
        {
            _sizeRepository = sizeRepository;
            _mapper = mapper;
        }

        public async Task<IViewModel> GetAll()
        {
            return await HandleErrors(Get);

            async Task<IViewModel> Get()
            {
                return Success(_mapper.Map<IEnumerable<SizeViewModel>>(await _sizeRepository
                    .GetAll().ConfigureAwait(false)));
            }
        }

        public async Task<IViewModel> GetById(int sizeId)
        {
            return await HandleErrors(Get, sizeId);

            async Task<IViewModel> Get(int id)
            {
                return Success(
                    _mapper.Map<SizeViewModel>(await _sizeRepository
                        .GetById(id)
                        .ConfigureAwait(false))
                );
            }
        }

        public async Task<IViewModel> Create(CreateSizeRequest size)
        {
            return await HandleErrors(Add, size);

            async Task<IViewModel> Add(CreateSizeRequest request)
            {
                var response = await _sizeRepository.Create(_mapper.Map<Size>(request))
                    .ConfigureAwait(false);

                return Success(_mapper.Map<SizeViewModel>(response));
            }
        }

        public async Task<IViewModel> BatchCreate(List<CreateSizeRequest> sizes)
        {
            return await HandleErrors(Add, sizes);

            async Task<IViewModel> Add(List<CreateSizeRequest> request)
            {
                var response = await _sizeRepository.BatchCreate(_mapper.Map<List<Size>>(request))
                    .ConfigureAwait(false);

                return Success(_mapper.Map<List<SizeViewModel>>(response));
            }
        }

        public async Task<IViewModel> Update(UpdateSizeRequest size)
        {
            return await HandleErrors(Modify, size);

            async Task<IViewModel> Modify(UpdateSizeRequest request)
            {
                var response = await _sizeRepository.Update(_mapper.Map<Size>(request))
                    .ConfigureAwait(false);

                return Success(_mapper.Map<SizeViewModel>(response));
            }
        }

        public async Task<IViewModel> BatchUpdate(List<UpdateSizeRequest> sizes)
        {
            return await HandleErrors(Modify, sizes);

            async Task<IViewModel> Modify(List<UpdateSizeRequest> request)
            {
                var response = await _sizeRepository.BatchUpdate(_mapper.Map<List<Size>>(request))
                    .ConfigureAwait(false);

                return Success(_mapper.Map<List<SizeViewModel>>(response));
            }
        }

        public async Task<IViewModel> Delete(DeleteSizeRequest size)
        {
            return await HandleErrors(Remove, size);

            async Task<IViewModel> Remove(DeleteSizeRequest request)
            {
                var response = await _sizeRepository.Delete(_mapper.Map<Size>(request))
                    .ConfigureAwait(false);

                return Success(_mapper.Map<SizeViewModel>(response));
            }
        }

        public async Task<IViewModel> BatchDelete(List<DeleteSizeRequest> sizes)
        {
            return await HandleErrors(Remove, sizes);

            async Task<IViewModel> Remove(List<DeleteSizeRequest> request)
            {
                var response = await _sizeRepository.BatchDelete(_mapper.Map<List<Size>>(request))
                    .ConfigureAwait(false);

                return Success(_mapper.Map<List<SizeViewModel>>(response));
            }
        }

        public async Task<IViewModel> GetAllForDropDownList(int sizeId)
        {
            return await HandleErrors(Get);

            async Task<IViewModel> Get()
            {
                return Success(_mapper.Map<IEnumerable<SizeViewModel>>(await _sizeRepository
                    .GetAllForDropDownList(sizeId).ConfigureAwait(false)));
            }
        }
    }
}