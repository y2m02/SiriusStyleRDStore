using System.Collections.Generic;
using SiriusStyleRdStore.Entities.Enums;

namespace SiriusStyleRdStore.Entities.Requests.Product
{
    public class AssignProductToOrderRequest
    {
        public string ProductCode { get; set; }
        public string OrderNumber { get; set; }
        public ProductStatus Status { get; set; }
    }
}