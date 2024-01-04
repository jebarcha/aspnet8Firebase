using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Netfirebase.Api.Dtos.Login;
using Netfirebase.Api.Dtos.UserRegister;
using Netfirebase.Api.Models.Domain;
using Netfirebase.Api.Pagination;
using Netfirebase.Api.Services.Authentication;
using Netfirebase.Api.Vms;

namespace Netfirebase.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public UserController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> Register([FromBody] UserRegisterRequestDto request)
        {
            return await _authenticationService.RegisterAsync(request);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginRequestDto request)
        {
            return await _authenticationService.LoginAsync(request);
        }

        [AllowAnonymous]
        [HttpGet("paginationv1")]
        public async Task<ActionResult<PagedResults<User>>> GetPaginationV1(
            [FromQuery] PaginationParams paginationQuery)
        {
            var results = await _authenticationService.GetPaginationVersion1(paginationQuery);
            return Ok(results);
        }

        [AllowAnonymous]
        [HttpGet("paginationv2")]
        public async Task<ActionResult<PagedResults<UserVm>>> GetPaginationV2(
           [FromQuery] PaginationParams paginationQuery)
        {
            var results = await _authenticationService.GetPaginationVersion2(paginationQuery);
            return Ok(results);
        }


    }
}
