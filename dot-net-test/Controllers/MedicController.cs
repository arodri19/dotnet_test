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
    [Authorize(Roles ="Medic")]
    [Route("api/[controller]")]
    [ApiController]
    public class MedicController : ControllerBase
    {

        private IMedicService _medicService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public MedicController(IMedicService medicService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _medicService = medicService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("medics/authenticate")]
        public IActionResult Authenticate([FromBody]MedicViewModel medicVM)
        {
            var medic = _medicService.Authenticate(medicVM.Cpf, medicVM.Password);

            if (medic == null)
                return BadRequest(new { errorMessage = "Senha ou Cpf incorretos" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, medic.ID.ToString()),
                    new Claim(ClaimTypes.Role, medic.GetType().Name)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info (without password) and token to store client side
            return Ok(new
            {
                Id = medic.ID,
                Cpf = medic.Cpf,
                Name = medic.Name,
                Crm = medic.Crm,
                Token = tokenString
            });
        }

        [AllowAnonymous]
        [HttpPost("medics")]
        public ActionResult Post([FromBody] MedicViewModel medicVM)
        {

            var medic = _mapper.Map<Medic>(medicVM);
            try
            {
                _medicService.Create(medic, medicVM.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { errorMessage = ex.Message });
            }
        }

        // GET api/values
        [HttpGet("medics")]
        public ActionResult GetAll()
        {
            var medics = _medicService.GetAll();
            var viewMedic = _mapper.Map<IList<MedicViewModel>>(medics);
            

            return Ok(viewMedic);
        }

        // GET api/values/5
        [HttpGet("medics/{name}/{cpf}/{crm}")]
        public ActionResult Get(string name, string cpf, string crm)
        {
            var medic = _medicService.GetByValues(name, cpf, crm);
            var viewMedic = _mapper.Map<IList<MedicViewModel>>(medic);

            return Ok(viewMedic);
        }

        // PUT api/values/5
        [HttpPut("medics/{id}")]
        public ActionResult Put(int id, [FromBody] MedicViewModel medicVM)
        {
            // map dto to entity and set id
            var medic = _mapper.Map<Medic>(medicVM);
            medic.ID = id;

            try
            {
                // save 
                _medicService.Update(medic, medicVM.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE api/values/5
        [HttpDelete("medics/{id}")]
        public ActionResult Delete(int id)
        {
            _medicService.Delete(id);
            return Ok();
        }
    }
}
