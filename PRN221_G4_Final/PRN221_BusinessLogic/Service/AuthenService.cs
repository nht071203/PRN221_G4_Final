using Microsoft.AspNetCore.Http;
using PRN221_BusinessLogic.Interface;
using PRN221_Models.Models;
using PRN221_Repository.AccountRepo;
using PRN221_Repository.RoleRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_BusinessLogic.Service
{
    public class AuthenService : IAuthenService
    {
        private readonly IAccountRepository _accountRepo;
        private readonly IRoleRepository _roleRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenService(IAccountRepository accountRepository, IRoleRepository roleRepository, IHttpContextAccessor httpContextAccessor)
        {
            _accountRepo = accountRepository;
            _roleRepo = roleRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Role?> GetRoleById(int roleId)
        {
            return await _roleRepo.getById(roleId);
        }

        public async Task<Account?> Login(string username, string password)
        {
            var account = await _accountRepo.GetByUsername(username);

            if (account == null) return null;

            //string hashpass = HashPass32(password);
            //if(!hashpass.Equals(account.Password)) return false;

            if (!password.Equals(account.Password)) return null;
            return account;
        }

        public IHttpContextAccessor Get_httpContextAccessor()
        {
            return _httpContextAccessor;
        }

        public async Task<Account?> Register(Account account)
        {
            if(account == null) return null;

            return await _accountRepo.Add(account);
        }

        private string HashPass32(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                var hashedString = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

                return hashedString.Substring(0, 32);
            }
        }

        
    }
}
