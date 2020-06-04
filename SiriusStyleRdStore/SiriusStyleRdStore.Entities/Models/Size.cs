using System;
using System.Collections.Generic;

namespace SiriusStyleRdStore.Entities.Models
{
    public class Size
    {
        public int SizeId { get; set; }
        public string Description { get; set; }
        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}