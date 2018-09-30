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
    [Authorize(Roles = "Medic")]
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineScheduleTreatmentController : ControllerBase
    {
        private IMedicineScheduleTreatmentService _medicineScheduleService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public MedicineScheduleTreatmentController(IMedicineScheduleTreatmentService medicineScheduleService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _medicineScheduleService = medicineScheduleService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Cadastra um medicamento para o paciente no sistema - Médico
        /// </summary>
        /// <param name="medicineScheduleVM">Objeto do MedicineScheduleTreatmentViewModel</param>
        /// <returns>Cadastro realizado com sucesso</returns>
        [HttpPost("medicinesschedules")]
        public ActionResult Post([FromBody] MedicineScheduleTreatmentViewModel medicineScheduleVM)
        {

            var schedule = _mapper.Map<MedicineScheduleTreatment>(medicineScheduleVM);
            try
            {
                _medicineScheduleService.Create(schedule);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Busca dos medicamentos associados aos pacientes - Médico
        /// </summary>
        /// <returns>Retorna todos os medicamentos associados aos pacientes</returns>
        [HttpGet("medicinesschedules")]
        public ActionResult GetAll()
        {
            var medicinesSchedule = _medicineScheduleService.GetAll();
            var viewMedicineSchedule = _mapper.Map<IList<MedicineScheduleTreatmentResultViewModel>>(medicinesSchedule);


            return Ok(viewMedicineSchedule);
        }

        /// <summary>
        /// Busca dos medicamentos associados aos pacientes por parametros relacionados ao médico - Médico
        /// </summary>
        /// <param name="id">Id do médico cadastrado no sistema</param>
        /// <param name="name">Nome do médico cadastrado no sistema</param>
        /// <param name="cpf">Cpf do médico cadastrado no sistema</param>
        /// <param name="crm">Crm do médico cadastrado no sistema</param>
        /// <returns>Retorna todos os medicamentos associados aos pacientes</returns>
        [HttpGet("medicinesschedules/medic/{id}/{name}/{cpf}/{crm}")]
        public ActionResult GetAllByMedic(int id = 0, string name = "", string cpf = "", string crm = "")
        {
            var medicinesSchedule = _medicineScheduleService.GetByMedic(id, name, cpf, crm);
            var viewMedicineSchedule = _mapper.Map<IList<MedicineScheduleTreatmentResultViewModel>>(medicinesSchedule);


            return Ok(viewMedicineSchedule);
        }

        /// <summary>
        /// Busca dos medicamentos associados aos pacientes por parametros relacionados ao paciente - Médico
        /// </summary>
        /// <param name="id">Id do paciente cadastrado no sistema</param>
        /// <param name="name">Nome do paciente cadastrado no sistema</param>
        /// <param name="cpf">Cpf do paciente cadastrado no sistema</param>
        /// <returns>Retorna todos os medicamentos associados aos pacientes</returns>
        [HttpGet("medicinesschedules/patient/{id}/{name}/{cpf}")]
        public ActionResult GetAllByPatient(int id = 0, string name = "", string cpf = "")
        {
            var medicinesSchedule = _medicineScheduleService.GetByPatient(id, name, cpf);
            var viewMedicineSchedule = _mapper.Map<IList<MedicineScheduleTreatmentResultViewModel>>(medicinesSchedule);


            return Ok(viewMedicineSchedule);
        }

        /// <summary>
        /// Busca dos medicamentos associados aos pacientes por parametros relacionados ao paciente/data - Médico
        /// </summary>
        /// <param name="name">Nome do paciente cadastrado no sistema</param>
        /// <param name="cpf">Cpf do paciente cadastrado no sistema</param>
        /// <param name="dateTimeSchedule">Data agendada do paciente</param>
        /// <returns>Retorna todos os medicamentos associados aos pacientes</returns>
        [HttpGet("medicinesschedules/{name}/{dateTimeSchedule}")]
        public ActionResult GetByDate(string name = "", string cpf = "", DateTime? dateTimeSchedule = null)
        {
            if (!dateTimeSchedule.HasValue)
                dateTimeSchedule = DateTime.Now;

            var medicinesSchedule = _medicineScheduleService.GetByDate(name, cpf, dateTimeSchedule);
            var viewMedicineSchedule = _mapper.Map<IList<MedicineScheduleTreatmentResultViewModel>>(medicinesSchedule);

            return Ok(viewMedicineSchedule);
        }

        /// <summary>
        /// Atualiza os dados dos medicamentos associados ao usuário - Médico
        /// </summary>
        /// <param name="idScheduleTreatment">Código do agendamento feito no sistema</param>
        /// <param name="scheduleVM">Objeto do MedicineScheduleTreatmentViewModel</param>
        /// <returns>Atualiza dados do medicamentos associados ao paciente, de acordo com os parametros utilizados</returns>
        [HttpPut("medicinesschedules/{idTreatment}")]
        public ActionResult Put(int idScheduleTreatment, [FromBody] MedicineScheduleTreatmentViewModel scheduleVM)
        {
            // map dto to entity and set id
            var schedule = _mapper.Map<MedicineScheduleTreatment>(scheduleVM);
            schedule.ScheduleTreatmentID = idScheduleTreatment;

            try
            {
                // save 
                _medicineScheduleService.Update(schedule);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Atualiza o status do medicamento ao paciente - Médico
        /// </summary>
        /// <param name="id">Código do agendamento feito no sistema</param>
        /// <returns>Atualiza dados do medicamentos associados ao paciente, de acordo com os parametros utilizados</returns>
        [HttpDelete("medicinesschedules/{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                // save 
                _medicineScheduleService.Delete(id);
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