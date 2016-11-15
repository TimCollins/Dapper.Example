namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        ICategoryRepository CategoryRepository { get; }
    }
}
