using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SiriusStyleRd.Entities.Models;
using SiriusStyleRd.Entities.Requests.Bale;
using SiriusStyleRd.Entities.ViewModels;
using SiriusStyleRd.Entities.ViewModels.Bale;
using SiriusStyleRd.Repository.Repositories;

namespace SiriusStyleRd.Services.Services
{
    public interface IBaleService
    {
        Task<IViewModel> GetAll();
        Task<IViewModel> GetById(int baleId);
        Task<IViewModel> Create(CreateBaleRequest bale);
        Task<IViewModel> BatchCreate(List<CreateBaleRequest> bales);
        Task<IViewModel> Update(UpdateBaleRequest bale);
        Task<IViewModel> BatchUpdate(List<UpdateBaleRequest> bales);
        Task<IViewModel> Delete(DeleteBaleRequest bale);
        Task<IViewModel> BatchDelete(List<DeleteBaleRequest> bales);
        Task<IViewModel> GetAllForDropDownList(int baleId);
        Task<IViewModel> GetAllNotCompleteUploaded();
    }

    public class BaleService : BaseService, IBaleService
    {
        private readonly IBaleRepository _baleRepository;
        private readonly IMapper _mapper;

        public BaleService(IMapper mapper, IBaleRepository baleRepository)
        {
            _mapper = mapper;
            _baleRepository = baleRepository;
        }

        public async Task<IViewModel> GetAll()
        {
            return await HandleErrors(Get);

            async Task<IViewModel> Get()
            {
                return Success(_mapper.Map<IEnumerable<BaleViewModel>>(await _baleRepository
                    .GetAll().ConfigureAwait(false)));
            }
        }

        public async Task<IViewModel> GetById(int baleId)
        {
            return await HandleErrors(Get, baleId);

            async Task<IViewModel> Get(int id)
            {
                return Success(
                    _mapper.Map<BaleViewModel>(await _baleRepository
                        .GetById(id)
                        .ConfigureAwait(false))
                );
            }
        }

        public async Task<IViewModel> Create(CreateBaleRequest bale)
        {
            return await HandleErrors(Add, bale);

            async Task<IViewModel> Add(CreateBaleRequest request)
            {
                var response = await _baleRepository.Create(_mapper.Map<Bale>(request))
                    .ConfigureAwait(false);

                return Success(_mapper.Map<BaleViewModel>(response));
            }
        }

        public async Task<IViewModel> BatchCreate(List<CreateBaleRequest> bales)
        {
            return await HandleErrors(Add, bales);

            async Task<IViewModel> Add(List<CreateBaleRequest> request)
            {
                var response = await _baleRepository.BatchCreate(_mapper.Map<List<Bale>>(request))
                    .ConfigureAwait(false);

                return Success(_mapper.Map<List<BaleViewModel>>(response));
            }
        }

        public async Task<IViewModel> Update(UpdateBaleRequest bale)
        {
            return await HandleErrors(Modify, bale);

            async Task<IViewModel> Modify(UpdateBaleRequest request)
            {
                var response = await _baleRepository.Update(_mapper.Map<Bale>(request))
                    .ConfigureAwait(false);

                return Success(_mapper.Map<BaleViewModel>(response));
            }
        }

        public async Task<IViewModel> BatchUpdate(List<UpdateBaleRequest> bales)
        {
            return await HandleErrors(Modify, bales);

            async Task<IViewModel> Modify(List<UpdateBaleRequest> request)
            {
                var response = await _baleRepository.BatchUpdate(_mapper.Map<List<Bale>>(request))
                    .ConfigureAwait(false);

                return Success(_mapper.Map<List<BaleViewModel>>(response));
            }
        }

        public async Task<IViewModel> Delete(DeleteBaleRequest bale)
        {
            return await HandleErrors(Remove, bale);

            async Task<IViewModel> Remove(DeleteBaleRequest request)
            {
                var response = await _baleRepository.Delete(_mapper.Map<Bale>(request))
                    .ConfigureAwait(false);

                return Success(_mapper.Map<BaleViewModel>(response));
            }
        }

        public async Task<IViewModel> BatchDelete(List<DeleteBaleRequest> bales)
        {
            return await HandleErrors(Remove, bales);

            async Task<IViewModel> Remove(List<DeleteBaleRequest> request)
            {
                var response = await _baleRepository.BatchDelete(_mapper.Map<List<Bale>>(request))
                    .ConfigureAwait(false);

                return Success(_mapper.Map<List<BaleViewModel>>(response));
            }
        }

        public async Task<IViewModel> GetAllForDropDownList(int baleId)
        {
            return await HandleErrors(Get);

            async Task<IViewModel> Get()
            {
                return Success(_mapper.Map<IEnumerable<BaleViewModel>>(await _baleRepository
                    .GetAllForDropDownList(baleId).ConfigureAwait(false)));
            }
        }

        public async Task<IViewModel> GetAllNotCompleteUploaded()
        {
            return await HandleErrors(Get);

            async Task<IViewModel> Get()
            {
                return Success(_mapper.Map<IEnumerable<BaleViewModel>>(await _baleRepository
                    .GetAllNotCompleteUploaded().ConfigureAwait(false)));
            }
        }
    }
}