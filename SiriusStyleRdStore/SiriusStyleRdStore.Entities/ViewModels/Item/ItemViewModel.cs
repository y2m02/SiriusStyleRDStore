using System.Collections.Generic;
using SiriusStyleRdStore.Entities.ViewModels.Category;
using SiriusStyleRdStore.Entities.ViewModels.Customer;
using SiriusStyleRdStore.Entities.ViewModels.Size;

namespace SiriusStyleRdStore.Entities.ViewModels.Item
{
    public class ItemViewModel
    {
        public IEnumerable<CategoryViewModel> Categories { get; set; }
        public IEnumerable<SizeViewModel> Sizes { get; set; }
        public IEnumerable<CustomerViewModel> Customers { get; set; }
    }
}