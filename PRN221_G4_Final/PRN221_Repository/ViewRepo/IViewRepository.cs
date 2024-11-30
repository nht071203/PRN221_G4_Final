using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_Repository.ViewRepo
{
    public interface IViewRepository
    {
        Task<int> GetViewByPostId(int postId);
        Task AddRecordPost(int acc_id, int post_id);
    }
}
