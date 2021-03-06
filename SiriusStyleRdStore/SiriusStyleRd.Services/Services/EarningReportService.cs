using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SiriusStyleRd.Entities.Enums;
using SiriusStyleRd.Entities.ViewModels;
using SiriusStyleRd.Entities.ViewModels.EarningReport;
using SiriusStyleRd.Repository.Repositories;

namespace SiriusStyleRd.Services.Services
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
                    var total = bale.Products
                        .Where(
                            w => w.OrderNumber != null
                                 && (w.Order.Status == OrderStatus.Paid
                                     || w.Order.Status == OrderStatus.Shipped)
                        )
                        .Sum(product => product.Price
                                        + (product.Order.AdditionalEarnings.GetValueOrDefault() / product.Order.Products.Count)
                                        - (product.Order.Discount / product.Order.Products.Count));

                    var report = new EarningReportViewModel
                    {
                        BaleId = bale.BaleId,
                        Bale = $"{bale.BaleId} - {bale.Description}",
                        Price = bale.Price,
                        Total = total
                    };

                    if (report.Total >= report.Price)
                    {
                        report.TotalPending = 0;
                        report.TotalEarned = report.Total - report.Price;
                    }
                    else
                    {
                        report.TotalPending = report.Price - report.Total;
                        report.TotalEarned = 0;
                    }

                    reports.Add(report);
                }

                return Success(reports);
            }
        }
    }
}