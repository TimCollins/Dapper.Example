using System.Collections.Generic;
using System.Data.SqlClient;
using Domain.Interfaces;
using Domain.Models;

namespace Persistence
{
    internal class CategoryRepository : ICategoryRepository
    {
        public List<Category> GetAll()
        {
            var categories = new List<Category>();
            const string sqlQuery = @"SELECT    CategoryID,
                                                CategoryName,
                                                Description,
                                                Picture
                                        FROM Categories";

            using (var reader = DbUtil.ExecuteReader(sqlQuery))
            {
                while (reader.Read())
                {
                    categories.Add(ReadCategory(reader));
                }
            }

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
