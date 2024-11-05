using PRN221_DataAccess.DAOs;
using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_Repository.ViewRepo
{
    public class ViewRepository : IViewRepository
    {
        private readonly ViewDAO _viewDAO;
        public ViewRepository(ViewDAO viewDAO)
        {
            _viewDAO = viewDAO;
        }

        public async Task AddRecordPost(int acc_id, int post_id)
        {
            var view = new View
            {
                AccountId = acc_id,
                PostId = post_id,
            };
            await _viewDAO.AddRecordPost(view);
        }

        public async Task<int> GetViewByPostId(int postId)
        {
            return await _viewDAO.GetViewByPostId(postId);
        }
    }
}
