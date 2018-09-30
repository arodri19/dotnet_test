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
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleTreatmentController : ControllerBase
    {
        private IScheduleTreatmentService _scheduleService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public ScheduleTreatmentController(IScheduleTreatmentService scheduleService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _scheduleService = scheduleService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("medicines")]
        public ActionResult Post([FromBody] ScheduleTreatmentViewModel scheduleVM)
        {

            var schedule = _mapper.Map<ScheduleTreatment>(scheduleVM);
            var treatment = schedule.Treatment;
            schedule.Treatment = null;
            try
            {
                _scheduleService.Create(schedule,treatment);
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
            var medicines = _scheduleService.GetAll();
            var viewMedicine = _mapper.Map<IList<ScheduleTreatmentResultViewModel>>(medicines);


            return Ok(viewMedicine);
        }

        // GET api/values/5
        [HttpGet("medicines/{name}/{dateTimeSchedule}")]
        public ActionResult Get(string name, DateTime dateTimeSchedule)
        {
            var medicine = _scheduleService.GetByValues(name, dateTimeSchedule);
            var viewMedicine = _mapper.Map<IList<ScheduleTreatmentViewModel>>(medicine);

            return Ok(viewMedicine);
        }

        // PUT api/values/5
        [HttpPut("medicines/{idTreatment}")]
        public ActionResult Put(int idTreatment, [FromBody] ScheduleTreatmentViewModel scheduleVM)
        {
            // map dto to entity and set id
            var schedule = _mapper.Map<ScheduleTreatment>(scheduleVM);
            schedule.TreatmentID = idTreatment;

            try
            {
                // save 
                _scheduleService.Update(schedule);
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
            _scheduleService.Delete(id);
            return Ok();
        }
    }
}