using System;
using Domain.Models;
using Persistence;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            UnitOfWork uow = new UnitOfWork();

            // Test GetAll()
            var categories = uow.CategoryRepository.GetAll();
            Console.WriteLine("There are {0} categories.", categories.Count);

            // Test GetById()
            var category = uow.CategoryRepository.GetById(2);
            Console.WriteLine("The second category is \"{0}\".", category.CategoryName);

            // Test Add()
            var newCategory = new Category
            {
                CategoryName = "Test Category 2",
                Description = "This is yet another test category"
            };
            int idToDelete = uow.CategoryRepository.Add(newCategory);
            Console.WriteLine("Added new category with id of {0}.", idToDelete);

            // Test Update()
            int id = 9;
            category = uow.CategoryRepository.GetById(id);
            category.Description = category.Description + " -- edited";
            Console.WriteLine("Updated existing category. Rows modified: {0}.", uow.CategoryRepository.Update(category));

            // Test Delete<T>()
            int ret;
            category = uow.CategoryRepository.GetById(idToDelete);
            if (category == null)
            {
                Console.WriteLine("Category {0} doesn't exist.", idToDelete);
            }
            else
            {
                ret = uow.CategoryRepository.Delete(category);
                Console.WriteLine("{0} category was deleted using an object reference. There are now {1} categories.", ret, uow.CategoryRepository.GetAll().Count);
            }

            // Test Delete(int)
            id = 11;
            ret = uow.CategoryRepository.Delete(id);
            if (ret == 0)
            {
                Console.WriteLine("Category {0} doesn't exist.", id);
            }
            else
            {
                Console.WriteLine("{0} category was deleted using an id value. There are now {1} categories.", ret, uow.CategoryRepository.GetAll().Count);
            }            

            Util.WaitForEscape();
        }
    }
}
