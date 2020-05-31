using SiriusStyleRdStore.Entities.ViewModels;

namespace SiriusStyleRdStore.Entities.Responses
{
    public class GetCustomerViewModel : IViewModel
    {
        public int CustomerId { get; set; }

        public string FullName { get; set; }

        public string City { get; set; }

        public string Sector { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Facebook { get; set; }

        public string Instagram { get; set; }
    }
}