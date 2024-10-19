﻿using PRN221_Models.DTO;
using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_BusinessLogic.Interface
{
    public interface IPostService
    {
        Task<List<PostDTO>> GetListPostAndImage();
        Task<PostDTO> GetPostAndImage(int postId);

        Task<Account?> FarmerWithMostPosts();

        Task<Account?> ExpertWithMostPosts();
    }
}
