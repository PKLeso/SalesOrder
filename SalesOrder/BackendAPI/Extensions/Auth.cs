﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SalesOrder.Controllers;
using SalesOrder.Data;
using SalesOrder.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SalesOrder.Extensions.HubConfig
{
    [Route("api/[controller]")] //auth instead of [controller]
    [ApiController]
    public class Auth : BaseController
    {
        public Auth(SalesOrderDbContext ctx, IConfiguration iConfig) : base(ctx, iConfig)
        {
        }

        [HttpPost, Route("login")]
        public IActionResult Login([FromBody] User userLogin)
        {
            if (userLogin == null)
                return BadRequest("Invalid login request! Contact the administrator");

            try
            {

                //User tempUser = context.Users.Where(u => u.Username == userLogin.Username && u.Password == userLogin.Password).SingleOrDefault();


                if (userLogin.Username == configuration["TemporaryLoginDetails:Username"] // tempUser.Username // 
                    && userLogin.Password == configuration["TemporaryLoginDetails:Password"]) // tempUser.Password) //  This is for testing purposes. Ideally yous get user details from the database
                {
                    var secreteKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtOptions:Key"]));
                    var signingCredentials = new SigningCredentials(secreteKey, SecurityAlgorithms.HmacSha256);

                    // If working with Roles
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userLogin.Username),
                    new Claim(ClaimTypes.Role, "Admin")
                };

                    var tokenOptions = new JwtSecurityToken(
                        issuer: configuration["JwtOptions:Issuer"],
                        audience: configuration["JwtOptions:Audiance"],
                        claims: new List<Claim>(),
                        expires: DateTime.Now.AddMinutes(5),
                        signingCredentials: signingCredentials
                    );

                    var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                    return Ok(new { Token = token });
                };

                return Unauthorized();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
