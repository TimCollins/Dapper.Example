using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using Domain.Interfaces;
using Domain.Models;

namespace Persistence
{
    internal class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public List<Category> GetAll()
        {            
            const string sqlQuery = @"SELECT    CategoryID,
                                                CategoryName,
                                                Description,
                                                Picture
                                        FROM Categories";
            var categories = _conn.Query<Category>(sqlQuery).AsList();

            return categories;
        }

        private Category ReadCategory(SqlDataReader reader)
        {
            var category = new Category
            {
                CategoryID = reader.Fetch<int>("CategoryID"),
                CategoryName = reader.Fetch<string>("CategoryName"),
                Description = reader.Fetch<string>("Description"),
                Picture = reader.Fetch<byte[]>("Picture")
            };

            return category;
        }

        public Category GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Add(Category category)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Category category)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(Category category)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
