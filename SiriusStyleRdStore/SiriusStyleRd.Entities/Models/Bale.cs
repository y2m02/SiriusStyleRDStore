using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiriusStyleRd.Entities.Models
{
    public class Bale
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BaleId { get; set; }

        [Required]
        [StringLength(300)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [StringLength(200)]
        public string BoughtTo { get; set; }

        [Required]
        public bool CompleteUploaded { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}