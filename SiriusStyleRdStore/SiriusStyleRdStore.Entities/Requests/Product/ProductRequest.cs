using System.ComponentModel;
using Microsoft.AspNetCore.Http;
using SiriusStyleRdStore.Entities.Enums;

namespace SiriusStyleRdStore.Entities.Requests.Product
{
    public class ProductRequest
    {
        public string ProductCode { get; set; }

        [DisplayName("Descripción")]
        public string Description { get; set; }

        [DisplayName("Size")]
        public ProductSize? Size { get; set; }

        [DisplayName("Comentarios")]
        public string Comments { get; set; }

        [DisplayName("Precio")]
        public decimal Price { get; set; }

        [DisplayName("Imagen")]
        public IFormFile Image { get; set; }

        [DisplayName("Categoría")]
        public int CategoryId { get; set; }
    }
}