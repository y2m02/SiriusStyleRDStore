using SiriusStyleRdStore.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using SiriusStyleRdStore.Entities.ViewModels.Product;
using SiriusStyleRdStore.Utility.Extensions;

namespace SiriusStyleRdStore.Entities.ViewModels.Order
{
    public class OrderViewModel : IViewModel
    {
        public string OrderNumber { get; set; }

        public int CustomerId { get; set; }

        [DisplayName("Cliente")]
        public string Customer { get; set; }

        [DisplayName("Fecha")]
        public DateTime CreatedOn { get; set; }

        [DisplayName("Pagada")]
        public string PaidOn { get; set; }

        [DisplayName("Enviada")]
        public string ShippedOn { get; set; }

        [DisplayName("Estado")]
        public string Status { get; set; }

        [DisplayName("Costo de envío")]
        public string ShippingCost { get; set; }

        [DisplayName("Descuento")]
        public string Discount { get; set; }

        [DisplayName("SubTotal")]
        public decimal SubTotal { get; set; }

        [DisplayName("Total")]
        public decimal Total { get; set; }

        public decimal TotalPaidAndShipped { get; set; }
        public decimal GrantTotal { get; set; }

        public List<ProductViewModel> Products { get; set; }
    }
}
