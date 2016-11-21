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

            var category = _conn.Query<Category>(sqlQuery, new {id = id}).FirstOrDefault();

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

        public int Update(Category category)
        {
            const string sqlQuery = @"UPDATE Categories 
                                        SET CategoryName = @name,
                                            Description = @description
                                        WHERE CategoryID = @id";
            var paramsObj = new {name = category.CategoryName, description = category.Description, id=category.CategoryID};
            return _conn.Execute(sqlQuery, paramsObj);
        }

        public int Delete(Category category)
        {
            return DeleteById(category.CategoryID);
        }

        public int Delete(int id)
        {
            return DeleteById(id);
        }

        private int DeleteById(int id)
        {
            const string sqlQuery = @"DELETE FROM Categories 
                                        WHERE CategoryID = @id";
            var paramsObj = new {id = id};

            return _conn.Execute(sqlQuery, paramsObj);
        }
    }
}
