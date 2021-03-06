using System;
using System.ComponentModel;

namespace SiriusStyleRd.Entities.ViewModels.Order
{
    public class OrderViewModel : IViewModel
    {
        public string OrderNumber { get; set; }

        public int CustomerId { get; set; }

        [DisplayName("Cliente")]
        public string Customer { get; set; }

        [DisplayName("Fecha")]
        public DateTime CreatedOn { get; set; }

        //[DisplayName("Pagada")]
        //public string PaidOn { get; set; }

        //[DisplayName("Enviada")]
        //public string ShippedOn { get; set; }

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

        [DisplayName("Pagada/Enviada")]
        public string PaidOrShippedOn { get; set; }

        [DisplayName("Ganancia adicional")]
        public string AdditionalEarnings { get; set; }

        [DisplayName("Forma de pago")]
        public string PaymentType { get; set; }
    }
}
