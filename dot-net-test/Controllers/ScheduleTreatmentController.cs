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
    [Authorize(Roles = "Patient,Medic")]
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

        /// <summary>
        /// Realiza um agendamento para um paciente no sistema - Paciente
        /// </summary>
        /// <param name="scheduleVM">Objeto do ScheduleTreatmentViewModel</param>
        /// <returns>Cadastro realizado com sucesso</returns>
        /// [Authorize(Roles = "Patient")]
        [HttpPost("treatments")]
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
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Busca dos agendamentos associados aos pacientes - Médico
        /// </summary>
        /// <returns>Retorna todos os agendamentos associados aos pacientes</returns>
        [HttpGet("treatments")]
        public ActionResult GetAll()
        {
            var schedules = _scheduleService.GetAll();
            var viewSchedule = _mapper.Map<IList<ScheduleTreatmentResultViewModel>>(schedules);


            return Ok(viewSchedule);
        }

        /// <summary>
        /// Busca dos agendamentos dos pacientes por parametros relacionados ao médico - Médico
        /// </summary>
        /// <param name="id">Id do médico cadastrado no sistema</param>
        /// <param name="name">Nome do médico cadastrado no sistema</param>
        /// <param name="cpf">Cpf do médico cadastrado no sistema</param>
        /// <param name="crm">Crm do médico cadastrado no sistema</param>
        /// <returns>Retorna todos os agendamentos associados aos pacientes</returns>
        [HttpGet("treatments/medic/{id}/{name}/{cpf}/{crm}")]
        public ActionResult GetAllByMedic(int id = 0, string name = "", string cpf = "", string crm = "")
        {
            var schedules = _scheduleService.GetByMedic(id, name, cpf, crm);
            var viewSchedule = _mapper.Map<IList<ScheduleTreatmentResultViewModel>>(schedules);


            return Ok(viewSchedule);
        }

        /// <summary>
        /// Busca dos agendamentos dos pacientes por parametros relacionados ao paciente - Médico
        /// </summary>
        /// <param name="id">Id do paciente cadastrado no sistema</param>
        /// <param name="name">Nome do paciente cadastrado no sistema</param>
        /// <param name="cpf">Cpf do paciente cadastrado no sistema</param>
        /// <returns>Retorna todos os agendamentos associados aos pacientes</returns>
        [HttpGet("treatments/patient/{id}/{name}/{cpf}")]
        public ActionResult GetAllByPatient(int id = 0, string name = "", string cpf = "")
        {
            var schedules = _scheduleService.GetByPatient(id, name, cpf);
            var viewMedicine = _mapper.Map<IList<ScheduleTreatmentResultViewModel>>(schedules);


            return Ok(viewMedicine);
        }

        /// <summary>
        /// Busca dos agendamentos dos pacientes por parametros relacionados ao paciente/data - Paciente
        /// </summary>
        /// <param name="name">Nome do paciente cadastrado no sistema</param>
        /// <param name="cpf">Cpf do paciente cadastrado no sistema</param>
        /// <param name="dateTimeSchedule">Data agendada do paciente</param>
        /// <returns>Retorna todos os agendamentos associados aos pacientes</returns>
        [HttpGet("treatments/{name}/{cpf}/{dateTimeSchedule}")]
        public ActionResult GetByDate(string name = "", string cpf="", DateTime? dateTimeSchedule = null)
        {
            if (!dateTimeSchedule.HasValue)
                dateTimeSchedule = DateTime.Now;


            var schedule = _scheduleService.GetByDate(name, cpf, dateTimeSchedule);
            var viewSchedule = _mapper.Map<IList<ScheduleTreatmentViewModel>>(schedule);

            return Ok(viewSchedule);
        }

        /// <summary>
        /// Atualiza os dados dos agendamentos dos pacientes - Paciente
        /// </summary>
        /// <param name="idTreatment">Código do tratamento feito no sistema</param>
        /// <param name="scheduleVM">Objeto do ScheduleTreatmentViewModel</param>
        /// <returns>Atualiza dados do agendamentos associados ao paciente, de acordo com os parametros utilizados</returns>
        [Authorize(Roles = "Patient")]
        [HttpPut("treatments/{idTreatment}")]
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

        /// <summary>
        /// Atualiza o status do agendamento ao paciente - Paciente
        /// </summary>
        /// <param name="id">Código do agendamento feito no sistema</param>
        /// <returns>Atualiza dados dos agendamentos associados ao paciente, de acordo com os parametros utilizados</returns>
        [Authorize(Roles = "Patient")]
        [HttpDelete("treatments/{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                // save 
                _scheduleService.Delete(id);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }

        }
    }
}