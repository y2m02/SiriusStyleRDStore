using Microsoft.AspNetCore.Http;
using System.ComponentModel;

namespace SiriusStyleRdStore.Entities.Requests.Product
{
    public class ProductRequest
    {
        public string ProductCode { get; set; }

        [DisplayName("Descripción")]
        public string Description { get; set; }

        [DisplayName("Size")]
        public int? SizeId { get; set; }

        [DisplayName("Comentarios")]
        public string Comments { get; set; }

        [DisplayName("Precio")]
        public decimal Price { get; set; }

        [DisplayName("Imagen")]
        public IFormFile Image { get; set; }

        [DisplayName("Categoría")]
        public int CategoryId { get; set; }

        [DisplayName("Paca")]
        public int BaleId { get; set; }
    }
}