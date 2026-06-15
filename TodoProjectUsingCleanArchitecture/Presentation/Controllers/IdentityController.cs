using Microsoft.AspNetCore.Mvc;
using TodoProjectUsingCleanArchitecture.Application.Services;
using TodoProjectUsingCleanArchitecture.Contract;
using TodoProjectUsingCleanArchitecture.Contract.Request;

namespace TodoProjectUsingCleanArchitecture.Presentation.Controllers;
[ApiController]
public class IdentityController : ControllerBase
{
    private readonly IIdentityService _identityService;

    public IdentityController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    [HttpPost(ApiEndpoints.Identity.Login)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var response = await _identityService.LoginAsync(request);
        if (response == null)
        {
            return Unauthorized(new { Message = "Invalid email or password." });
        }

        return Ok(response);
    }
}

