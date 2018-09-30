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
        List<ScheduleTreatment> GetByValues(string name, DateTime dateTimeSchedule);
        ScheduleTreatment Create(ScheduleTreatment schedule, Treatment treatment);
        void Update(ScheduleTreatment schedule);
        void Delete(int id);
    }

    public class ScheduleTreatmentService : IScheduleTreatmentService
    {
        private HealthcareContext _context;
        private PasswordTasks _passwordTasks = new PasswordTasks();

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
            var schedule = _context.ScheduleTreatment.Find(id);
            if (schedule != null)
            {
                _context.ScheduleTreatment.Remove(schedule);
                _context.SaveChanges();
            }
        }

        public IEnumerable<ScheduleTreatment> GetAll()
        {
            return _context.Set<ScheduleTreatment>().Include(e=>e.Medic).Include(e=>e.Patient).Include(e=>e.Treatment);
        }

        public ScheduleTreatment GetById(int id)
        {
            return _context.ScheduleTreatment.Find(id);
        }

        public List<ScheduleTreatment> GetByValues(string name, DateTime dateTimeSchedule)
        {
            return (from c in _context.ScheduleTreatment
                    where c.Patient.Name == name || c.Schedule == dateTimeSchedule
                    select c).ToList<ScheduleTreatment>();
        }

        public void Update(ScheduleTreatment scheduleVM)
        {
            var schedule = _context.ScheduleTreatment.Find(scheduleVM.TreatmentID);

            if (scheduleVM == null)
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
