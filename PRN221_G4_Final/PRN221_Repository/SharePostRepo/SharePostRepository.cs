using PRN221_DataAccess.DAOs;
using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_Repository.SharePostRepo
{
    public class SharePostRepository : ISharePostRepository
    {
        private readonly SharePostDAO _sharePostDAO;

        public SharePostRepository(SharePostDAO sharePostDAO)
        {
            _sharePostDAO = sharePostDAO;
        }

        public async Task<IEnumerable<SharePost>> GetAllSharePostByPostId(int id) => await _sharePostDAO.GetAllSharePostByPostId(id);   
    }
}
