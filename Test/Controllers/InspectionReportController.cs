using BLL.DTO.Requests;
using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Test.Controllers
{
    [Route("api/reports")]
    [ApiController]
    public class InspectionReportController : Controller
    {
        private readonly IInspectionReportService _service;

        public InspectionReportController(IInspectionReportService service)
        {
            _service = service;
        }

        [Authorize(Policy = "AuthenticatedUsers")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllReports(CancellationToken cancellationToken = default)
        {
            var reports = await _service.GetAllInspectionReportAsync(cancellationToken);
            return Ok(reports);
        }



        [Authorize(Policy = "AuthenticatedUsers")]
        [HttpPost]
        public async Task<IActionResult> CreateReport([FromForm] CreateInspectionReportDTO createReportDto, CancellationToken cancellationToken = default)
        {
            var inspectorId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var report = await _service.CreateInspectionReportAsync(createReportDto, inspectorId, cancellationToken);
            return CreatedAtAction(nameof(GetReportById), new { id = report.Id }, report);
        }


        [Authorize(Policy = "AuthenticatedUsers")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReportById(string id, CancellationToken cancellationToken = default)
        {
            var report = await _service.GetInspectionReportByIdAsync(id, cancellationToken);
            return Ok(report);
        }
    }
}
