using System.ComponentModel;

namespace SiriusStyleRdStore.Entities.Enums
{
    public enum OrderStatus
    {
        [Description("Cancelada")]
        Canceled = 0,

        [Description("Pendiente")]
        Pending = 1,

        [Description("Paga")]
        Paid = 2,

        [Description("Enviada")]
        Shipped = 3
    }
}