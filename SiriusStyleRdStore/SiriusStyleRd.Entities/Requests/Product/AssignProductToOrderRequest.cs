using SiriusStyleRd.Entities.Enums;

namespace SiriusStyleRd.Entities.Requests.Product
{
    public class AssignProductToOrderRequest
    {
        public string ProductCode { get; set; }
        public string OrderNumber { get; set; }
        public ProductStatus Status { get; set; }
    }
}