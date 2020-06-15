using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SiriusStyleRd.Entities.Models;
using SiriusStyleRd.Entities.Requests.Customer;
using SiriusStyleRd.Entities.ViewModels;
using SiriusStyleRd.Entities.ViewModels.Customer;
using SiriusStyleRd.Repository.Repositories;

namespace SiriusStyleRd.Services.Services
{
    public interface ICustomerService
    {
        Task<IViewModel> GetAll();

        Task<IViewModel> GetById(int customerId);
        Task<IViewModel> Create(CreateCustomerRequest customer);
        Task<IViewModel> BatchCreate(List<CreateCustomerRequest> customers);
        Task<IViewModel> Update(UpdateCustomerRequest customer);
        Task<IViewModel> BatchUpdate(List<UpdateCustomerRequest> customers);
        Task<IViewModel> Delete(DeleteCustomerRequest customer);
        Task<IViewModel> BatchDelete(List<DeleteCustomerRequest> customers);
        Task<IViewModel> GetAllForDropDownList(int customerId);
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

        public async Task<IViewModel> GetAll()
        {
            return await HandleErrors(Get);

            async Task<IViewModel> Get()
            {
                return Success(_mapper.Map<IEnumerable<CustomerViewModel>>(await _customerRepository
                    .GetAll().ConfigureAwait(false)));
            }
        }

        public async Task<IViewModel> GetById(int customerId)
        {
            return await HandleErrors(Get, customerId);

            async Task<IViewModel> Get(int id)
            {
                return Success(
                    _mapper.Map<CustomerViewModel>(await _customerRepository
                        .GetById(id)
                        .ConfigureAwait(false))
                );
            }
        }

        public async Task<IViewModel> Create(CreateCustomerRequest customer)
        {
            return await HandleErrors(Add, customer);

            async Task<IViewModel> Add(CreateCustomerRequest request)
            {
                var response = await _customerRepository.Create(_mapper.Map<Customer>(request))
                    .ConfigureAwait(false);

                return Success(_mapper.Map<CustomerViewModel>(response));
            }
        }

        public async Task<IViewModel> BatchCreate(List<CreateCustomerRequest> customers)
        {
            return await HandleErrors(Add, customers);

            async Task<IViewModel> Add(List<CreateCustomerRequest> request)
            {
                var response = await _customerRepository.BatchCreate(_mapper.Map<List<Customer>>(request))
                    .ConfigureAwait(false);

                return Success(_mapper.Map<List<CustomerViewModel>>(response));
            }
        }

        public async Task<IViewModel> Update(UpdateCustomerRequest customer)
        {
            return await HandleErrors(Modify, customer);

            async Task<IViewModel> Modify(UpdateCustomerRequest request)
            {
                var response = await _customerRepository.Update(_mapper.Map<Customer>(request))
                    .ConfigureAwait(false);

                return Success(_mapper.Map<CustomerViewModel>(response));
            }
        }

        public async Task<IViewModel> BatchUpdate(List<UpdateCustomerRequest> customers)
        {
            return await HandleErrors(Modify, customers);

            async Task<IViewModel> Modify(List<UpdateCustomerRequest> request)
            {
                var response = await _customerRepository.BatchUpdate(_mapper.Map<List<Customer>>(request))
                    .ConfigureAwait(false);

                return Success(_mapper.Map<List<CustomerViewModel>>(response));
            }
        }

        public async Task<IViewModel> Delete(DeleteCustomerRequest customer)
        {
            return await HandleErrors(Remove, customer);

            async Task<IViewModel> Remove(DeleteCustomerRequest request)
            {
                var response = await _customerRepository.Delete(_mapper.Map<Customer>(request))
                    .ConfigureAwait(false);

                return Success(_mapper.Map<CustomerViewModel>(response));
            }
        }

        public async Task<IViewModel> BatchDelete(List<DeleteCustomerRequest> customers)
        {
            return await HandleErrors(Remove, customers);

            async Task<IViewModel> Remove(List<DeleteCustomerRequest> request)
            {
                var response = await _customerRepository.BatchDelete(_mapper.Map<List<Customer>>(request))
                    .ConfigureAwait(false);

                return Success(_mapper.Map<List<CustomerViewModel>>(response));
            }
        }

        public async Task<IViewModel> GetAllForDropDownList(int customerId)
        {
            return await HandleErrors(Get);

            async Task<IViewModel> Get()
            {
                return Success(_mapper.Map<IEnumerable<CustomerViewModel>>(await _customerRepository
                    .GetAllForDropDownList(customerId).ConfigureAwait(false)));
            }
        }
    }
}