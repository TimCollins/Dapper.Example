using System.Collections.Generic;
using Dapper;
using Domain;

namespace DataAccess.Impl
{
    public class CategoryRepository : BaseRepository
    {
        public List<Category> GetAll()
        {
            var cat = new Domain.Category();

            const string sqlQuery = @"SELECT    CategoryID,
                                                CategoryName,
                                                Description,
                                                Picture
                                        FROM Categories";
            var categories = Query<Category>(sqlQuery).AsList();

            return categories;
        }

    }
}
