﻿using PRN221_DataAccess.DAOs;
using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_Repository.CommentRepo
{
    public class CommentRepository : ICommentRepository
    {
        private readonly CommentPostDAO _commentPostDAO;

        public CommentRepository(CommentPostDAO commentPostDAO)
        {
            _commentPostDAO = commentPostDAO;
        }

        public async Task Add(Comment item) => await _commentPostDAO.Add(item);

        public async Task Delete(int id) => await _commentPostDAO.Delete(id);

        public async Task<Comment> FindById(int id) => await _commentPostDAO.FindById(id);

        public async Task<IEnumerable<Comment>> GetAllCommentPostByPostId(int id) => await _commentPostDAO.GetAllCommentPostByPostId(id);

        public async Task Update(Comment item) => await _commentPostDAO.Update(item);
    }
}
