using System.Collections.Generic;
using SiriusStyleRdStore.Entities.Enums;

namespace SiriusStyleRdStore.Entities.Requests.Order
{
    public class OrderRequest
    {
        public string OrderNumber { get; set; }

        public int CustomerId { get; set; }

        public OrderStatus Status { get; set; }

        public decimal? ShippingCost { get; set; }

        public decimal? Discount { get; set; }

        public decimal SubTotal { get; set; }

        public decimal Total { get; set; }

        public List<string> ProductCodes { get; set; }
    }
}