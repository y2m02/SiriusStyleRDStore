using System.ComponentModel;

namespace SiriusStyleRd.Entities.Enums
{
    public enum ProductStatus
    {
        [Description("1. Disponible")]
        Available = 1,
        [Description("2. Ordenado")]
        Ordered = 2,
        [Description("3. Vendido")]
        Sold = 3
    }
}