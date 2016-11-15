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
            Util.WaitForEscape();
        }
    }
}
