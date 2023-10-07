using Microsoft.AspNetCore.Mvc;
using TravelerAppService.Models;
using TravelerAppService.Services;
using TravelerAppWebService.Services.Interfaces;

namespace TravelerAppWebService.Controllers
{
    [Route("api/trains")]
    [ApiController]
    public class TrainController : ControllerBase
    {
        private readonly ITrainService _trainService;

        public TrainController(ITrainService trainService)
        {
            _trainService = trainService;
        }

        [HttpPost]
        public async Task<ActionResult<Train>> PostTrains(Train train)
        {
            try
            { 

                // Call the TrainService to create a new train details asynchronously
                await _trainService.CreateAsync(train);

                Console.WriteLine($"Received user data: Id = {train.Id}, Name = {train.TrainName} etc.");

                // Return HTTP 201 Created status along with the newly created user
                return Ok(train);
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return an error response
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Train>>> GetAllTrains()
        {
            var trains = await _trainService.GetAllAsync();
            return Ok(trains);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Train>> GetTrainById(string id)
        {
            var train = await _trainService.GetByIdAsync(id);
            if (train == null)
            {
                return NotFound();
            }
            return Ok(train);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTrain(string id, Train train)
        {
            if (id != train.Id)
            {
                return BadRequest();
            }

            await _trainService.UpdateAsync(id, train);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrain(string id)
        {
            await _trainService.DeleteAsync(id);
            return NoContent();
        }
    }
}
