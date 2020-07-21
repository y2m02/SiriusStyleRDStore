using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SiriusStyleRd.Entities.Models;
using SiriusStyleRd.Utility.Extensions;

namespace SiriusStyleRd.Repository.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAll();
        Task<Order> GetByNumber(string orderNumber);
        Task<Order> Create(Order order);
        Task<Order> Update(Order order);
        Task<bool> CheckIfOrderNumberExists(string orderNumber);
        Task<Order> Cancel(Order order);
    }

    public class OrderRepository : BaseRepository, IOrderRepository
    {
        public OrderRepository(SiriusStyleRdContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await Context.Order
                .Include(w => w.Customer)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<Order> GetByNumber(string orderNumber)
        {
            return await Context.Order
                .Include(w => w.Customer)
                .SingleAsync(w => w.OrderNumber == orderNumber)
                .ConfigureAwait(false);
        }

        public async Task<Order> Create(Order order)
        {
            await Context.Order.AddAsync(order);
            await Save();

            return order;
        }

        public async Task<Order> Update(Order order)
        {
            Context.Attach(order);
            AddPropertiesToModify(order, new List<string>
            {
                nameof(order.Status),
                nameof(order.PaymentType),
                nameof(order.CustomerId),
                //nameof(order.ShippedOn),
                //nameof(order.PaidOn),
                nameof(order.ShippingCost),
                nameof(order.Discount),
                nameof(order.SubTotal),
                nameof(order.Total),
                nameof(order.PaidOrShippedOn),
                nameof(order.AdditionalEarnings),
            });

            await Save();
            return order;
        }

        public async Task<bool> CheckIfOrderNumberExists(string orderNumber)
        {
            var order = await Context.Order
                .AsNoTracking()
                .SingleOrDefaultAsync(w => w.OrderNumber == orderNumber)
                .ConfigureAwait(false);

            return order.HasValue();
        }

        public async Task<Order> Cancel(Order order)
        {
            Context.Attach(order);
            Context.Entry(order).State = EntityState.Deleted;

            await Save();
            return order;
        }
    }
}