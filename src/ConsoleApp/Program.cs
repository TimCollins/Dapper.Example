using System;
using DataAccess.Impl;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var categoryRepository = new CategoryRepository();
            // Test GetAll()
            var categories = categoryRepository.GetAll();
            Console.WriteLine("There are {0} categories.", categories.Count);

            Util.WaitForEscape();
        }
    }
}
