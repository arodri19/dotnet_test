using System;
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
    [Authorize(Roles ="SystemUser")]
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

        /// <summary>
        /// Autenticação de usuário como Usuário do sistema, utilizando cpf e senha - Qualquer pessoa
        /// </summary>
        /// <param name="userVM">Objeto do SystemUserViewModel</param>
        /// <returns>Retorna algumas informações sobre o usuário e o token a ser utilizado enquanto estiver utilizando este serviço</returns>
        [AllowAnonymous]
        [HttpPost("systemusers/authenticate")]
        public IActionResult Authenticate([FromBody]SystemUserViewModel userVM)
        {
            var user = _userService.Authenticate(userVM.Cpf, userVM.Password);

            if (user == null)
                return BadRequest(new { message = "Senha ou Cpf incorretos" });

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

        /// <summary>
        /// Cadastra um usuário do sistema no sistema - Usuário do sistema
        /// </summary>
        /// <param name="userVM">Objeto do SystemUserViewModel</param>
        /// <returns>Cadastro realizado com sucesso</returns>
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
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Busca dos usuários do sistema cadastrados no sistema - Usuário do sistema
        /// </summary>
        /// <returns>Retorna todos os usuários do sistema cadastrados no sistema</returns>
        [HttpGet("systemusers")]
        public ActionResult GetAll()
        {
            var users = _userService.GetAll();
            var viewUser = _mapper.Map<IList<SystemUserViewModel>>(users);

            return Ok(viewUser);
        }

        /// <summary>
        /// Busca dos usuários do sistema cadastrados no sistema - Usuário do sistema
        /// </summary>
        /// <param name="name">Nome do usuário do sistema</param>
        /// <param name="cpf">Cpf do usuário do sistema</param>
        /// <returns>Retorna todos os usuários dos sistemas cadastrados no sistema de acordo com os parametros utilizados</returns>
        [HttpGet("systemusers/{name}/{cpf}")]
        public ActionResult Get(string name, string cpf)
        {
            var user = _userService.GetByValues(name, cpf);
            var viewUser = _mapper.Map<IList<SystemUserViewModel>>(user);

            return Ok(viewUser);
        }

        /// <summary>
        /// Atualiza os dados dos usuários do sistema cadastrados no sistema - Usuário do sistema
        /// </summary>
        /// <param name="id">Código do usuário do sistema cadastrado no sistema</param>
        /// <param name="userVM">Objeto do SystemUserViewModel</param>
        /// <returns>Atualiza dados do usuário do sistema, de acordo com os parametros utilizados</returns>
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

        /// <summary>
        /// Atualiza o status do usuário do sistema no hospital - Usuário do sistema
        /// </summary>
        /// <param name="id">Código do usuário do sistema registrado no sistema</param>
        /// <returns>Atualiza dados do usuário do sistema, de acordo com os parametros utilizados</returns>
        [HttpDelete("systemusers/{id}")]
        public ActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Ok();
        }
    }
}