using dotnet_test.Helpers;
using dotnet_test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_test.Services
{

    public interface IMedicineService
    {
        IEnumerable<Medicine> GetAll();
        List<Medicine> GetByValues(string name);
        Medicine Create(Medicine medicine);
        void Update(Medicine medicine);
        void Delete(int id);
    }

    public class MedicineService : IMedicineService
    {
        private HealthcareContext _context;
        private PasswordTasks _passwordTasks = new PasswordTasks();

        public MedicineService(HealthcareContext context)
        {
            _context = context;
        }

        public Medicine Create(Medicine medicine)
        {

            _context.Medicine.Add(medicine);
            _context.SaveChanges();

            return medicine;
        }

        public void Delete(int id)
        {
            var medicine = _context.Medicine.Find(id);
            if (medicine != null)
            {
                _context.Medicine.Remove(medicine);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Medicine> GetAll()
        {
            return _context.Medicine;
        }

        public Medicine GetById(int id)
        {
            return _context.Medicine.Find(id);
        }

        public List<Medicine> GetByValues(string name)
        {
            return (from c in _context.Medicine
                    where c.Name == name
                    select c).ToList<Medicine>();
        }

        public void Update(Medicine medicineVM)
        {
            var medicine = _context.Medicine.Find(medicineVM.ID);

            if (medicineVM == null)
                throw new AppException("O medicamento não foi encontrado");

            if (medicineVM.Name != medicineVM.Name)
            {
                // username has changed so check if the new username is already taken
                if (_context.Medicine.Any(x => x.Name == medicineVM.Name))
                    throw new AppException("O medicamento \"" + medicine.Name + "\" já registrado no sistema");
            }

            // update user properties
            medicine.Name = medicineVM.Name;
            medicine.Obs = medicineVM.Obs;

            _context.Medicine.Update(medicine);
            _context.SaveChanges();
        }

    }

}
