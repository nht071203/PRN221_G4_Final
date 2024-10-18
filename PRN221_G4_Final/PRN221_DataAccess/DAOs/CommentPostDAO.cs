using Microsoft.EntityFrameworkCore;
using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_DataAccess.DAOs
{
    public class CommentPostDAO : SingletonBase<CommentPostDAO>
    {
        public async Task<IEnumerable<Comment>> GetAllCommentPostByPostId(int id)
        {
            return await _context.Comments.Where(l => l.PostId == id).ToListAsync();
        }
    }
}
