using System.ComponentModel;

namespace SiriusStyleRd.Entities.Enums
{
    public enum PaymentType
    {
        [Description("Transferencia")]
        WireTransfer,

        [Description("Contra entrega")]
        PaymentAgainstDelivery
    }
}