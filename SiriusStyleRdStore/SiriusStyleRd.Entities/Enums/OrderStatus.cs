using System.ComponentModel;

namespace SiriusStyleRd.Entities.Enums
{
    public enum OrderStatus
    {
        [Description("Pendiente")]
        Pending = 1,

        [Description("Paga")]
        Paid = 2,

        [Description("Enviada")]
        Shipped = 3
    }
}