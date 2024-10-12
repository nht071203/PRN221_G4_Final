﻿using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_Repository.NewsRepo
{
    public interface INewsRepository
    {
        Task<IEnumerable<News>> GetAllNews();
    }
}