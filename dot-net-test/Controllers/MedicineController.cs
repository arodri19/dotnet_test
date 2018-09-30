using System;
using System.Collections.Generic;
using System.Linq;
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

namespace dotnet_test.Controllers
{
    //[Authorize(Roles = "Medic")]
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private IMedicineService _medicineService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public MedicineController(IMedicineService userService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _medicineService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("medicines")]
        public ActionResult Post([FromBody] MedicineViewModel medicineVM)
        {

            var medicine = _mapper.Map<Medicine>(medicineVM);
            try
            {
                _medicineService.Create(medicine);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { errorMessage = ex.Message });
            }
        }

        // GET api/values
        [HttpGet("medicines")]
        public ActionResult GetAll()
        {
            var medicines = _medicineService.GetAll();
            var viewMedicine = _mapper.Map<IList<MedicineViewModel>>(medicines);


            return Ok(viewMedicine);
        }

        // GET api/values/5
        [HttpGet("medicines/{name}")]
        public ActionResult Get(string name)
        {
            var medicine = _medicineService.GetByValues(name);
            var viewMedicine = _mapper.Map<IList<MedicineViewModel>>(medicine);

            return Ok(viewMedicine);
        }

        // PUT api/values/5
        [HttpPut("medicines/{id}")]
        public ActionResult Put(int id, [FromBody] MedicineViewModel medicineVM)
        {
            // map dto to entity and set id
            var medicine = _mapper.Map<Medicine>(medicineVM);
            medicine.ID = id;

            try
            {
                // save 
                _medicineService.Update(medicine);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE api/values/5
        [HttpDelete("medicines/{id}")]
        public ActionResult Delete(int id)
        {
            _medicineService.Delete(id);
            return Ok();
        }
    }
}