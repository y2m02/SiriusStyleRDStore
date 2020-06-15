using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SiriusStyleRd.Entities.Models;

namespace SiriusStyleRd.Repository.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAll();
        Task<Customer> GetById(int customerId);
        Task<Customer> Create(Customer customer);
        Task<IEnumerable<Customer>> BatchCreate(List<Customer> customers);
        Task<Customer> Update(Customer customer);
        Task<IEnumerable<Customer>> BatchUpdate(List<Customer> customers);
        Task<Customer> Delete(Customer customer);
        Task<IEnumerable<Customer>> BatchDelete(List<Customer> customers);
        Task<IEnumerable<Customer>> GetAllForDropDownList(int customerId);
    }

    public class CustomerRepository : BaseRepository, ICustomerRepository
    {
        public CustomerRepository(SiriusStyleRdContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await Context.Customer
                .Where(w => w.DeletedOn == null)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<Customer> GetById(int customerId)
        {
            return await Context.Customer
                .SingleAsync(w => w.CustomerId == customerId && w.DeletedOn == null)
                .ConfigureAwait(false);
        }

        public async Task<Customer> Create(Customer customer)
        {
            await Context.Customer.AddAsync(customer);
            await Save();

            return customer;
        }

        public async Task<IEnumerable<Customer>> BatchCreate(List<Customer> customers)
        {
            await Context.Customer.AddRangeAsync(customers);
            await Save();

            return customers;
        }

        public async Task<Customer> Update(Customer customer)
        {
            Context.Attach(customer);
            AddPropertiesToModify(customer, new List<string>
            {
                nameof(customer.FullName),
                nameof(customer.City),
                nameof(customer.Sector),
                nameof(customer.Address),
                nameof(customer.PhoneNumber),
                nameof(customer.Facebook),
                nameof(customer.Instagram)
            });

            await Save();

            return customer;
        }

        public async Task<IEnumerable<Customer>> BatchUpdate(List<Customer> customers)
        {
            foreach (var customer in customers)
            {
                Context.Attach(customer);
                AddPropertiesToModify(customer, new List<string>
                {
                    nameof(customer.FullName),
                    nameof(customer.City),
                    nameof(customer.Sector),
                    nameof(customer.Address),
                    nameof(customer.PhoneNumber),
                    nameof(customer.Facebook),
                    nameof(customer.Instagram)
                });

                await Save();
            }

            return customers;
        }

        public async Task<Customer> Delete(Customer customer)
        {
            Context.Attach(customer);
            AddPropertiesToModify(customer, new List<string>
            {
                nameof(customer.DeletedOn)
            });

            await Save();

            return customer;
        }

        public async Task<IEnumerable<Customer>> BatchDelete(List<Customer> customers)
        {
            foreach (var customer in customers)
            {
                Context.Attach(customer);
                AddPropertiesToModify(customer, new List<string>
                {
                    nameof(customer.DeletedOn)
                });

                await Save();
            }

            return customers;
        }

        public async Task<IEnumerable<Customer>> GetAllForDropDownList(int customerId)
        {
            return await Context.Customer
                .OrderBy(w => w.FullName)
                .Where(w => w.DeletedOn == null || w.CustomerId == customerId)
                .ToListAsync()
                .ConfigureAwait(false);
        }
    }
}