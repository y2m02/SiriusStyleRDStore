using SiriusStyleRdStore.Entities.Requests.Customer;
using SiriusStyleRdStore.Entities.Requests.Order;

namespace SiriusStyleRdStore.Entities.ViewModels
{
    public class OrderCustomerViewModel
    {
        public CustomerRequest Customer { get; set; }
        public OrderRequest Order { get; set; }
    }
}