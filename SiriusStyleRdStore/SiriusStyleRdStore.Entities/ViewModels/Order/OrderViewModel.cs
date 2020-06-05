using SiriusStyleRdStore.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using SiriusStyleRdStore.Entities.ViewModels.Product;

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
        public DateTime? PaidOn { get; set; }

        [DisplayName("Enviada")]
        public DateTime? ShippedOn { get; set; }

        [DisplayName("Cancelada")]
        public DateTime? CanceledOn { get; set; }

        [DisplayName("Estado")]
        public string Status { get; set; }

        [DisplayName("Costo de envío")]
        public string ShippingCost { get; set; }

        [DisplayName("Descuento")]
        public string Discount { get; set; }

        [DisplayName("SubTotal")]
        public string SubTotal { get; set; }

        [DisplayName("Total")]
        public string Total { get; set; }

        public List<ProductViewModel> Products { get; set; }
    }
}
