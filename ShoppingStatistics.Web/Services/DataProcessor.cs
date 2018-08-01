using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using ShoppingStatistics.Core.Models.Domain;
using ShoppingStatistics.Core.Services;
using FileReaderComponent;
using ShoppingStatistics.Core.Extensions;

namespace ShoppingStatistics.Web.Services
{
    public class DataProcessor
    {
        private readonly DataAnalyzer _dataAnalyzer;

        public DataProcessor(DataAnalyzer dataAnalyzer)
        {
            _dataAnalyzer = dataAnalyzer;
        }

        public async Task ProcessFileAsync(Action stateHasChanged, IFileReference file)
        {
            var fileBytes = new List<byte>();

            using (var fs = await file.OpenReadAsync())
            {
                const int bufferSize = 4096;
                var buffer = new byte[bufferSize];
                int count;
                while ((count = await fs.ReadAsync(buffer, 0, buffer.Length)) != 0)
                {
                    fileBytes.AddRange(buffer.Take(count));
                    stateHasChanged();
                }
                stateHasChanged();
            }

            var reportsSerialized = Encoding.UTF8.GetString(fileBytes.ToArray());
            var reports = Json.Deserialize<List<Report>>(reportsSerialized);
            await CreateCharts(reports);
        }

        private async Task CreateCharts(List<Report> reports)
        {
            const int maxItems = 30;

            var sumByCompany = _dataAnalyzer.GetStatistics(reports, r => r.document.receipt.user, r => r.document.receipt.totalSum);
            var itemCountByCompany = _dataAnalyzer.GetStatistics(reports, r => r.document.receipt.user, r => r.document.receipt.items.Count);

            var sumByItem = _dataAnalyzer.GetStatistics(reports.SelectMany(r => r.document.receipt.items), i => i.name, i => i.sum);
            var priceByItem = _dataAnalyzer.GetStatistics(reports.SelectMany(r => r.document.receipt.items), i => i.name, i => i.price);
            var quantityByItem = _dataAnalyzer.GetStatistics(reports.SelectMany(r => r.document.receipt.items), i => i.name, i => i.quantity);

            var countByKeyword = _dataAnalyzer.GetStatistics(
                reports.SelectMany(report =>
                    report.document.receipt.items.SelectMany(item =>
                        item.name
                            .RemoveCharacters(new[]{ '/', '\\', ',', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '(', ')', '[', ']', '"', '«', '»' })
                            .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                            .Where(kw => kw.Length > 2))), keyword => keyword, keyword => 1);


            var receiptCountByCompany = sumByCompany.OrderByDescending(item => item.Count).Select(item => new { name = item.Key, y = item.Count }).ToList();
            var totalSumByCompany = sumByCompany.OrderByDescending(item => item.Sum).Select(item => new { name = item.Key, y = item.Sum / 100 }).ToList();
            var medianSumByCompany = sumByCompany.OrderByDescending(item => item.Median).Take(maxItems).Select(item => new { name = item.Key, y = item.Median / 100 }).ToList();

            var totalItemCountByCompany = itemCountByCompany.OrderByDescending(item => item.Sum).Take(maxItems).Select(item => new { name = item.Key, y = item.Sum }).ToList();
            var medianItemCountByCompany = itemCountByCompany.OrderByDescending(item => item.Median).Take(maxItems).Select(item => new { name = item.Key, y = item.Median }).ToList();

            var totalSumByItem = sumByItem.OrderByDescending(item => item.Sum).Take(maxItems).Select(item => new { name = item.Key, y = item.Sum / 100 }).ToList();

            // var medianPriceByItem = priceByItem.OrderByDescending(item => item.Median).Select(item => new { name = item.Key, y = item.Median / 100 }).ToList(); // todo: add the text report

            var totalQuantityByItem = quantityByItem.OrderByDescending(item => item.Sum).Take(maxItems).Select(item => new { name = item.Key, y = item.Sum }).ToList();
            var medianQuantityByItem = quantityByItem.OrderByDescending(item => item.Median).Take(maxItems).Select(item => new { name = item.Key, y = item.Median }).ToList();

            var totalCountByKeyword = countByKeyword.OrderByDescending(item => item.Sum).Take(maxItems).Select(item => new { name = item.Key, y = item.Sum }).ToList();

            var summary = new
            {
                receiptCountByCompany,
                totalSumByCompany,
                medianSumByCompany,
                totalItemCountByCompany,
                medianItemCountByCompany,
                totalSumByItem,
                //medianPriceByItem,
                totalQuantityByItem,
                medianQuantityByItem,
                totalCountByKeyword,
            };


            await JSRuntime.Current.InvokeAsync<object>("removeAllCharts");

            await JSRuntime.Current.InvokeAsync<object>("renderAsPieChart", "chartReceiptCountByCompany", "Receipt count per company", summary.receiptCountByCompany.ToArray());
            await JSRuntime.Current.InvokeAsync<object>("renderAsPieChart", "chartTotalSumByCompany", "Total spend per company", summary.totalSumByCompany.ToArray());
            await JSRuntime.Current.InvokeAsync<object>("renderAsColumnChart", "chartMedianSumByCompany", $"Median spend per company (first {maxItems})", summary.medianSumByCompany.ToArray());
            await JSRuntime.Current.InvokeAsync<object>("renderAsColumnChart", "chartTotalItemCountByCompany", $"Total item count (first {maxItems})", summary.totalItemCountByCompany.ToArray());
            await JSRuntime.Current.InvokeAsync<object>("renderAsColumnChart", "chartMedianItemCountByCompany", $"Median item count (first {maxItems})", summary.medianItemCountByCompany.ToArray());
            await JSRuntime.Current.InvokeAsync<object>("renderAsColumnChart", "chartTotalSumByItem", $"Total spend per item (first {maxItems})", summary.totalSumByItem.ToArray());
            await JSRuntime.Current.InvokeAsync<object>("renderAsColumnChart", "chartTotalQuantityByItem", $"Total quantity of items (first {maxItems})", summary.totalQuantityByItem.ToArray());
            await JSRuntime.Current.InvokeAsync<object>("renderAsColumnChart", "chartMedianQuantityByItem", $"Median quantity of items in receipts (first {maxItems})", summary.medianQuantityByItem.ToArray());
            await JSRuntime.Current.InvokeAsync<object>("renderAsColumnChart", "chartTotalCountByKeyword", $"Keyword frequency in receipts (first {maxItems})", summary.totalCountByKeyword.ToArray());
        }
    }
}
