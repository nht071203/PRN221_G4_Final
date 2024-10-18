using Microsoft.EntityFrameworkCore;
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
    }
}
