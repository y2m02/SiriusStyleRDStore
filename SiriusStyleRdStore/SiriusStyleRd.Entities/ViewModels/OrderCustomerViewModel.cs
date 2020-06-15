using SiriusStyleRd.Entities.Requests.Customer;
using SiriusStyleRd.Entities.Requests.Order;

namespace SiriusStyleRd.Entities.ViewModels
{
    public class OrderCustomerViewModel
    {
        public CustomerRequest Customer { get; set; }
        public OrderRequest Order { get; set; }
    }
}