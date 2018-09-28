using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_test.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicController : ControllerBase
    {
        // GET api/values
        [HttpGet("Medic")]
        public ActionResult<Medic> Get()
        {
            Medic medic = new Medic();

            medic.Cpf = "teste";
            medic.Crm = "teste";
            medic.LastAccess = DateTime.Now;

            return medic;
        }

        // GET api/values/5
        [HttpGet("Medic/{name}/{cpf}/{crm}")]
        public ActionResult<string> Get(string name,string cpf, string crm)
        {
            return "value";
        }

        // POST api/values
        [HttpPost("Medic")]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("Medic/{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("Medic/{id}")]
        public void Delete(int id)
        {
        }
    }
}
