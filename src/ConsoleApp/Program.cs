using System;
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

            Util.WaitForEscape();
        }
    }
}
