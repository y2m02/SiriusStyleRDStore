using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SiriusStyleRd.Entities.Enums;

namespace SiriusStyleRd.Entities.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ProductCode { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        [StringLength(1000)]
        public string Comments { get; set; }

        [Required]
        public ProductStatus Status { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string Image { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public int? SizeId { get; set; }

        public string OrderNumber { get; set; }

        public int? BaleId { get; set; }

        [ForeignKey(nameof(OrderNumber))]
        public virtual Order Order { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; }

        [ForeignKey(nameof(SizeId))]
        public virtual Size Size { get; set; }

        [ForeignKey(nameof(BaleId))]
        public virtual Bale Bale { get; set; }
    }
}