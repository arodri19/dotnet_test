using dotnet_test.Helpers;
using dotnet_test.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_test.Services
{
    public interface IScheduleTreatmentService
    {
        IEnumerable<ScheduleTreatment> GetAll();
        IEnumerable<ScheduleTreatment> GetByMedic(int id, string name, string cpf, string crm);
        IEnumerable<ScheduleTreatment> GetByPatient(int id, string name, string cpf);
        List<ScheduleTreatment> GetByDate(string name, string cpf, DateTime? dateTimeSchedule = null);
        ScheduleTreatment Create(ScheduleTreatment schedule, Treatment treatment);
        void Update(ScheduleTreatment schedule);
        void Delete(int id);
    }

    public class ScheduleTreatmentService : IScheduleTreatmentService
    {
        private HealthcareContext _context;

        public ScheduleTreatmentService(HealthcareContext context)
        {
            _context = context;
        }

        public ScheduleTreatment Create(ScheduleTreatment schedule, Treatment treatment)
        {
            _context.Treatment.Add(treatment);

            _context.SaveChanges();

            schedule.TreatmentID = treatment.ID;

            _context.ScheduleTreatment.Add(schedule);

            _context.SaveChanges();

            return schedule;
        }

        public void Delete(int id)
        {

            var schedule = _context.ScheduleTreatment.Where(x => x.TreatmentID == id).FirstOrDefault<ScheduleTreatment>();

            if (schedule == null)
                throw new AppException("O Agendamento não foi encontrado");


            if (schedule.Schedule.AddHours(24) >= DateTime.Now)
            {
                throw new AppException("O Agendamento não pode ser cancelado com menos de 24h de antecedencia");
            }

            // update user properties
            schedule.Cancel = true;

            _context.ScheduleTreatment.Update(schedule);
            _context.SaveChanges();

            
        }

        public IEnumerable<ScheduleTreatment> GetAll()
        {
            return _context.Set<ScheduleTreatment>().Include(e=>e.Medic).Include(e=>e.Patient).Include(e=>e.Treatment);
        }

        public IEnumerable<ScheduleTreatment> GetByMedic(int id , string name, string cpf, string crm)
        {
            return _context.Set<ScheduleTreatment>().Include(e => e.Medic).Include(e => e.Patient).Include(e => e.Treatment).Where(e => e.Medic.ID == id || e.Medic.Name.Contains(name) || e.Medic.Cpf == cpf || e.Medic.Crm == crm);
        }

        public IEnumerable<ScheduleTreatment> GetByPatient(int id, string name, string cpf)
        {
            return _context.Set<ScheduleTreatment>().Include(e => e.Medic).Include(e => e.Patient).Include(e => e.Treatment).Where(e => e.Patient.ID == id || e.Patient.Name.Contains(name) || e.Patient.Cpf == cpf);
        }

        public ScheduleTreatment GetById(int id)
        {
            return _context.ScheduleTreatment.Where(x => x.TreatmentID == id).FirstOrDefault<ScheduleTreatment>();
        }

        public List<ScheduleTreatment> GetByDate(string name, string cpf, DateTime? dateTimeSchedule = null)
        {
            return (from c in _context.ScheduleTreatment
                    where c.Patient.Name == name || c.Schedule == dateTimeSchedule || c.Patient.Cpf == cpf
                    select c).ToList<ScheduleTreatment>();
        }

        public void Update(ScheduleTreatment scheduleVM)
        {
            var schedule = _context.ScheduleTreatment.Where(x => x.TreatmentID == scheduleVM.TreatmentID).FirstOrDefault<ScheduleTreatment>();

            if (schedule == null)
                throw new AppException("O Agendamento não foi encontrado");

            if (scheduleVM.Schedule != scheduleVM.Schedule)
            {
                // username has changed so check if the new username is already taken
                if (_context.ScheduleTreatment.Any(x => x.Schedule == scheduleVM.Schedule && x.MedicID == schedule.MedicID))
                    throw new AppException("Este dia e horário \"" + schedule.Schedule + "\" já registrado no sistema");
            }

            // update user properties
            schedule.Schedule = scheduleVM.Schedule;

            _context.ScheduleTreatment.Update(schedule);
            _context.SaveChanges();
        }

    }
}
