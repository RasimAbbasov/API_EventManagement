using API_EventManagement.Data;
using API_EventManagement.Dtos.Users;
using API_EventManagement.Interfaces;
using API_EventManagement.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_EventManagement.Controllers
{
    public class AccountController(EventAppDbContext eventAppDbContext,UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager,IMapper mapper,ITokenService tokenService) : BaseController
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
            if (result.Succeeded)
            {
                string code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                return Ok(new { message = $"Please confirm your email with the code that you have received {code}" });
            }

            else if(!result.Succeeded)
                return BadRequest(result.Errors);
            
            return Ok("User registered successfully.");
        }
        [HttpPost("EmailVerification")]
        public async Task<IActionResult> EmailVerification(string? email,string? code)
        {
            if(email== null || code == null)
            {
                return BadRequest("Email and code are required for verification.");
            }
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound("User not found.");
            } 
            var IsVerified= await userManager.ConfirmEmailAsync(user,code);
            if (IsVerified.Succeeded)
            {
                return Ok("Email verified successfully.");

            }
            return BadRequest("Email verification failed. Please check the code and try again.");

        }



        [HttpPost("login")] 
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await userManager.FindByNameAsync(loginDto.UserName);
            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }
            var result = await userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!result)
            {
                return Unauthorized("Invalid username or password.");
            }
            if (!user.EmailConfirmed)
            {
                return BadRequest("Email not confirmed. Please verify your email before logging in.");
            }
            var token = await tokenService.GenerateToken(user);
            return Ok(new { token });
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
        {
            var user = await userManager.FindByEmailAsync(forgotPasswordDto.Email);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            // Here you would typically send the token to the user's email
            return Ok(new 
            {  
                token = token,
                email= user.Email 
            });
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            var user = await userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            var result = await userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.NewPassword);
            if (result.Succeeded)
            {
                resetPasswordDto.ConfirmPassword = resetPasswordDto.NewPassword; // Ensure confirmation matches new password
                return Ok("Password reset successfully.");
            }
            return BadRequest(result.Errors);
        }


        [HttpPost("role")]
        public async Task<IActionResult> AddRole()
        {
            if (!await roleManager.RoleExistsAsync("Member"))
                await roleManager.CreateAsync(new IdentityRole { Name = "Member" });

            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new IdentityRole { Name = "Admin" });

            return NoContent();
        }
    }
}
