using Microsoft.AspNetCore.Mvc;
using TravelerAppService.Models;
using TravelerAppService.Services;
using TravelerAppWebService.Services;
using TravelerAppWebService.Services.Interfaces;

namespace TravelerAppWebService.Controllers
{
    [Route("api/bookings")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        public async Task<ActionResult<Booking>> PostBookings(Booking booking)
        {
            try
            {

                await _bookingService.CreateAsync(booking);

                Console.WriteLine($"Received user data: Id = {booking.Id}, Name = {booking.ContactNumber} etc.");

                // Return HTTP 201 Created status along with the newly created user
                return Ok(booking);
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return an error response
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(string id, Booking booking)
        {
            if (id != booking.Id)
            {
                return BadRequest();
            }

            await _bookingService.UpdateAsync(id, booking);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetAllBookings()
        {
            var bookings = await _bookingService.GetAllAsync();
            return Ok(bookings);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBookingById(string id)
        {
            var booking = await _bookingService.GetByIdAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            return Ok(booking);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(string id)
        {
            await _bookingService.DeleteAsync(id);
            return NoContent();
        }
    }
}
