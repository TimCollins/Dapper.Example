using System.Collections.Generic;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
        Category GetById(int id);
        int Add(Category category);
        void Update(Category category);
        void Delete(Category category);
        void Delete(int id);
    }
}
