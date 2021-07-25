using System;
using DataAccess.Impl;
using Domain;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var categoryRepository = new CategoryRepository();

            // Test GetAll()
            var categories = categoryRepository.GetAll();
            Console.WriteLine($"There are {categories.Count} categories.");

            // Test GetById()
            var category = categoryRepository.GetById(2);
            Console.WriteLine($"The second category is \"{category.CategoryName}\".");

            // Test Add()
            var newCategory = new Category
            {
                CategoryName = "Test Category 2",
                Description = "This is yet another test category"
            };

            var idToDelete = categoryRepository.Add(newCategory);
            Console.WriteLine($"Added new category with id of {idToDelete}.");

            // Test Update()
            var id = 9;
            category = categoryRepository.GetById(id);
            category.Description = category.Description + " -- edited";
            var rowsModified = categoryRepository.Update(category);
            Console.WriteLine($"Updated existing category. Rows modified: {rowsModified}.");

            // Test Delete<T>()
            int ret;
            category = categoryRepository.GetById(idToDelete);
            if (category == null)
            {
                Console.WriteLine($"Category {idToDelete} doesn't exist.");
            }
            else
            {
                ret = categoryRepository.Delete(category);
                var categoryCount = categoryRepository.GetAll().Count;
                Console.WriteLine($"{ret} category was deleted using an object reference. There are now {categoryCount} categories.");
            }

            // Test Delete(int)
            id = 11;
            ret = categoryRepository.Delete(id);
            if (ret == 0)
            {
                Console.WriteLine($"Category {id} doesn't exist.");
            }
            else
            {
                var categoryCount = categoryRepository.GetAll().Count;
                Console.WriteLine($"{ret} category was deleted using an id value of {id}. There are now {categoryCount} categories.");
            }

            Util.WaitForEscape();
        }
    }
}
