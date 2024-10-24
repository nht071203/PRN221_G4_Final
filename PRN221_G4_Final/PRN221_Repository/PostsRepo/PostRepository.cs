﻿using PRN221_DataAccess.DAOs;
using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_Repository.PostsRepo
{
    public class PostRepository : IPostRepository
    {
        private readonly PostDAO _postDAO;
        public PostRepository(PostDAO postDAO)
        {
            _postDAO = postDAO;
        }

        public async Task<Post> AddPost(Post post)
        {
            return await _postDAO.Add(post);
        }

        public async Task<Account> ExpertWithMostPosts()
        {
            return await _postDAO.ExpertWithMostPosts();
        }

        public async Task<Account> FarmerWithMostPosts()
        {
            return await _postDAO.FarmerWithMostPosts();    
        }

        public async Task<IEnumerable<Post>> GetAll()
        {
            return await _postDAO.GetAll();
        }

        public async Task<IEnumerable<Post>> GetAllPostByAccountId(int id) => await _postDAO.GetAllPostByAccountId(id);

        public async Task<Post?> GetById(int postId)
        {
            return await _postDAO.GetById(postId);
        }
    }
}
