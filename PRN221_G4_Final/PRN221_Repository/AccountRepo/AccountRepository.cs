﻿using PRN221_DataAccess.DAOs;
using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_Repository.AccountRepo
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AccountDAO _accountDAO;

        public AccountRepository(AccountDAO accountDAO)
        {
            _accountDAO = accountDAO;
        }

        public async Task<IEnumerable<Account>> GetAll()
        {
            return await _accountDAO.getAll();
        }

        public async Task<Account?> GetByUsername(string username)
        {
            return await _accountDAO.getByUsername(username);
        }
    }
}