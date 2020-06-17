using System.ComponentModel;

namespace SiriusStyleRd.Entities.Enums
{
    public enum OrderStatus
    {
        [Description("1. Pendiente")]
        Pending = 1,

        [Description("2. Paga")]
        Paid = 2,

        [Description("3. Enviada")]
        Shipped = 3
    }
}