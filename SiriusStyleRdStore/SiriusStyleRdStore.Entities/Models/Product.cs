﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SiriusStyleRdStore.Entities.Enums;

namespace SiriusStyleRdStore.Entities.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ProductCode { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        [StringLength(5)]
        public string Size { get; set; }

        [StringLength(1000)]
        public string Comments { get; set; }

        [Required]
        public ProductStatus Status { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string Image { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; }

        public virtual ICollection<OrderLine> OrderLines { get; set; }
    }
}