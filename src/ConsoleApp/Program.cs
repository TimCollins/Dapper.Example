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

            var categories = uow.CategoryRepository.GetAll();

            Console.WriteLine("There are {0} categories.", categories.Count);

            var category = uow.CategoryRepository.GetById(2);
            Console.WriteLine("The second category is \"{0}\".", category.CategoryName);

            var newCategory = new Category
            {
                CategoryName = "Test Category 2",
                Description = "This is yet another test category"
            };
            int id = uow.CategoryRepository.Add(newCategory);
            Console.WriteLine("Added new category with id of {0}", id);

            Util.WaitForEscape();
        }
    }
}
