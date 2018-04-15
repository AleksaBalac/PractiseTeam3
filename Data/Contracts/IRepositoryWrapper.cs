namespace Contracts
{
    public interface IRepositoryWrapper
    {
        ICategoryRepository Category { get; set; }
        IAccountRepository Account { get; set; }
    }
}
