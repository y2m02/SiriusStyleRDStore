using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SiriusStyleRdStore.Utility.Validations;

namespace SiriusStyleRdStore.Entities.ViewModels.Customer
{
    public class CustomerViewModel : IViewModel
    {
        public int CustomerId { get; set; }

        [DisplayName("Nombre")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string FullName { get; set; }

        [DisplayName("Ciudad")]
        [StringLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string City { get; set; }

        [DisplayName("Sector")]
        [StringLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string Sector { get; set; }

        [DisplayName("Dirección")]
        [StringLength(1000, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string Address { get; set; }

        [DisplayName("Teléfono")]
        [CustomValidator(ValidationType.PhoneNumber, ErrorMessage = "El campo {0} es inválido")]
        public string PhoneNumber { get; set; }

        [DisplayName("Facebook")]
        [StringLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string Facebook { get; set; }

        [DisplayName("Instagram")]
        [StringLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string Instagram { get; set; }
    }
}