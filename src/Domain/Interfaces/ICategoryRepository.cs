using System.Collections.Generic;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
        Category GetById(int id);
        int Add(Category category);
        int Update(Category category);
        int Delete(Category category);
        int Delete(int id);
    }
}
