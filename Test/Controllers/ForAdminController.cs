using BLL.Interfaces;
using DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Policy = "OnlyAdminUsers")]
        [HttpGet("revenue")]
        public async Task<IActionResult> Money(CancellationToken cancellationToken)
        {
            var total = await _incpectionReportService.CalculateTotalRevenueAsync(cancellationToken);
            return Ok($" Заработанная сумма: {total} рублей");
        }

        [Authorize(Policy = "OnlyAdminUsers")]
        [HttpGet("rating")]
        public async Task<IActionResult> Rating(CancellationToken cancellationToken)
        {
            var ratings = await _incpectionReportService.CalculateAverageRatingPerCarAsync(cancellationToken);
            return Ok(ratings);
        }

    }
}
