using globalize_dates_in_server_side_generated_files.Models;
using IronPdf;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace globalize_dates_in_server_side_generated_files
{
    public class BalanceRenderer : IBalanceRenderer
    {
        private const string _format = "yyyy-MM-dd HH:mm:ss";
        private readonly ITimeZoneService _timeZoneService;

        public BalanceRenderer(ITimeZoneService timeZoneService)
        {
            _timeZoneService = timeZoneService;
        }

        public async Task<Stream> RenderBalancePdf(GetUserBalanceInputModel parameters, IReadOnlyCollection<BalanceItemModel> balanceItems)
        {
            HtmlToPdf renderer = new HtmlToPdf();
            StringBuilder htmlContent = new StringBuilder();

            htmlContent.Append($"<html><head><link rel=\"stylesheet\" href=\"https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css\" integrity=\"sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T\" crossorigin=\"anonymous\"><style>td {{text-align:center;}}</style></head><body>");

            htmlContent.Append($"<h1 align=\"center\">{parameters.UserFullName} transaction's report</h1>");
            var fromLocal = _timeZoneService.ToLocalDate(parameters.DateTimeFrom).ToString(_format);
            var toLocal = _timeZoneService.ToLocalDate(parameters.DateTimeTo).ToString(_format);

            htmlContent.Append($"<h2 align=\"center\">From: {fromLocal}</h2>");
            htmlContent.Append($"<h2 align=\"center\">To: {toLocal}</h2>");
            
            if (!balanceItems.Any())
            {
                return renderer.RenderHtmlAsPdf("<h2>No transaction data ...</h2>").Stream;
            }
            htmlContent.Append($"</br>");
            htmlContent.Append($"<table width=\"100%\" class=\"table table-striped\">" +
                                    $"<thead class=\"thead-dark\">" +
                                        $"<tr>" +
                                            $"<th scope=\"col\">No.</th>" +
                                            $"<th scope=\"col\">Transaction date</th>" +
                                            $"<th scope=\"col\">Amount of monay</th>" +
                                            $"<th scope=\"col\">Source/Target</th>" +
                                        $"</tr>" +
                                    $"</thead>" +
                                    $"<tbody>");
            int counter = 1;
            foreach (var balanceItem in balanceItems)
            {
                htmlContent.Append(
                            $"<tr>" +
                                $"<th scope=\"row\">{counter++}</th>" +
                                $"<td width=\"30%\">{_timeZoneService.ToLocalDate(balanceItem.ChangeDateTime).ToString(_format)}</td>" +
                                $"<td width=\"30%\">{balanceItem.ChangeValue.ToString("F")} $</td>" +
                                $"<td width=\"30%\">{balanceItem.ChangeSourceOrTarget}</td>" +
                            $"</tr>");
            }
            htmlContent.Append($"</tbody>" +
                $"<tfoot  class=\"table-info\">" +
                    $"<td>-</td>" +
                    $"<td>-</td>" +
                    $"<td>{balanceItems.Sum(x=>x.ChangeValue).ToString("F")}</td>" +
                    $"<td>-</td>" +
                $"</tfoot>" +
                $"</table></body></html>");

            htmlContent.Append("</br>");
            htmlContent.Append($"<strong>Microsoft timezone name: {_timeZoneService.RequestTimeZoneInfo.DisplayName}</strong>");
            htmlContent.Append("</br>");
            htmlContent.Append($"<strong>IANA timezone name: {_timeZoneService.IanaRequestTimeZoneName}</strong>");

            return (await renderer.RenderHtmlAsPdfAsync(htmlContent.ToString())).Stream;
        }
    }
}
