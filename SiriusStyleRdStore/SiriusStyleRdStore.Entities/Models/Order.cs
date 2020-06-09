using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SiriusStyleRdStore.Entities.Enums;

namespace SiriusStyleRdStore.Entities.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        //[StringLength(10)]
        public string OrderNumber { get; set; }
        
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public DateTime? PaidOn { get; set; }

        public DateTime? ShippedOn { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        [Required]
        public decimal ShippingCost { get; set; }

        [Required]
        public decimal Discount { get; set; }

        [Required]
        public decimal SubTotal { get; set; }

        [Required]
        public decimal Total { get; set; }

        [ForeignKey(nameof(CustomerId))] 
        public virtual Customer Customer { get; set; }

        //public virtual ICollection<OrderLine> OrderLines { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}