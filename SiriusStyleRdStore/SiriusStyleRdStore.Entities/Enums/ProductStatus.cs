using System.ComponentModel;

namespace SiriusStyleRdStore.Entities.Enums
{
    public enum ProductStatus
    {
        [Description("Activo")]
        Active = 1,
        [Description("Ordenado")]
        Ordered = 2,
        [Description("Vendido")]
        Sold = 3
    }
}