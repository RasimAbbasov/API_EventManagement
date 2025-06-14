using API_EventManagement.Data;
using API_EventManagement.Dtos.Users;
using API_EventManagement.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API_EventManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(EventAppDbContext eventAppDbContext,UserManager<AppUser> userManager,IMapper mapper) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var user= await userManager.FindByNameAsync(registerDto.UserName);
            if(user != null)
            {
                return Conflict("User already exists.");
            }
            user=mapper.Map<AppUser>(registerDto);

            IdentityResult result = await userManager.CreateAsync(user, registerDto.Password);
            if(!result.Succeeded)
             return BadRequest(result.Errors);
            
            await userManager.AddToRoleAsync(user, "Member");
            return Ok("User registered successfully.");
        }
    }
}
