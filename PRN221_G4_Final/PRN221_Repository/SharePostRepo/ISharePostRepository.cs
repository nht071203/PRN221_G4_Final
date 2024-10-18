using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_Repository.SharePostRepo
{
    public interface ISharePostRepository
    {
        Task<IEnumerable<SharePost>> GetAllSharePostByPostId(int id);
    }
}
