﻿using Microsoft.EntityFrameworkCore;
using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_DataAccess.DAOs
{
    public class AccountDAO : SingletonBase<AccountDAO>
    {

        
        private readonly Prn221Context _context;
        
        public AccountDAO(Prn221Context context)
        {
            _context = context;
        }


        public AccountDAO()
        {
            _context = new Prn221Context();
        }


        public async Task<IEnumerable<Account>> getAll()
        {
            return await _context.Accounts.ToListAsync();
        }

        public async Task<Account?> getByUsername(string username)
        {
            var account = await _context.Accounts.SingleOrDefaultAsync(acc => acc.Username.Equals(username));
            if (account == null) return null;
            return account;
        }
        public async Task<IEnumerable<Account>> GetListAccountByRoleId(int role_id)
        {
            var item = await _context.Accounts.Where(c => c.RoleId == role_id && c.IsDeleted == false).ToListAsync();
            if (item == null) return null;
            return item;
        }
        public async Task<Account?> GetAccountByEmail(string email)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(acc => acc.Email.Equals(email));
            if (account == null) return null;
            return account;
        }

        public async Task<Account> GetById(int? id)
        {
            var item = await _context.Accounts.FirstOrDefaultAsync(c => c.AccountId == id);
            if (item == null) return null;
            return item;
        }

        public async Task<Account> Add(Account item)
        {
            _context.Accounts.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }


        public async Task Update(Account item)
        {
            var existingItem = await GetById(item.AccountId);
            if (existingItem == null) return;

            _context.Entry(existingItem).CurrentValues.SetValues(item);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Account item)
        {
            var existingItem = await GetById(item.AccountId);
            if (existingItem == null) return;

            existingItem.IsDeleted = true;

            _context.Entry(existingItem).CurrentValues.SetValues(item);
            await _context.SaveChangesAsync();
        }
   
        public async Task<int> GetTotalFarmerCountAsync()
        {
            return await _context.Accounts.CountAsync(n => n.IsDeleted == false && n.RoleId == 1);
        }

        public async Task<Account> GetByFbId(string fbId)
        {
            var item = await _context.Accounts.FirstOrDefaultAsync(acc => acc.FacebookId.Equals(fbId));
            if (item == null) return null;
            return item;
        }


        // Lấy account bằng gmail để reset password
        public async Task<Account?> GetAccountByEmailForReset(string email)
        {
            var account = await _context.Accounts.SingleOrDefaultAsync(acc => acc.Email.Equals(email) && acc.IsDeleted == false && acc.FacebookId == null);
            if (account == null) return null;
            return account;
        }

        public async Task<string?> GetFullNameByUsername(string username)
        {
            return await _context.Accounts.Where(a => a.Username.Equals(username)).Select(f => f.FullName).FirstOrDefaultAsync();
        }

    }
}
