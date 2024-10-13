using Microsoft.EntityFrameworkCore;
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
            var item = await _context.Accounts.Where(c => c.RoleId == role_id).ToListAsync();
            if (item == null) return null;
            return item;
        }
        public async Task<Account?> GetAccountByEmail(string email)
        {
            var account = await _context.Accounts.SingleOrDefaultAsync(acc => acc.Email.Equals(email));
            if (account == null) return null;
            return account;
        }

        public async Task<Account> GetById(int id)
        {
            var item = await _context.Accounts.FirstOrDefaultAsync(c => c.AccountId == id);
            if (item == null) return null;
            return item;
        }

        public async Task Add(Account item)
        {
            _context.Accounts.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Account item)
        {
            var existingItem = await GetById(item.AccountId);
            if (existingItem == null) return;

            _context.Entry(existingItem).CurrentValues.SetValues(item);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var item = await GetById(id);
            if (item == null) return;

            _context.Accounts.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
