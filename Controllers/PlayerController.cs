using Microsoft.AspNetCore.Mvc;
using WebCoTuong_API_ASPCore_MongoDB.Models;
using WebCoTuong_API_ASPCore_MongoDB.Services;

namespace WebCoTuong_API_ASPCore_MongoDB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly PlayerService _playerService;

        public PlayerController(PlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> Get(string id)
        {
            var existingPlayer = await _playerService.GetAsync(id);

            if (existingPlayer is null)
                return NotFound();

            return Ok(existingPlayer);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var allPlayers = await _playerService.GetAsync();
            if (allPlayers.Any())
                return Ok(allPlayers);
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post(Player player)
        {
            await _playerService.CreateAsync(player);
            Console.WriteLine("Success");
            return CreatedAtAction(nameof(Get), new {id =  player.PlayerId}, player);
            
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Player player)
        {
            var existingPlayer = await _playerService.GetAsync(id);
            if (existingPlayer is null)
                return BadRequest();
    
            // Assuming 'PlayerId' is the actual property name in the Player class
            existingPlayer.PlayerId = player.PlayerId; // Update the 'PlayerId' property
    
            await _playerService.UpdateAsync(existingPlayer);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingPlayer = await _playerService.GetAsync(id);
    
            if (existingPlayer is null)
            {
                return BadRequest();
            }
    
            await _playerService.RemoveAsync(id); // Call your service to delete the player.
    
            return NotFound(); // Player deleted successfully, return a 204 response.
        }

    }
}