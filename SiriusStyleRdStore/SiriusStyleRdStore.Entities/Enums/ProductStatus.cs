using System.ComponentModel;

namespace SiriusStyleRdStore.Entities.Enums
{
    public enum ProductStatus
    {
        [Description("Disponible")]
        Available = 1,
        [Description("Ordenado")]
        Ordered = 2,
        [Description("Vendido")]
        Sold = 3
    }
}