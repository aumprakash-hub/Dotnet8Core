using AutoMapper;
using DotnetCore.DataService.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCore.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseController(IUnitOfWork unitOfWork, IMapper mapper) : ControllerBase
{
    protected readonly IUnitOfWork _unitOfWork = unitOfWork;
    protected readonly IMapper _mapper = mapper;
}