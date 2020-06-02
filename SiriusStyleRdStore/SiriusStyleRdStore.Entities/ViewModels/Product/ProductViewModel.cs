using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SiriusStyleRdStore.Entities.Enums;

namespace SiriusStyleRdStore.Entities.ViewModels.Product
{
    public class ProductViewModel : IViewModel
    {
        public string ProductCode { get; set; }

        [DisplayName("Descripción")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Description { get; set; }

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
    }
}