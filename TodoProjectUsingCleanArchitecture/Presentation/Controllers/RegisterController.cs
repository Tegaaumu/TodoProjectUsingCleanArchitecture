using Microsoft.AspNetCore.Mvc;
using TodoProjectUsingCleanArchitecture.Application.Services;
using TodoProjectUsingCleanArchitecture.Contract;
using TodoProjectUsingCleanArchitecture.Contract.Request;

namespace TodoProjectUsingCleanArchitecture.Presentation.Controllers;
[ApiController]
public class RegisterController : ControllerBase
{
    private readonly IIdentityService _identityService;

    public RegisterController(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    [HttpPost(ApiEndpoints.Register.RegisterUser)]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var response = await _identityService.RegisterAsync(request);
        if (response == null)
        {
            return BadRequest(new { Message = "User registration failed. Email might already be in use." });
        }

        return Ok(response);
    }
}