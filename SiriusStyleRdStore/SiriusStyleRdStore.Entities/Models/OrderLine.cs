using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiriusStyleRdStore.Entities.Models
{
    public class OrderLine
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderLineId { get; set; }

        [Required]
        public string OrderNumber { get; set; }

        [Required]
        public string ProductCode { get; set; }

        [ForeignKey(nameof(OrderNumber))]
        public virtual Order Order { get; set; }

        [ForeignKey(nameof(ProductCode))]
        public virtual Product Product { get; set; }
    }
}