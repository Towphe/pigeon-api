
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using services.auth;

namespace api.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<string> CreateUser(string email, string userId)
        {
            // check if Supabase is the one that accesses this

            var res = await _authService.CreateUser(email, userId);

            if (res.IsSuccess == false)
            {
                Response.StatusCode = 400;
                return "";
            }

            return "Successfuly created user.";
        }
    }
}