using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiriusStyleRd.Entities.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }

        [Required] 
        [StringLength(200)] 
        public string Description { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}