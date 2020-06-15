using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SiriusStyleRd.Utility.Validations;

namespace SiriusStyleRd.Entities.Requests.Customer
{
    public class CustomerRequest
    {
        public int CustomerId { get; set; }

        [DisplayName("Nombre")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string FullName { get; set; }

        [StringLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string City { get; set; }

        [StringLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string Sector { get; set; }

        [StringLength(1000, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string Address { get; set; }

        [CustomValidator(ValidationType.PhoneNumber, ErrorMessage = "El campo {0} es inválido")]
        public string PhoneNumber { get; set; }

        [StringLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string Facebook { get; set; }

        [StringLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string Instagram { get; set; }
    }
}