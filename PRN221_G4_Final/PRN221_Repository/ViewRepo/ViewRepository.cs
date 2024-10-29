using PRN221_DataAccess.DAOs;
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

        public async Task<int> GetViewByPostId(int postId)
        {
            return await _viewDAO.GetViewByPostId(postId);
        }
    }
}
