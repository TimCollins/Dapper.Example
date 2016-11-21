using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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

        public Category GetById(int id)
        {
            const string sqlQuery = @"SELECT	CategoryID,
                                                CategoryName,
                                                Description,
                                                Picture
                                        FROM Categories
                                        WHERE CategoryID = @id";

            var category = _conn.Query<Category>(sqlQuery, new {id = id}).Single();

            return category;
        }

        public int Add(Category category)
        {
            const string sqlQuery = @"INSERT Categories(CategoryName, Description) 
                                        VALUES (@name, @description) 
                                        SELECT CAST(SCOPE_IDENTITY() AS INT)";
            var paramsObj = new {name = category.CategoryName, description = category.Description};
            category.CategoryID = _conn.Query<int>(sqlQuery, paramsObj).First();

            return category.CategoryID;
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
    }
}
