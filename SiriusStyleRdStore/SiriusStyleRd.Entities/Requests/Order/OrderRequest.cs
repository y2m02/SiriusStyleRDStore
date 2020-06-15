using System.Collections.Generic;
using System.ComponentModel;
using SiriusStyleRd.Entities.Enums;

namespace SiriusStyleRd.Entities.Requests.Order
{
    public class OrderRequest
    {
        public string OrderNumber { get; set; }

        [DisplayName("Cliente")]
        public int CustomerId { get; set; }

        [DisplayName("Estado")]
        public OrderStatus Status { get; set; }

        [DisplayName("Costo de envío")]
        public decimal? ShippingCost { get; set; }

        [DisplayName("Descuento")]
        public decimal? Discount { get; set; }

        [DisplayName("Subtotal")]
        public decimal SubTotal { get; set; }

        [DisplayName("Total")]
        public decimal Total { get; set; }

        public List<string> ProductCodes { get; set; }
    }
}