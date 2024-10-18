﻿using Microsoft.EntityFrameworkCore;
using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_DataAccess.DAOs
{
    public class PostImageDAO : SingletonBase<PostImageDAO>
    {
        public async Task<IEnumerable<PostImage>> GetAll()
        {
            return await _context.PostImages.ToListAsync();
        }
        
        public async Task<IEnumerable<PostImage>> GetAllByPostId(int postId)
        {
            return await _context.PostImages.Where(img => img.PostId == postId).ToListAsync();
        }
    }
}