using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;
using ShopLibrary.DTOs;
using ShopLibrary.Services;

namespace WebApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController(AuthorizationService service) : ControllerBase
    {
        private readonly AuthorizationService _service = service;

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<string>> PostUserAsync(AuthorizationRequest request)
        {
            var token = await _service.AuthorizationUserWithTokenAsync(request);

            if (token == null)
                return Unauthorized("Неверный логин или пароль");       
            return Ok(token);
        }
    }
}
