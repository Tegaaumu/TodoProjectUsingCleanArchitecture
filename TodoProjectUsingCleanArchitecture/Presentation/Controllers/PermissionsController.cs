using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TodoProjectUsingCleanArchitecture.Application.Repositories;
using TodoProjectUsingCleanArchitecture.Contract;
using TodoProjectUsingCleanArchitecture.Contract.Request;

namespace TodoProjectUsingCleanArchitecture.Presentation.Controllers
{
    [ApiController]
    // Require standard authentication to ensure a valid JWT is supplied.
    [Authorize]
    public class PermissionsController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public PermissionsController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // POST api/permissions/assign
        // Secures permission modifications to only users carrying the "CanAssign" permission claim/policy.
        [Authorize(Policy = ApiEndpoints.Policies.CanAssign)]
        [HttpPost(ApiEndpoints.Permissions.Assign)]
        public async Task<IActionResult> AssignPermissions([FromBody] UpdatePermissionsRequest request)
        {
            // Verify if the target user actually exists in the database
            var targetUser = await _userRepository.GetByIdAsync(request.UserId);
            if (targetUser == null)
            {
                return NotFound(new { message = $"User with ID '{request.UserId}' was not found." });
            }

            // Update permissions in the database using repository method
            var result = await _userRepository.UpdatePermissionsAsync(
                request.UserId,
                request.CanCreate,
                request.CanEdit,
                request.CanDelete,
                request.CanAssign
            );

            if (!result)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Failed to update user permissions in database." });
            }

            return Ok(new { message = "Permissions updated successfully.", userId = request.UserId });
        }
    }
}
