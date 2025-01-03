﻿using Microsoft.EntityFrameworkCore;
using PRN221_DataAccess.DAOs;
using PRN221_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221_Repository.NewsRepo
{
    public class NewsRepository : INewsRepository
    {
        private readonly NewsDAO _newsDAO;
        private readonly CategoryNewsDAO _categoryNewsDAO;

        public NewsRepository(NewsDAO newsDAO)
        {
            _newsDAO = newsDAO;
        }

        public async Task<IEnumerable<News>> GetAllNews()
        {
            return await _newsDAO.GetAllNews();
        }

        public async Task<News> GetById(int id) => await _newsDAO.FindById(id);
        public async Task Add(News news) => await _newsDAO.Add(news);
        public async Task Update(News news) => await _newsDAO.Update(news);

        public async Task Delete(int id) => await _newsDAO.Delete(id);

        public async Task<IEnumerable<News>> GetAllNewsByCategoryId(int categoryId) => await _newsDAO.GetAllNewsByCategoryId(categoryId);

        public async Task<CategoryNews> GetCategoryNewsById(int id) => await _categoryNewsDAO.GetCategoryNewsById(id);

        public async Task<int> GetTotalNewsRepo()
        {
            return await _newsDAO.GetTotalNewsCountAsync();
        }
        public async Task<IEnumerable<CategoryNews>> GetAllCategoryNews() => await _categoryNewsDAO.GetAllCategoryNews();

        public async Task<IEnumerable<News>> SearchNews(int category, string searchString) => await _newsDAO.SearchNews(category, searchString);

        public Task<IEnumerable<News>> GetNewsPaged(int pageNumber, int pageSize) => _newsDAO.GetNewsPaged(pageNumber, pageSize);   

        public Task<int> GetTotalNewsCount() => _newsDAO.GetTotalNewsCount();

        public async Task<IEnumerable<(string Month, int Count)>> GetNewsCountByMonth(int year) => await _newsDAO.GetNewsCountByMonth(year);

        public async Task<IEnumerable<(string Day, int Count)>> GetNewsCountByDay(int year, int month) => await _newsDAO.GetNewsCountByDay(year, month);


    }
}
