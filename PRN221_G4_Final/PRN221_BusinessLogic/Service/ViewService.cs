using PRN221_BusinessLogic.Interface;
using PRN221_Repository.ViewRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_BusinessLogic.Service
{
    public class ViewService : IViewService
    {
        private readonly IViewRepository _viewRepository;
        public ViewService(IViewRepository viewRepository)
        {
            _viewRepository = viewRepository;
        }

        public async Task<int> GetViewByPostId(int postId)
        {
            return await _viewRepository.GetViewByPostId(postId);
        }
    }
}
