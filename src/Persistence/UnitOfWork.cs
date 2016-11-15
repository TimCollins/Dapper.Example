using Domain.Interfaces;

namespace Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepository CategoryRepository { get; }

        public UnitOfWork()
        {
            CategoryRepository = new CategoryRepository();
        }
    }
}
