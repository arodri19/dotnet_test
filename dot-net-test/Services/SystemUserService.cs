using dotnet_test.Helpers;
using dotnet_test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_test.Services
{

    public interface ISystemUserService
    {
        SystemUser Authenticate(string username, string password);
        IEnumerable<SystemUser> GetAll();
        SystemUser GetById(int id);
        List<SystemUser> GetByValues(string name, string cpf);
        SystemUser Create(SystemUser user, string password);
        void Update(SystemUser user, string password = null);
        void Delete(int id);
    }

    public class SystemUserService : ISystemUserService
    {
        private HealthcareContext _context;
        private PasswordTasks _passwordTasks = new PasswordTasks();

        public SystemUserService(HealthcareContext context)
        {
            _context = context;
        }

        public SystemUser Authenticate(string cpf, string password)
        {

            if (string.IsNullOrEmpty(cpf) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.SystemUser.SingleOrDefault(x => x.Cpf == cpf);

            if (user == null)
                return null;

            if (!_passwordTasks.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        public SystemUser Create(SystemUser user, string password)
        {

            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Preencha a senha");

            if (_context.SystemUser.Any(x => x.Cpf == user.Cpf))
                throw new AppException("Usuário \"" + user.Cpf + "\" já registrado no sistema");

            byte[] passwordHash, passwordSalt;
            _passwordTasks.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.SystemUser.Add(user);
            _context.SaveChanges();

            return user;
        }

        public void Delete(int id)
        {
            var user = _context.SystemUser.Find(id);
            if (user != null)
            {
                _context.SystemUser.Remove(user);
                _context.SaveChanges();
            }
        }

        public IEnumerable<SystemUser> GetAll()
        {
            return _context.SystemUser;
        }

        public SystemUser GetById(int id)
        {
            return _context.SystemUser.Find(id);
        }

        public List<SystemUser> GetByValues(string name, string cpf)
        {
            return (from c in _context.SystemUser
                    where c.Cpf == cpf || c.Name == name
                    select c).ToList<SystemUser>();
        }

        public void Update(SystemUser userVM, string password = null)
        {
            var user = _context.SystemUser.Find(userVM.ID);

            if (user == null)
                throw new AppException("Usuário não encontrado");

            if (userVM.Cpf != user.Cpf)
            {
                // username has changed so check if the new username is already taken
                if (_context.SystemUser.Any(x => x.Cpf == userVM.Cpf))
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

            _context.SystemUser.Update(user);
            _context.SaveChanges();
        }

    }

}
