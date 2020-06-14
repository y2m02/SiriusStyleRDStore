using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SiriusStyleRdStore.Entities.Enums;
using SiriusStyleRdStore.Entities.ViewModels;
using SiriusStyleRdStore.Entities.ViewModels.EarningReport;
using SiriusStyleRdStore.Repositories.Repositories;

namespace SiriusStyleRdStore.BL.Services
{
    public interface IEarningReportService
    {
        Task<IViewModel> GetReport();
    }

    public class EarningReportService : BaseService, IEarningReportService
    {
        private readonly IBaleRepository _baleRepository;

        public EarningReportService(IBaleRepository baleRepository)
        {
            _baleRepository = baleRepository;
        }

        public async Task<IViewModel> GetReport()
        {
            return await HandleErrors(Get);

            async Task<IViewModel> Get()
            {
                var bales = await _baleRepository.GetAllWithProducts().ConfigureAwait(false);

                var reports = new List<EarningReportViewModel>();
                foreach (var bale in bales)
                {
                    decimal total = 0;
                    foreach (var product in bale.Products
                        .Where(
                            w => w.OrderNumber != null
                                 && (w.Order.Status == OrderStatus.Paid
                                     || w.Order.Status == OrderStatus.Shipped)
                        ))
                    {
                        total += product.Price - (product.Order.Discount / product.Order.Products.Count);
                    }

                    var report = new EarningReportViewModel
                    {
                        BaleId = bale.BaleId,
                        Bale = $"{bale.BaleId} - {bale.Description}",
                        Price = bale.Price,
                        TotalEarned = total
                    };

                    report.TotalPending = report.Price - report.TotalEarned;

                    reports.Add(report);
                }

                return Success(reports);
            }
        }
    }
}