﻿using BLL.DTO.Requests;
using BLL.Interfaces;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Test.Controllers
{
    [Route("api/booking")]
    [ApiController]
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [Authorize(Policy = "AuthenticatedUsers")]
        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromForm] CreateBookingDTO createBookingDTO, CancellationToken cancellationToken = default)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("Пользователь не авторизован.");

            var booking = await _bookingService.CreateBookingAsync(createBookingDTO,userId, cancellationToken);
            return CreatedAtAction(nameof(GetBookingById), new { id = booking.Id }, booking);
        }


        [Authorize(Policy = "AuthenticatedUsers")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingById(string id, CancellationToken cancellationToken = default)
        {
            var booking = await _bookingService.GetBookingByIdAsync(id, cancellationToken);
            return Ok(booking);
        }


        [Authorize(Policy = "AuthenticatedUsers")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllBookings(CancellationToken cancellationToken = default)  
        {
            var bookings = await _bookingService.GetAllBookingAsync(cancellationToken);
            return Ok(bookings);
        }

        [Authorize(Policy = "AuthenticatedUsers")]
        [HttpPatch("{id}/confirm")]
        public async Task<IActionResult> ConfirmBooking(string id, CancellationToken cancellationToken = default)
        {
            await _bookingService.ConfirmBookingAsync(id, cancellationToken);
            return Ok("Статус подтверждён");
        }

        [Authorize(Policy = "AuthenticatedUsers")]
        [HttpGet("{id}/contract")]
        public async Task<IActionResult> GetRentalContract(string id, CancellationToken cancellationToken = default)
        {
            var pdfBytes = await _bookingService.GenerateRentalAgreementAsync(id, cancellationToken);
            return File(pdfBytes, "application/pdf", $"RentalAgreement_{id}.pdf");
        }
    }
}
