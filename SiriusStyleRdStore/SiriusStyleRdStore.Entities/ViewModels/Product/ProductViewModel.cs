using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SiriusStyleRdStore.Entities.ViewModels.Product
{
    public class ProductViewModel : IViewModel
    {
        public string ProductCode { get; set; }

        [DisplayName("Orden #")]
        public string OrderNumber { get; set; }

        [DisplayName("Descripción")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Description { get; set; }

        public int SizeId { get; set; }

        [DisplayName("Size")]
        public string Size { get; set; }

        [DisplayName("Comentarios")]
        public string Comments { get; set; }

        [DisplayName("Estado")]
        public string Status { get; set; }

        [DisplayName("Precio")]
        public decimal Price { get; set; }

        [DisplayName("Imagen")] 
        public string Image { get; set; }

        public int CategoryId { get; set; }

        [DisplayName("Categoría")]
        public string Category { get; set; }
    }
}