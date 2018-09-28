using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_test.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        // GET api/values
        [HttpGet("patients")]
        public ActionResult<Patient> Get()
        {
            Patient patient = new Patient();

            patient.Cpf = "teste";
            patient.LastAccess = DateTime.Now;

            //return new string[] { "value1", "value2" };
            return patient;
        }

        // GET api/values/5
        [HttpGet("patients/{name}/{cpf}")]
        public ActionResult<string> Get(string name, string cpf, string crm)
        {
            return "value";
        }

        // POST api/values
        [HttpPost("patients")]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("patients/{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("medics/{id}")]
        public void Delete(int id)
        {
        }
    }
}