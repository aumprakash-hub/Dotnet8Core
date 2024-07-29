using AutoMapper;
using DotnetCore.DataService.Repositories.Interfaces;
using DotnetCore.Entities.DbSet;
using DotnetCore.Entities.Dtos.Requests;
using DotnetCore.Entities.Dtos.Responses;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCore.Api.Controllers;

public class DriversController(IUnitOfWork unitOfWork, IMapper mapper) : BaseController(unitOfWork, mapper)
{
    [HttpGet]
    [Route("{driverId:guid}")]
    public async Task<IActionResult> GetDriver(Guid driverId)
    {
        var driver = await _unitOfWork.Drivers.GetById(driverId);
        if (driver is null) return NotFound("Achievements not found");
        var result = _mapper.Map<GetDriverResponse>(driver);
        return Ok(result);
    }

    [HttpPost("")]
    public async Task<IActionResult> AddDriver([FromBody] CreateDriverRequest driver)
    {
        if (!ModelState.IsValid) return BadRequest();

        var result = _mapper.Map<Driver>(driver);
        await _unitOfWork.Drivers.Add(result);
        await _unitOfWork.CompleteAsync();

        return CreatedAtAction(nameof(GetDriver), new { driverId = result.Id }, result);
    }

    [HttpPut("")]
    public async Task<IActionResult> UpdateDriver([FromBody] UpdateDriverRequest driverRequest)
    {
        if (!ModelState.IsValid) return BadRequest();
        var result = _mapper.Map<Driver>(driverRequest);
        await _unitOfWork.Drivers.Update(result);
        await _unitOfWork.CompleteAsync();
        return NoContent();
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllDrivers()
    {
        var driver = await _unitOfWork.Drivers.GetAll();
        var result = _mapper.Map<IEnumerator<GetDriverResponse>>(driver);
        return Ok(result);
    }
    
    [HttpDelete("{driverId:guid}")]
    public async Task<IActionResult> DeleteDriver(Guid driverId)
    {
        var driver = await _unitOfWork.Drivers.GetById(driverId);
        if (driver is null) return NoContent();
        await _unitOfWork.Drivers.Delete(driverId);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }
}