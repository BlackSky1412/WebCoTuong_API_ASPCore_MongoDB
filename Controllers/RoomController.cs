using Microsoft.AspNetCore.Mvc;
using WebCoTuong_API_ASPCore_MongoDB.Models;
using WebCoTuong_API_ASPCore_MongoDB.Services;

namespace WebCoTuong_API_ASPCore_MongoDB.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomController : ControllerBase
{
    private readonly RoomService _roomService;
    private readonly ILogger<RoomController> _logger;

    public RoomController(RoomService roomService, ILogger<RoomController> logger)
    {
        _roomService = roomService;
        _logger = logger;
    }
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var allRooms = await _roomService.GetAsync();
        if (allRooms.Any())
        {
            return Ok(allRooms);
        }

        _logger.LogInformation("No rooms found.");
        return NotFound();
    }
    [HttpGet("{id:length(24)}")]
    public async Task<IActionResult> Get(string id)
    {
        var existingRoom = await _roomService.GetAsync(id);

        if (existingRoom == null)
        {
            _logger.LogInformation($"Room with ID '{id}' not found.");
            return NotFound();
        }

        return Ok(existingRoom);
    }
    [HttpPost]
    public async Task<IActionResult> Post(Room room)
    {
        try
        {
            await _roomService.CreateAsync(room);
            _logger.LogInformation($"Room '{room.RoomId}' created successfully.");
            return CreatedAtAction(nameof(Get), new { id = room.RoomId }, room);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error creating room '{room.RoomId}'.");
            return StatusCode(500, "Internal server error.");
        }
    }
    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Room room)
    {
        var existingRoom = await _roomService.GetAsync(id);
        if (existingRoom == null)
        {
            _logger.LogInformation($"Room with ID '{id}' not found.");
            return BadRequest("Room not found.");
        }
        existingRoom.RoomId = room.RoomId;
        try
        {
            await _roomService.UpdateAsync(existingRoom);
            _logger.LogInformation($"Room '{id}' updated successfully.");
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error updating room '{id}'.");
            return StatusCode(500, "Internal server error.");
        }
    }
    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var existingRoom = await _roomService.GetAsync(id);

        if (existingRoom == null)
        {
            _logger.LogInformation($"Room with ID '{id}' not found.");
            return BadRequest("Room not found.");
        }
        try
        {
            await _roomService.RemoveAsync(id);
            _logger.LogInformation($"Room '{id}' deleted successfully.");
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting room '{id}'.");
            return StatusCode(500, "Internal server error.");
        }
    }
}
