using BLL.Interfaces;
using DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Test.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class ForAdminController : Controller
    {
        private readonly IInspectionReportService _incpectionReportService;
        public ForAdminController(IInspectionReportService incpectionReportService)
        {
            _incpectionReportService = incpectionReportService;
        }

        [HttpGet("revenue")]
        public async Task<IActionResult> Money(CancellationToken cancellationToken)
        {
            var total = await _incpectionReportService.CalculateTotalRevenueAsync(cancellationToken);
            return Ok($" Заработанная сумма: {total} рублей");
        }
    }
}
