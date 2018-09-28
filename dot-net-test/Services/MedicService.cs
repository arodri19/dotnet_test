using dotnet_test.Helpers;
using dotnet_test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_test.Services
{
    public interface IMedicService
    {
        Medic Authenticate(string username, string password);
        IEnumerable<Medic> GetAll();
        List<Medic> GetByValues(string name, string cpf, string crm);
        Medic Create(Medic medic, string password);
        void Update(Medic medic, string password = null);
        void Delete(int id);
    }

    public class MedicService : IMedicService
    {
        private HealthcareContext _context;
        private PasswordTasks _passwordTasks = new PasswordTasks();

        public MedicService(HealthcareContext context)
        {
            _context = context;
        }

        public Medic Authenticate(string cpf, string password)
        {

            if (string.IsNullOrEmpty(cpf) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.Medic.SingleOrDefault(x => x.Cpf == cpf);

            if (user == null)
                return null;

            if (!_passwordTasks.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        public Medic Create(Medic medic, string password)
        {

            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Preencha a senha");

            if (_context.Medic.Any(x => x.Cpf == medic.Cpf))
                throw new AppException("Usuário \"" + medic.Cpf + "\" já registrado no sistema");

            byte[] passwordHash, passwordSalt;
            _passwordTasks.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            medic.PasswordHash = passwordHash;
            medic.PasswordSalt = passwordSalt;

            _context.Medic.Add(medic);
            _context.SaveChanges();

            return medic;
        }

        public void Delete(int id)
        {
            var medic = _context.Medic.Find(id);
            if (medic != null)
            {
                _context.Medic.Remove(medic);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Medic> GetAll()
        {
            return _context.Medic;
        }

        public List<Medic> GetByValues(string name, string cpf, string crm)
        {
            return (from c in _context.Medic
                   where c.Cpf == cpf || c.Name == name || c.Crm == crm
                   select c).ToList<Medic>();
        }

        public void Update(Medic medicVM, string password = null)
        {
            var medic = _context.Medic.Find(medicVM.ID);

            if (medic == null)
                throw new AppException("Usuário não encontrado");

            if (medicVM.Cpf != medic.Cpf)
            {
                // username has changed so check if the new username is already taken
                if (_context.Medic.Any(x => x.Cpf == medicVM.Cpf))
                    throw new AppException("Usuário \"" + medic.Cpf + "\" já registrado no sistema");
            }

            // update user properties
            medic.Cpf = medicVM.Cpf;
            medic.Crm = medicVM.Crm;
            medic.Name = medicVM.Name;
            medic.TypeSpeciality = medicVM.TypeSpeciality;

            // update password if it was entered
            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                _passwordTasks.CreatePasswordHash(password, out passwordHash, out passwordSalt);

                medic.PasswordHash = passwordHash;
                medic.PasswordSalt = passwordSalt;
            }

            _context.Medic.Update(medic);
            _context.SaveChanges();
        }
        
    }
}
