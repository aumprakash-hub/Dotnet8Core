using AutoMapper;
using DotnetCore.DataService.Repositories.Interfaces;
using DotnetCore.Entities.DbSet;
using DotnetCore.Entities.Dtos.Requests;
using DotnetCore.Entities.Dtos.Responses;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCore.Api.Controllers;

public class AchievementsController(IUnitOfWork unitOfWork, IMapper mapper) : BaseController(unitOfWork, mapper)
{
    [HttpGet]
    [Route("{driverId:guid}")]
    public async Task<IActionResult> GetDriverAchievements(Guid driverId)
    {
        var driverAchievements = await _unitOfWork.Achievements.GetDriverAchievementAsync(driverId: driverId);
        if (driverAchievements is null) return NotFound("Achievements not found");
        var result = _mapper.Map<DriverAchievementResponse>(driverAchievements);
        return Ok(result);
    }

    [HttpPost("")]
    public async Task<IActionResult> AddAchievement([FromBody] CreateDriverAchievementRequest request)
    {
        if (!ModelState.IsValid) return BadRequest();

        var result = _mapper.Map<Achievement>(request);
        await _unitOfWork.Achievements.Add(result);
        await _unitOfWork.CompleteAsync();

        return CreatedAtAction(nameof(GetDriverAchievements), new { driverId = request.DriverId }, request);
    }

    [HttpPut("")]
    public async Task<IActionResult> UpdateAchievements([FromBody] UpdateDriverAchievementRequest request)
    {
        if (!ModelState.IsValid) return BadRequest();
        var result = _mapper.Map<Achievement>(request);
        await _unitOfWork.Achievements.Update(result);
        await _unitOfWork.CompleteAsync();
        return NoContent();
    }
}