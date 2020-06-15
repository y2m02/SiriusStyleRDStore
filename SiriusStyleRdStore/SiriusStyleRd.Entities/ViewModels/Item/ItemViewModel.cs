using System.Collections.Generic;
using SiriusStyleRd.Entities.ViewModels.Bale;
using SiriusStyleRd.Entities.ViewModels.Category;
using SiriusStyleRd.Entities.ViewModels.Customer;
using SiriusStyleRd.Entities.ViewModels.Size;

namespace SiriusStyleRd.Entities.ViewModels.Item
{
    public class ItemViewModel
    {
        public IEnumerable<CategoryViewModel> Categories { get; set; }
        public IEnumerable<SizeViewModel> Sizes { get; set; }
        public IEnumerable<CustomerViewModel> Customers { get; set; }
        public IEnumerable<BaleViewModel> Bales { get; set; }
    }
}