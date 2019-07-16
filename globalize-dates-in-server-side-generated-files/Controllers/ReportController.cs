using Microsoft.AspNetCore.Mvc;
using globalize_dates_in_server_side_generated_files.Models;
using System;
using System.Threading.Tasks;
using System.Net.Mime;

namespace globalize_dates_in_server_side_generated_files.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IBalanceRenderer _balanceRenderer;
        private readonly IUserAccountReader _userAccountReader;

        public ReportController(
            IBalanceRenderer balanceRenderer,
            IUserAccountReader userAccountReader)
        {
            _balanceRenderer = balanceRenderer ?? throw new ArgumentNullException(nameof(balanceRenderer));
            _userAccountReader = userAccountReader ?? throw new ArgumentNullException(nameof(userAccountReader));
        }

        [Route("balance")]
        [HttpGet]
        public async Task<IActionResult> GetBalance([FromQuery]string userFullName, [FromQuery] DateTime dateTimeFrom, [FromQuery]DateTime dateTimeTo)
        {
            var parameters = new GetUserBalanceInputModel
            {
                UserFullName = userFullName,
                DateTimeFrom = dateTimeFrom,
                DateTimeTo = dateTimeTo,
            };
            var balanceItems = await _userAccountReader.GetUserAccountBalance(parameters);
            return File(await _balanceRenderer.RenderBalancePdf(parameters, balanceItems), MediaTypeNames.Application.Pdf, fileDownloadName: $"balanceReport_{Guid.NewGuid()}.pdf");
        }
    }
}
