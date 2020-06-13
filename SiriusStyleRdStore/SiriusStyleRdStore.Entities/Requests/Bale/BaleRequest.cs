namespace SiriusStyleRdStore.Entities.Requests.Bale
{
    public class BaleRequest
    {
        public string Description { get; set; }

        public decimal Price { get; set; }

        public string BoughtTo { get; set; }
    }
}