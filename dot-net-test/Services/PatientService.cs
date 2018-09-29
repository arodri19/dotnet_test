using dotnet_test.Helpers;
using dotnet_test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_test.Services
{
    public interface IPatientService
    {
        Patient Authenticate(string username, string password);
        IEnumerable<Patient> GetAll();
        Patient GetById(int id);
        List<Patient> GetByValues(string name, string cpf);
        Patient Create(Patient user, string password);
        void Update(Patient user, string password = null);
        void Delete(int id);
    }

    public class PatientService : IPatientService
    {
        private HealthcareContext _context;
        private PasswordTasks _passwordTasks = new PasswordTasks();

        public PatientService(HealthcareContext context)
        {
            _context = context;
        }

        public Patient Authenticate(string cpf, string password)
        {

            if (string.IsNullOrEmpty(cpf) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.Patient.SingleOrDefault(x => x.Cpf == cpf);

            if (user == null)
                return null;

            if (!_passwordTasks.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        public Patient Create(Patient user, string password)
        {

            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Preencha a senha");

            if (_context.Patient.Any(x => x.Cpf == user.Cpf))
                throw new AppException("Usuário \"" + user.Cpf + "\" já registrado no sistema");

            byte[] passwordHash, passwordSalt;
            _passwordTasks.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Patient.Add(user);
            _context.SaveChanges();

            return user;
        }

        public void Delete(int id)
        {
            var user = _context.Patient.Find(id);
            if (user != null)
            {
                _context.Patient.Remove(user);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Patient> GetAll()
        {
            return _context.Patient;
        }

        public Patient GetById(int id)
        {
            return _context.Patient.Find(id);
        }

        public List<Patient> GetByValues(string name, string cpf)
        {
            return (from c in _context.Patient
                    where c.Cpf == cpf || c.Name == name
                    select c).ToList<Patient>();
        }

        public void Update(Patient userVM, string password = null)
        {
            var user = _context.Patient.Find(userVM.ID);

            if (user == null)
                throw new AppException("Usuário não encontrado");

            if (userVM.Cpf != user.Cpf)
            {
                // username has changed so check if the new username is already taken
                if (_context.Patient.Any(x => x.Cpf == userVM.Cpf))
                    throw new AppException("Usuário \"" + user.Cpf + "\" já registrado no sistema");
            }

            // update user properties
            user.Cpf = userVM.Cpf;
            user.Name = userVM.Name;

            // update password if it was entered
            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                _passwordTasks.CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            _context.Patient.Update(user);
            _context.SaveChanges();
        }

    }
}
