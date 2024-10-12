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
    }
}
