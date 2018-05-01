namespace Contracts
{
    public interface IRepositoryWrapper
    {
        ICategoryRepository Category { get; set; }
        IAccountRepository Account { get; set; }
        IUsersRepository Users { get; set; }
        IItemRepository Items { get; set; }
        ICompanyRepository Company { get; set; }
        IDashboardRepository Dashboard { get; set; }
    }
}
