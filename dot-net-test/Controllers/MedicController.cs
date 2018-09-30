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
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace dotnet_test.Controllers
{
    [Authorize(Roles ="SystemUser")]
    [Route("api/[controller]")]
    [ApiController]
    public class MedicController : ControllerBase
    {

        private IMedicService _userService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public MedicController(IMedicService userService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Autenticação de usuário como Médico, utilizando cpf e senha - Qualquer pessoa
        /// </summary>
        /// <param name="userVM">Objeto do MedicViewModel</param>
        /// <returns>Retorna algumas informações sobre o usuário e o token a ser utilizado enquanto estiver utilizando este serviço</returns>
        [AllowAnonymous]
        [HttpPost("medics/authenticate")]
        public IActionResult Authenticate([FromBody]MedicViewModel userVM)
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
                Crm = user.Crm,
                Token = tokenString
            });
        }

        /// <summary>
        /// Cadastra um médico no sistema - Qualquer pessoa
        /// </summary>
        /// <param name="userVM">Objeto do MedicViewModel</param>
        /// <returns>Cadastro realizado com sucesso</returns>
        [AllowAnonymous]
        [HttpPost("medics")]
        public ActionResult Post([FromBody] MedicViewModel userVM)
        {

            var user = _mapper.Map<Medic>(userVM);
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
        /// Busca dos médicos cadastrados no sistema - Usuário do sistema
        /// </summary>
        /// <returns>Retorna todos os médicos cadastrados no sistema</returns>
        [HttpGet("medics")]
        public ActionResult GetAll()
        {
            var users = _userService.GetAll();
            var viewUser = _mapper.Map<IList<MedicViewModel>>(users);
            

            return Ok(viewUser);
        }

        /// <summary>
        /// Busca dos médicos cadastrados no sistema - Usuário do sistema
        /// </summary>
        /// <param name="name">Nome do médico</param>
        /// <param name="cpf">Cpf do médico</param>
        /// <param name="crm">Crm do médico</param>
        /// <returns>Retorna todos os médicos cadastrados no sistema de acordo com os parametros utilizados</returns>
        [HttpGet("medics/{name}/{cpf}/{crm}")]
        public ActionResult Get(string name, string cpf, string crm)
        {
            var user = _userService.GetByValues(name, cpf, crm);
            var viewUser = _mapper.Map<IList<MedicViewModel>>(user);

            return Ok(viewUser);
        }

        /// <summary>
        /// Atualiza os dados dos médicos cadastrados no sistema - Usuário do sistema
        /// </summary>
        /// <param name="id">Código do médico cadastrado no sistema</param>
        /// <param name="userVM">Objeto do MedicViewModel</param>
        /// <returns>Atualiza dados do médico, de acordo com os parametros utilizados</returns>
        [HttpPut("medics/{id}")]
        public ActionResult Put(int id, [FromBody] MedicViewModel userVM)
        {
            // map dto to entity and set id
            var user = _mapper.Map<Medic>(userVM);
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
        /// Atualiza o status do Médico no hospital - Usuário do sistema
        /// </summary>
        /// <param name="id">Código do médico registrado no sistema</param>
        /// <returns>Atualiza dados do médico, de acordo com os parametros utilizados</returns>
        [HttpDelete("medics/{id}")]
        public ActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Ok();
        }
    }
}
