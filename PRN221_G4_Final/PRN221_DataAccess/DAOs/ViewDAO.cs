using Microsoft.EntityFrameworkCore;
using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_DataAccess.DAOs
{
    public class ViewDAO : SingletonBase<ViewDAO>
    {
        public async Task<int> GetViewByPostId(int postId)
        {
            var response = _context.Views.Where(v => v.PostId == postId).Count();
            return response > 0 ? response : 0;
        }

        public async Task AddRecordPost(View view)
        {
            _context.Views.Add(view);
            await _context.SaveChangesAsync();
        }
    }
}
