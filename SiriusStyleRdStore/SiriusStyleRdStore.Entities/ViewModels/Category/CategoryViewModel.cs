using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SiriusStyleRdStore.Entities.ViewModels.Category
{
    public class CategoryViewModel : IViewModel
    {
        public int CategoryId { get; set; }

        [DisplayName("Descripción")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(200, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string Description { get; set; }
    }
}