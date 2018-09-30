using dotnet_test.Helpers;
using dotnet_test.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_test.Services
{
    public interface IMedicineScheduleTreatmentService
    {
        IEnumerable<MedicineScheduleTreatment> GetAll();
        IEnumerable<MedicineScheduleTreatment> GetByMedic(int id, string name, string cpf, string crm);
        IEnumerable<MedicineScheduleTreatment> GetByPatient(int id, string name, string cpf);
        List<MedicineScheduleTreatment> GetByDate(string name, string cpf, DateTime? dateTimeSchedule);
        MedicineScheduleTreatment Create(MedicineScheduleTreatment medicineSchedule);
        void Update(MedicineScheduleTreatment medicineSchedule);
        void Delete(int id);
    }

    public class MedicineScheduleTreatmentService : IMedicineScheduleTreatmentService
    {
        private HealthcareContext _context;

        public MedicineScheduleTreatmentService(HealthcareContext context)
        {
            _context = context;
        }

        public MedicineScheduleTreatment Create(MedicineScheduleTreatment medicineSchedule)
        {
            _context.MedicineScheduleTreatment.Add(medicineSchedule);

            _context.SaveChanges();

            return medicineSchedule;
        }

        public void Delete(int id)
        {

            var schedule = _context.MedicineScheduleTreatment.Find(id);

            if (schedule == null)
                throw new AppException("O Agendamento não foi encontrado");

            // update user properties
            schedule.Disabled = true;

            _context.MedicineScheduleTreatment.Update(schedule);
            _context.SaveChanges();


        }

        public IEnumerable<MedicineScheduleTreatment> GetAll()
        {
            return _context.Set<MedicineScheduleTreatment>().Include(e => e.ScheduleTreatment).Include(e => e.Medicine);
        }

        public IEnumerable<MedicineScheduleTreatment> GetByMedic(int id, string name, string cpf, string crm)
        {
            return _context.Set<MedicineScheduleTreatment>().Include(e => e.ScheduleTreatment).Include(e => e.Medicine).Where(e => e.ScheduleTreatment.Medic.ID == id || e.ScheduleTreatment.Medic.Name.Contains(name) || e.ScheduleTreatment.Medic.Cpf == cpf || e.ScheduleTreatment.Medic.Crm == crm);
        }

        public IEnumerable<MedicineScheduleTreatment> GetByPatient(int id, string name, string cpf)
        {
            return _context.Set<MedicineScheduleTreatment>().Include(e => e.ScheduleTreatment).Include(e => e.Medicine).Where(e => e.ScheduleTreatment.Medic.ID == id || e.ScheduleTreatment.Medic.Name.Contains(name) || e.ScheduleTreatment.Medic.Cpf == cpf);
        }

        public MedicineScheduleTreatment GetById(int id)
        {
            return _context.MedicineScheduleTreatment.Find(id);
        }

        public List<MedicineScheduleTreatment> GetByDate(string name, string cpf, DateTime? dateTimeSchedule)
        {
            return (from c in _context.MedicineScheduleTreatment
                    where c.ScheduleTreatment.Patient.Name.Contains(name) || c.ScheduleTreatment.Schedule == dateTimeSchedule || c.ScheduleTreatment.Patient.Cpf == cpf
                    select c).ToList<MedicineScheduleTreatment>();
        }

        public void Update(MedicineScheduleTreatment medicineScheduleVM)
        {
            var schedule = _context.MedicineScheduleTreatment.Find(medicineScheduleVM.ScheduleTreatmentID);

            if (schedule == null)
                throw new AppException("Lista de medicamentos associados ao paciente não foram encontrados");

            // update user properties
            schedule.MedicineID = medicineScheduleVM.MedicineID;

            _context.MedicineScheduleTreatment.Update(schedule);
            _context.SaveChanges();
        }

    }
}
