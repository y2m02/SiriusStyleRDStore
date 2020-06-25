using System.ComponentModel;

namespace SiriusStyleRd.Entities.ViewModels.EarningReport
{
    public class EarningReportViewModel
    {
        public int BaleId { get; set; }

        [DisplayName("Paca")]
        public string Bale { get; set; }

        [DisplayName("Precio")]
        public decimal Price { get; set; }

        [DisplayName("Ganancias netas")]
        public decimal TotalEarned { get; set; }

        [DisplayName("Pendiente")]
        public decimal TotalPending { get; set; }

        [DisplayName("Ganancias brutas")]
        public decimal Total { get; set; }
    }
}