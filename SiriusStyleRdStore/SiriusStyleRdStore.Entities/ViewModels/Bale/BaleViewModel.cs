using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SiriusStyleRdStore.Entities.Enums;

namespace SiriusStyleRdStore.Entities.ViewModels.Bale
{
    public class BaleViewModel
    {
        [DisplayName("#")]
        public int BaleId { get; set; }

        [DisplayName("Descripción")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(300, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string Description { get; set; }

        public string IdAndDescription { get; set; }

        [DisplayName("Precio")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.Currency)]
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }

        [DisplayName("Comprada a")]
        [StringLength(200, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres")]
        public string BoughtTo { get; set; }

        //[DisplayName("Subida completa")]
        //public CompleteUploaded CompleteUploaded { get; set; }
    }

}