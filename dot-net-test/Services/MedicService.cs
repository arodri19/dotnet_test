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
        Medic GetById(int id);
        List<Medic> GetByValues(string name, string cpf, string crm);
        Medic Create(Medic user, string password);
        void Update(Medic user, string password = null);
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

        public Medic Create(Medic user, string password)
        {

            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Preencha a senha");

            if (_context.Medic.Any(x => x.Cpf == user.Cpf))
                throw new AppException("Usuário \"" + user.Cpf + "\" já registrado no sistema");

            byte[] passwordHash, passwordSalt;
            _passwordTasks.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Medic.Add(user);
            _context.SaveChanges();

            return user;
        }

        public void Delete(int id)
        {
            var user = _context.Medic.Find(id);

            if (user == null)
                throw new AppException("Usuário não encontrado");

            // update user properties
            user.Disabled = true;

            _context.Medic.Update(user);
            _context.SaveChanges();
        }

        public IEnumerable<Medic> GetAll()
        {
            return _context.Medic;
        }

        public Medic GetById(int id)
        {
            return _context.Medic.Find(id);
        }

        public List<Medic> GetByValues(string name, string cpf, string crm)
        {
            return (from c in _context.Medic
                   where c.Cpf == cpf || c.Name.Contains(name) || c.Crm == crm
                   select c).ToList<Medic>();
        }

        public void Update(Medic userVM, string password = null)
        {
            var user = _context.Medic.Find(userVM.ID);

            if (user == null)
                throw new AppException("Usuário não encontrado");

            if (userVM.Cpf != user.Cpf)
            {
                // username has changed so check if the new username is already taken
                if (_context.Medic.Any(x => x.Cpf == userVM.Cpf))
                    throw new AppException("Usuário \"" + user.Cpf + "\" já registrado no sistema");
            }

            // update user properties
            user.Cpf = userVM.Cpf;
            user.Crm = userVM.Crm;
            user.Name = userVM.Name;
            user.TypeSpeciality = userVM.TypeSpeciality;

            // update password if it was entered
            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                _passwordTasks.CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            _context.Medic.Update(user);
            _context.SaveChanges();
        }
        
    }
}
