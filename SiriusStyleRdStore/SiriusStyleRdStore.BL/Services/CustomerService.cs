using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SiriusStyleRdStore.Entities.Responses;
using SiriusStyleRdStore.Repositories.Repositories;

namespace SiriusStyleRdStore.BL.Services
{
    public interface ICustomerService
    {
        Task<IResponse> GetAll();

        Task<IResponse> GetById(int customerId);
        //Task<IResponse> Create(CreateCustomerRequest customer);
        //Task<IResponse> Update(UpdateCustomerRequest customer);
        //Task<IResponse> Delete(DeletePersonRequest customer);
    }

    public class CustomerService : BaseService, ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(IMapper mapper,
            ICustomerRepository customerRepository)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
        }

        public async Task<IResponse> GetAll()
        {
            return await HandleErrors(Get);

            async Task<IResponse> Get()
            {
                return Success(_mapper.Map<IEnumerable<GetCustomerResponse>>(await _customerRepository
                    .GetAll().ConfigureAwait(false)));
            }
        }

        public async Task<IResponse> GetById(int customerId)
        {
            return await HandleErrors(Get, customerId);

            async Task<IResponse> Get(int id)
            {
                return Success(
                    _mapper.Map<GetCustomerResponse>(await _customerRepository
                        .GetById(id)
                        .ConfigureAwait(false))
                );
            }
        }

        //public async Task<IResponse> Create(CreateCustomerRequest customer)
        //{
        //    return await HandleErrors(Add, customer);

        //    async Task<IResponse> Add(CreateCustomerRequest request)
        //    {
        //        var response = await _customerRepository.Create(_mapper.Map<Person>(request))
        //            .ConfigureAwait(false);

        //        return Success(_mapper.Map<GetCustomerResponse>(response));
        //    }
        //}

        //public async Task<IResponse> Update(UpdateCustomerRequest customer)
        //{
        //    return await HandleErrors(Modify, customer);

        //    async Task<IResponse> Modify(UpdateCustomerRequest request)
        //    {
        //        var response = await _customerRepository.Update(_mapper.Map<Person>(request))
        //            .ConfigureAwait(false);

        //        return Success(_mapper.Map<GetCustomerResponse>(response));
        //    }
        //}

        //public async Task<IResponse> Delete(DeletePersonRequest customer)
        //{
        //    return await HandleErrors(Remove, customer);

        //    async Task<IResponse> Remove(DeletePersonRequest request)
        //    {
        //        var response = await _customerRepository.Delete(_mapper.Map<Person>(request))
        //            .ConfigureAwait(false);

        //        return Success(_mapper.Map<GetCustomerResponse>(response));
        //    }
        //}
    }
}