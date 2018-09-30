﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_test.Helpers;
using dotnet_test.Models;
using dotnet_test.Models.ViewModels;
using dotnet_test.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace dotnet_test.Controllers
{
    //[Authorize(Roles ="SystemUser")]
    [Route("api/[controller]")]
    [ApiController]
    public class SystemUserController : ControllerBase
    {
        private ISystemUserService _userService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public SystemUserController(ISystemUserService userService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("systemusers/authenticate")]
        public IActionResult Authenticate([FromBody]SystemUserViewModel userVM)
        {
            var user = _userService.Authenticate(userVM.Cpf, userVM.Password);

            if (user == null)
                return BadRequest(new { errorMessage = "Senha ou Cpf incorretos" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.ID.ToString()),
                    new Claim(ClaimTypes.Role, user.GetType().Name)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info (without password) and token to store client side
            return Ok(new
            {
                Id = user.ID,
                Cpf = user.Cpf,
                Name = user.Name,
                Token = tokenString
            });
        }

        [AllowAnonymous]
        [HttpPost("systemusers")]
        public ActionResult Post([FromBody] SystemUserViewModel userVM)
        {

            var user = _mapper.Map<SystemUser>(userVM);
            try
            {
                _userService.Create(user, userVM.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { errorMessage = ex.Message });
            }
        }

        // GET api/values
        [HttpGet("systemusers")]
        public ActionResult GetAll()
        {
            var users = _userService.GetAll();
            var viewUser = _mapper.Map<IList<SystemUserViewModel>>(users);

            return Ok(viewUser);
        }

        // GET api/values/5
        [HttpGet("systemusers/{name}/{cpf}")]
        public ActionResult Get(string name, string cpf)
        {
            var user = _userService.GetByValues(name, cpf);
            var viewUser = _mapper.Map<IList<SystemUserViewModel>>(user);

            return Ok(viewUser);
        }

        // PUT api/values/5
        [HttpPut("systemusers/{id}")]
        public ActionResult Put(int id, [FromBody] SystemUserViewModel userVM)
        {
            // map dto to entity and set id
            var user = _mapper.Map<SystemUser>(userVM);
            user.ID = id;

            try
            {
                // save 
                _userService.Update(user, userVM.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE api/values/5
        [HttpDelete("systemusers/{id}")]
        public ActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Ok();
        }
    }
}